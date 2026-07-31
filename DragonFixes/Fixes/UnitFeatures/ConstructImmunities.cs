using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;

namespace DragonFixes.Fixes.UnitFeatures;

public class ConstructImmunities
{
    [DragonConfigure]
    public static void PatchConstructImmunities()
    {
        Main.log.Log("Patching ConstructImmunities to include immunity to energy drain component.");
        FeatureConfigurator.For(FeatureRefs.ConstructImmunities)
            .AddImmunityToEnergyDrain()
            .Configure();
    }
}