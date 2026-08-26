using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonFixes.Fixes.UnitFeatures;

public class Domains
{
    // Death Domain
    [DragonConfigure]
    public static void PatchDeathDomainAllowedSeparatist()
    {
        Main.log.Log("Patching DeathDomainAllowedSeparatist to remove AddSpecialSpellListForArchetype");
        var feat = FeatureRefs.DeathDomainAllowedSeparatist.Reference.Get();
        var comp = feat.GetComponent<AddSpecialSpellListForArchetype>();
        if (comp != null)
            DragonHelpers.RemoveComponent(feat, comp);
    }
    
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
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.ExtraDomain)
            .AddToAllFeatures(ProgressionRefs.ScalykindDomainProgressionSecondary.Reference.Get())
            .Configure();
    }

    [DragonConfigure]
    public static void PatchDomainZealot()
    {
        Main.log.Log("Patching Domain Zealot for Scalykind");
        FeatureConfigurator.For(FeatureRefs.DomainMastery)
            .EditComponent<AutoMetamagic>(addstuff)
            .Configure();
    }

    public static void addstuff(AutoMetamagic component)
    {
        component.Abilities.Add(AbilityRefs.ScalykindDomainBaseFeatureAbility.Reference.Get().ToReference<BlueprintAbilityReference>());
        component.Abilities.Add(AbilityRefs.ScalykindDomainBaseFeatureAbilitySeparatist.Reference.Get().ToReference<BlueprintAbilityReference>());

    }
}