using System.Collections.Generic;
using System.Reflection.Emit;
using BlueprintCore.Blueprints.References;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
namespace DragonFixes.Patches;

[HarmonyPatch(typeof(RuleAttackRoll), nameof(RuleAttackRoll.OnTrigger))]
public class MirrorImage
{
    static readonly System.Reflection.MethodInfo IsImmune =
        AccessTools.PropertyGetter(typeof(UnitDescriptor), nameof(UnitDescriptor.IsImmuneToVisualEffects));

    static readonly System.Reflection.FieldInfo InitiatorField =
        AccessTools.Field(typeof(RulebookEvent), nameof(RulebookEvent.Initiator));

    static readonly System.Reflection.MethodInfo Helper =
        AccessTools.Method(typeof(MirrorImage), nameof(HasEcholocation));

    private static BlueprintBuffReference EcholocationBuff;
    public static bool HasEcholocation(UnitEntityData unit)
    {
        EcholocationBuff ??= BuffRefs.EcholocationBuff.Reference.Get().ToReference<BlueprintBuffReference>();
        return unit.Buffs.HasFact(EcholocationBuff);
    }

    [HarmonyTranspiler]
    static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var code = new List<CodeInstruction>(instructions);
        bool patched = false;

        for (int i = 0; i < code.Count - 2; i++)
        {
            // callvirt get_IsImmuneToVisualEffects  ->  brtrue(.s) <skip>
            if (code[i].Calls(IsImmune) &&
                (code[i + 1].opcode == OpCodes.Brtrue || code[i + 1].opcode == OpCodes.Brtrue_S))
            {
                var skipLabel = code[i + 1].operand; // reuse the exact same target

                var insert = new List<CodeInstruction>
                {
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldfld, InitiatorField),
                    new CodeInstruction(OpCodes.Call, Helper),
                    new CodeInstruction(OpCodes.Brtrue, skipLabel),
                };

                // Anything that jumped to the following instruction (ldarg.0 @ 429)
                // must now land on the start of our inserted block.
                insert[0].labels.AddRange(code[i + 2].labels);
                code[i + 2].labels.Clear();

                code.InsertRange(i + 2, insert);
                patched = true;
                break;
            }
        }

        if (!patched)
            UnityModManagerNet.UnityModManager.Logger.Error("EcholocationVisualPatch: target IL not found");

        return code;
    }
}