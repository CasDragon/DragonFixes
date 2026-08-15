using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;

namespace DragonFixes.Fixes.UnitFeatures;

public class FightersFiness
{
    [DragonConfigure]
    public static void PatchFighterFinessDamageFeature()
    {
        Main.log.Log("Patching FighterFinessDamageFeature to be correctly removed upon respec");
        FeatureConfigurator.For(FeatureRefs.FighterFinessDamageFeature)
            .SetIsClassFeature(true)
            .Configure();
    }
}