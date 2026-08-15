using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Prerequisites;

namespace DragonFixes.Fixes.MythicStuff;

public class MythicFeats
{
    [DragonConfigure]
    public static void PatchAbundantArcanePool()
    {
        Main.log.Log("Patching Abundant Arcane Pool for Spell Dancer");
        FeatureConfigurator.For(FeatureRefs.AbundantArcanePool)
            .AddPrerequisiteFeature(FeatureRefs.SpellDanceFeature.Reference.Get(), false, Prerequisite.GroupType.Any, false)
            .Configure();
    }
}