using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonFixes.Util;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Prerequisites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace DragonFixes.Fixes.Classes
{
    internal class Oracle
    {
        private const string settingname = "oracleapsurestrictionremoval";
        private const string settingdescription = "Allow Oracle to select Apsu as a deity";
        [DragonConfigure]
        [DragonSetting(SettingCategories.None, settingname, settingdescription)]
        public static void RemoveApsuRestriction()
        {
            if (SettingsAction.GetSetting<bool>(settingname))
            {
                Main.log.Log("Removing Apsu restriction from Oracle");
                CharacterClassConfigurator.For(CharacterClassRefs.OracleClass)
                    .RemoveComponents(c => c is PrerequisiteNoFeature feature && feature.m_Feature.deserializedGuid == "772e2673945e4583a804ae01f67efea0")
                    .Configure();
                FeatureConfigurator.For("772e2673945e4583a804ae01f67efea0")
                    .RemoveComponents(c => c is PrerequisiteNoClassLevel cclass && cclass.m_CharacterClass.deserializedGuid == "20ce9bf8af32bee4c8557a045ab499b1")
                    .RemoveComponents(c => c is PrerequisiteClassLevel cl && cl.m_CharacterClass.deserializedGuid == "20ce9bf8af32bee4c8557a045ab499b1")
                    .Configure();
            }
            else
            {
                Main.log.Log("Oracle Apsu patch disabled, skipping.");
            }
        }
        [DragonConfigure]
        public static void PatchOracleRevelations()
        {
            Blueprint<BlueprintReference<BlueprintFeature>>[] toFixFeatures =
            [
                FeatureRefs.OracleRevelationChannel, FeatureRefs.OracleRevelationCombatHealer,
                FeatureRefs.OracleRevelationEnhancedCures, FeatureRefs.OracleRevelationFluidNature,
                FeatureRefs.OracleRevelationInvisibility, FeatureRefs.OracleRevelationInvisibilityGreater,
                FeatureRefs.OracleRevelationLifesense, FeatureRefs.Lifesense, FeatureRefs.OracleRevelationSpiritBoost,
                FeatureRefs.FortuneRevelationFeature, FeatureRefs.MisfortuneRevelationFeature
            ];
            Blueprint<BlueprintReference<BlueprintBuff>>[] toFixBuffs =
            [
                BuffRefs.OracleRevelationFriendToAnimalsBuff, BuffRefs.OracleRevelationTouchOfAcidBuff,
                BuffRefs.OracleRevelationTouchOfElectricity11Buff, BuffRefs.OracleRevelationTouchOfFlame11Buff,
                BuffRefs.OracleRevelationWintryTouch11Buff, BuffRefs.FortuneRevelationBuff, BuffRefs.MisfortuneRevelationBuff
            ];
            foreach (var x in toFixFeatures)
            {
                Main.log.Log($"Patching {x.Reference.NameSafe()} isClassFeature to true");
                FeatureConfigurator.For(x)
                    .SetIsClassFeature(true)
                    .Configure();
            }
            foreach (var x in toFixBuffs)
            {
                Main.log.Log($"Patching {x.Reference.NameSafe()} isClassFeature to true");
                BuffConfigurator.For(x)
                    .SetIsClassFeature(true)
                    .Configure();
            }
            Main.log.Log("Patching Form of Flame Revelation progression `isClassFeature` to true");
            ProgressionConfigurator.For(ProgressionRefs.OracleRevelationFormOfFlame)
                .SetIsClassFeature(true)
                .Configure();
        }
    }
}
