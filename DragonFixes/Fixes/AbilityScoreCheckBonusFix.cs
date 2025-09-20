using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.View.MapObjects;

namespace DragonFixes.Fixes
{
    [HarmonyPatch]
    internal class AbilityScoreCheckBonusFix
    {

        // From ADDB 
        [HarmonyPatch(typeof(AbilityScoreCheckBonus), nameof(AbilityScoreCheckBonus.OnEventAboutToTrigger)), HarmonyPrefix]
        private static bool OnEventAboutToTrigger(RuleRollD20 evt, AbilityScoreCheckBonus __instance)
        {
            RuleSkillCheck ruleSkillCheck = Rulebook.CurrentContext.PreviousEvent as RuleSkillCheck;
            if (ruleSkillCheck != null && ruleSkillCheck.StatType == __instance.Stat)
            {
                var stat = ruleSkillCheck.Initiator.Stats.GetStat(__instance.Stat);
                if (stat != null)
                {
                    int bonus = __instance.Bonus.Calculate(__instance.Context);
                    if (StatTypeHelper.IsAttribute(__instance.Stat))
                    {
                        bonus *= 2;
                    }
                    ruleSkillCheck.AddTemporaryModifier(stat.AddModifier(bonus, __instance.Runtime, __instance.Descriptor));
                    ruleSkillCheck.UpdateValues();
                }
            }
            return false;
        }
    }
}
