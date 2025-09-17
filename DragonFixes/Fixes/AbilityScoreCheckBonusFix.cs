using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;

namespace DragonFixes.Fixes
{
    [HarmonyPatch]
    internal class AbilityScoreCheckBonusFix
    {

        // From ADDB 
        [HarmonyPatch(typeof(AbilityScoreCheckBonus), nameof(AbilityScoreCheckBonus.OnEventAboutToTrigger)), HarmonyPrefix]
        private static bool OnEventAboutToTrigger(RuleRollD20 evt, AbilityScoreCheckBonus __instance)
        {
            if (Rulebook.CurrentContext.PreviousEvent is RuleSkillCheck ruleSkillCheck && ruleSkillCheck.StatType == __instance.Stat)
            {
                var stat = ruleSkillCheck.Initiator.Stats.GetStat(__instance.Stat);
                if (stat != null)
                {
                    ruleSkillCheck.AddTemporaryModifier(stat.AddModifier(__instance.Bonus.Calculate(__instance.Context), __instance.Runtime, __instance.Descriptor));
                    ruleSkillCheck.UpdateValues();
                }
            }
            return false;
        }
    }
}
