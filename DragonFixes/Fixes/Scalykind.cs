﻿using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonFixes.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes
{
    internal class Scalykind
    {
        [DragonFix]
        public static void PatchScalykind()
        {
            if (Settings.GetSetting<bool>("scalykind"))
            {
                Main.log.Log("Patching second domain selection.");
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.SecondDomainsSelection)
                    .AddToAllFeatures(ProgressionRefs.ScalykindDomainProgressionSecondary.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.SecondDomainsSeparatistSelection)
                    .AddToAllFeatures(ProgressionRefs.ScalykindDomainProgressionSeparatist.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.ExtraDomain)
                    .AddToAllFeatures(ProgressionRefs.ScalykindDomainProgressionSeparatist.Reference.Get())
                    .Configure();
            }
            else
            {
                Main.log.Log("Scalykind patch disabled, skipping.");
            }
        }
        [DragonFix]
        public static void PatchDomainZealot()
        {
            if (Settings.GetSetting<bool>("scalykinddomain"))
            {
                Main.log.Log("Patching Domain Zealot for Scalykind");
                FeatureConfigurator.For(FeatureRefs.DomainMastery)
                    .EditComponent<AutoMetamagic>(c => addstuff(c))
                    .Configure();
            }
            else
            {
                Main.log.Log("Scalykind Domain Zealot patch disabled, skipping.");
            }
        }
        public static void addstuff(AutoMetamagic component)
        {
            component.Abilities.Add(AbilityRefs.ScalykindDomainBaseFeatureAbility.Reference.Get().ToReference<BlueprintAbilityReference>());
            component.Abilities.Add(AbilityRefs.ScalykindDomainBaseFeatureAbilitySeparatist.Reference.Get().ToReference<BlueprintAbilityReference>());

        }

    }
}
