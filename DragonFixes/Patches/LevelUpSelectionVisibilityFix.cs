using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UI.Common;
using Kingmaker.UI.MVVM._VM.CharGen;

namespace DragonFixes.Patches;

/// <summary>
/// UIUtilityUnit.IsHiddenByPrerequisites checks the currently selected character, but
/// CharGenVM.CreateNewOrUpdateFeaturePhases uses it to decide whether a level up feature
/// selection phase appears for the unit being leveled. Nothing syncs the party bar selection to
/// that unit outside character creation, so a valid phase (Vivisectionist discoveries, for
/// example) silently vanishes whenever a different character is selected, and the pick is lost.
/// This retargets that one call to check the level up controller's preview unit instead.
/// </summary>
[HarmonyPatch]
internal class LevelUpSelectionVisibilityFix
{
    [HarmonyPatch(typeof(CharGenVM), "CreateNewOrUpdateFeaturePhases")]
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        MethodInfo vanillaCheck = AccessTools.Method(typeof(UIUtilityUnit), nameof(UIUtilityUnit.IsHiddenByPrerequisites));
        MethodInfo replacement = ((Func<BlueprintFeature, CharGenVM, bool>)IsHiddenForLevelingUnit).Method;
        bool patched = false;

        foreach (CodeInstruction instruction in instructions)
        {
            if (!patched && instruction.Calls(vanillaCheck))
            {
                instruction.opcode = OpCodes.Ldarg_0;
                instruction.operand = null;
                yield return instruction;
                yield return new CodeInstruction(OpCodes.Call, replacement);
                patched = true;
                continue;
            }

            yield return instruction;
        }

        if (!patched)
        {
            Main.log.Log("LevelUpSelectionVisibilityFix: IsHiddenByPrerequisites call not found, phase visibility fix not applied.");
        }
    }

    private static bool IsHiddenForLevelingUnit(BlueprintFeature feature, CharGenVM vm)
    {
        UnitEntityData unit = vm.m_LevelUpController?.Preview ?? vm.m_LevelUpController?.Unit;
        if (unit == null)
        {
            return false;
        }

        return !feature.MeetsPrerequisites(null, unit, null, fromProgression: false);
    }
}
