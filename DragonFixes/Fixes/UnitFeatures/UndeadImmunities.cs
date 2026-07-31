using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.EntitySystem.Stats;

namespace DragonFixes.Fixes.UnitFeatures;

public class UndeadImmunities
{
    [DragonConfigure]
    public static void PatchUndeadImmunities()
    {
        Main.log.Log("Patching UndeadImmunities to include RecalculateOnStatChange component.");
        FeatureConfigurator.For(FeatureRefs.UndeadImmunities)
            .AddRecalculateOnStatChange(stat: StatType.Charisma)
            .Configure();
    }
}