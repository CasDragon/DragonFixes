using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Kingmaker.Items;
using Kingmaker.RuleSystem.Rules;

namespace DragonFixes.Fixes
{
    [HarmonyPatch]
    internal class WeaponEnhancementPatch
    {
        [HarmonyPatch(typeof(RuleCalculateWeaponStats), nameof(RuleCalculateWeaponStats.OnTrigger)), HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> SomeName(IEnumerable<CodeInstruction> instructions)
        {
            var method = AccessTools.PropertyGetter(typeof(ItemEntity), nameof(ItemEntity.EnchantmentValue));
            foreach (var inst in instructions)
            {
                if (inst.Calls(method))
                {
                    yield return new(OpCodes.Pop);
                    yield return new(OpCodes.Ldc_I4_0);
                }
                else
                {
                    yield return inst;
                }
            }
        }
    }
}
