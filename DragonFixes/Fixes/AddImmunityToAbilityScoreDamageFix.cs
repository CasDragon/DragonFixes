using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonFixes.Fixes
{
    [HarmonyPatch]
    internal class AddImmunityToAbilityScoreDamageFix
    {
        [HarmonyPatch(typeof(AddImmunityToAbilityScoreDamage), nameof(AddImmunityToAbilityScoreDamage.OnEventAboutToTrigger))]
        [HarmonyPrefix]
        public static bool FixNonsense(RuleDealStatDamage evt)
        {
            return !evt.Immune;
        }
    }
}
