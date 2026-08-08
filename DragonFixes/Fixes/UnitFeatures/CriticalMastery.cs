using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Prerequisites;

namespace DragonFixes.Fixes.UnitFeatures;

public class CriticalMastery
{
    
    [DragonConfigure]
    public static void PatchCriticalMastery()
    {
        Main.log.Log("Patching CriticalMastery to include Bleeding/Flaying Critical");
        var x = FeatureRefs.CriticalMastery.Reference.Get().GetComponent<PrerequisiteFeaturesFromList>();
        x!.m_Features = [.. x.m_Features, FeatureRefs.FlayingCriticalFeature.Reference.Get().ToReference<BlueprintFeatureReference>(),
            FeatureRefs.BleedingCriticalFeature.Reference.Get().ToReference<BlueprintFeatureReference>()];
    }
}