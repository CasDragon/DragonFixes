using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities.Blueprints;

namespace DragonFixes.Fixes.UnitFeatures;

public class Domains
{
    // Nobility Domain
    [DragonConfigure]
    public static void PatchInspiringCommand()
    {
        Main.log.Log("Patching Inspiring Command");
        AbilityConfigurator.For(AbilityRefs.NobilityDomainBaseAbility)
            .SetType(AbilityType.Supernatural)
            .Configure();
        AbilityConfigurator.For(AbilityRefs.NobilityDomainBaseAbilitySeparatist)
            .SetType(AbilityType.Supernatural)
            .Configure();
    }
    
    // Scalykind

    [DragonConfigure]
    public static void PatchScalykind()
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

    [DragonConfigure]
    public static void PatchDomainZealot()
    {
        Main.log.Log("Patching Domain Zealot for Scalykind");
        FeatureConfigurator.For(FeatureRefs.DomainMastery)
            .EditComponent<AutoMetamagic>(c => addstuff(c))
            .Configure();
    }

    public static void addstuff(AutoMetamagic component)
    {
        component.Abilities.Add(AbilityRefs.ScalykindDomainBaseFeatureAbility.Reference.Get().ToReference<BlueprintAbilityReference>());
        component.Abilities.Add(AbilityRefs.ScalykindDomainBaseFeatureAbilitySeparatist.Reference.Get().ToReference<BlueprintAbilityReference>());

    }
}