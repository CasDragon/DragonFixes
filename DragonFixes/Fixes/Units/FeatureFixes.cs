using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using DragonFixes.Util;
using Kingmaker.UnitLogic.Abilities.Components;

namespace DragonFixes.Fixes.Units;

public class FeatureFixes
{
    
    [DragonConfigure]
    public static void PatchAbrikandilu_Feature_Mutilation()
    {
        Main.log.Log("Patching Abrikandilu_Feature_Mutilation to have correct DC.");
        FeatureConfigurator.For(FeatureRefs.Abrikandilu_Feature_Mutilation)
            .EditComponent<AddInitiatorAttackWithWeaponTrigger>(c =>
                FixHelpers.SetCustomDC(c.Action.Actions.OfType<ContextActionSavingThrow>().FirstOrDefault(), 13))
            .Configure();
    }
    [DragonConfigure]
    public static void PatchSchir_DiseaseFeature()
    {
        Main.log.Log("Patching Schir_DiseaseFeature to have correct DC.");
        BlueprintFeature x = FeatureConfigurator.For(FeatureRefs.Schir_DiseaseFeature)
            .EditComponent<AddInitiatorAttackWithWeaponTrigger>(c =>
                FixHelpers.SetCustomDC(c.Action.Actions.OfType<ContextActionSavingThrow>().FirstOrDefault(), 15))
            .Configure();
        DragonHelpers.RemoveComponent<ContextCalculateAbilityParams>(x);
        DragonHelpers.RemoveComponent<RecalculateOnStatChange>(x);
    }

    [DragonConfigure]
    public static void PatchShatterScream()
    {
        Main.log.Log("Patching DLC3_FAB_ShatterScream_Stage2 to dispel correctly");
        var x = AbilityRefs.DLC3_FAB_ShatterScream_Stage2.Reference.Get();
        if (x.m_AllElements.First(a => a is ContextActionDispelMagic) is ContextActionDispelMagic dispel)
            dispel.CheckSchoolOrDescriptor = false;
    }
}