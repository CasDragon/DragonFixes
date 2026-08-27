using System;
using HarmonyLib;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Parts;

namespace DragonFixes.Patches
{
    /// <summary>
    /// Bolstered Spell's splash effect is dealt from a plain Rulebook.Trigger once the ability has
    /// finished executing, outside any context or fact scope. AssignSource then has nothing to work
    /// from and leaves Reason.Ability and Reason.Context both as null. Every component check for
    /// whether this damage came from a spell or not, skips the splash. Elemental Conversion is the
    /// visible case, leaving the bolstered damage in the spell's original element.
    /// To fix, we hand the rule the ability context it was built from.
    /// </summary>
    [HarmonyPatch]
    internal class BolsteredSplashReasonFix
    {
        [HarmonyPatch(typeof(UnitPartBolsteredAoE), "CreateDamageRule"), HarmonyPostfix]
        private static void CreateDamageRule_Postfix(UnitPartBolsteredAoE __instance, RuleDealDamage __result)
        {
            try
            {
                if (__result == null || __instance.m_AbilityContext == null)
                {
                    return;
                }

                __result.Reason = new RuleReason(__instance.m_AbilityContext);
            }
            catch (Exception e)
            {
                Main.log.Log($"BolsteredSplashReasonFix failed: {e}");
            }
        }
    }
}
