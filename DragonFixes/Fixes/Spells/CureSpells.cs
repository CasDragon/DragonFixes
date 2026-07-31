using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonFixes.Util;
using DragonLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Spells
{
    internal class CureSpells
    {
        private const string settingname = "curespellstargetfix";
        private const string settingdescription = "Fixes several healing spells (Like Heal and Cure Light Wounds) to target enemies";
        [DragonConfigure]
        [DragonSetting(SettingCategories.None, settingname, settingdescription)]
        public static void TargetEnemiesPatch()
        {
            if (SettingsAction.GetSetting<bool>(settingname))
            {
                Main.log.Log("Patching cure spells to target enemies.");
                AbilityConfigurator.For(AbilityRefs.CureLightWounds)
                    .SetCanTargetEnemies(true)
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.CureModerateWounds)
                    .SetCanTargetEnemies(true)
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.CureSeriousWounds)
                    .SetCanTargetEnemies(true)
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.CureCriticalWounds)
                    .SetCanTargetEnemies(true)
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.CureLightWoundsCast)
                    .SetCanTargetEnemies(true)
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.CureModerateWoundsCast)
                    .SetCanTargetEnemies(true)
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.CureSeriousWoundsCast)
                    .SetCanTargetEnemies(true)
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.CureCriticalWoundsCast)
                    .SetCanTargetEnemies(true)
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.WhiteMageCureLightWoundsCast)
                    .SetCanTargetEnemies(true)
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.WhiteMageCureModerateWoundsCast)
                    .SetCanTargetEnemies(true)
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.WhiteMageCureSeriousWoundsCast)
                    .SetCanTargetEnemies(true)
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.WhiteMageCureCriticalWoundsCast)
                    .SetCanTargetEnemies(true)
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.Heal)
                    .SetCanTargetEnemies(true)
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.HealCast)
                    .SetCanTargetEnemies(true)
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.WitchHexLifeGiverAbility)
                    .SetCanTargetEnemies(true)
                    .Configure();
            }
            else
            {
                Main.log.Log("Cure spell patch disabled, skipping.");
            }
        }
    }
}
