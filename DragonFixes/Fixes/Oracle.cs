﻿using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonFixes.Util;
using Kingmaker.Blueprints.Classes.Prerequisites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes
{
    internal class Oracle
    {
        [DragonFix]
        public static void RemoveApsuRestriction()
        {
            if (Settings.GetSetting<bool>("oracleapsurestrictionremoval"))
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
        [DragonFix]
        public static void PatchOracleRevelations()
        {
            Main.log.Log("Patching Fortune Revelation feature `isClassFeature`");
            FeatureConfigurator.For(FeatureRefs.FortuneRevelationFeature)
                .SetIsClassFeature(true)
                .Configure();
            Main.log.Log("Patching Mistfortune Revelation feature `isClassFeature`");
            FeatureConfigurator.For(FeatureRefs.MisfortuneRevelationFeature)
                .SetIsClassFeature(true)
                .Configure();
        }
    }
}
