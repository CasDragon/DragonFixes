using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonFixes.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes
{
    internal class CureSpells
    {
        public static void TargetEnemiesPatch()
        {
            if (Settings.GetSetting<bool>("curespellstargetfix"))
            {
                AbilityConfigurator.For(AbilityRefs.CureLightWounds)
                .SetCanTargetEnemies(true)
                .Configure();
                AbilityConfigurator.For(AbilityRefs.CureModerateWounds)
                    .SetCanTargetEnemies(true)
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.CureSeriousWounds)
                    .SetCanTargetEnemies(true)
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.Heal)
                    .SetCanTargetEnemies(true)
                    .Configure();
            }
        }
    }
}
