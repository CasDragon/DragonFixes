using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Craft;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;

namespace DragonFixes.Patches
{
    [HarmonyPatch]
    internal class ScribeScrollsPatch
    {
        [HarmonyPatch(typeof(CraftRoot), nameof(CraftRoot.CheckCraftAvail)), HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> A(IEnumerable<CodeInstruction> instructions)
        {
            var method = AccessTools.Method(typeof(UnitHelper), nameof(UnitHelper.HasFact), [typeof(UnitEntityData), typeof(BlueprintFact)]);
            var field = AccessTools.Field(typeof(CraftRequirements), nameof(CraftRequirements.RequiredFeature));
            bool wasField = false;
            foreach (var inst in instructions)
            {
                if (inst.LoadsField(field))
                {
                    wasField = true;
                    yield return inst;
                    continue;
                }
                else if (inst.opcode == OpCodes.Call && wasField)
                {
                    yield return new(OpCodes.Dup);
                    yield return inst;
                }
                else if (inst.Calls(method))
                {
                    yield return CodeInstruction.Call((UnitEntityData unit, BlueprintFeatureReference reference, BlueprintFact fact) => ButWhatAboutEmpty(unit, reference, fact));
                }
                else
                {
                    yield return inst;
                }
                wasField = false;
            }
        }
        private static bool ButWhatAboutEmpty(UnitEntityData unit, BlueprintFeatureReference reference, BlueprintFact fact)
        {
            return (reference?.IsEmpty() ?? true) || unit.HasFact(fact);
        }
    }
}
