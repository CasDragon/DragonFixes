using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;

namespace DragonFixes.Fixes.Classes;

public class Barbarian
{
    [DragonConfigure]
    public static void PatchCrushAndTearFeature()
    {
        Main.log.Log("Patching CrushAndTearFeature to work at level 5");
        AddFeatureOnClassLevel c = FeatureRefs.CrushAndTearFeature.Reference.Get().GetComponent<AddFeatureOnClassLevel>(com => com.Level == 5);
        c!.m_Feature = FeatureRefs.CrushAndTearFeatureLevelUp5.Reference.Get().ToReference<BlueprintFeatureReference>();
    }
}