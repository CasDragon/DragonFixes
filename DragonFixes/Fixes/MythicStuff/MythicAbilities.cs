using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;

namespace DragonFixes.Fixes.MythicStuff;

public class MythicAbilities
{
    public static void PatchIsClassFeature()
    {
        Main.log.Log("Patching Perfect Cavalry mythic ability `isClassFeature`");
        FeatureConfigurator.For(FeatureRefs.PerfectCavalry)
            .SetIsClassFeature(true)
            .Configure();
        Main.log.Log("Patching Explosives Expert mythic ability `isClassFeature`");
        FeatureConfigurator.For(FeatureRefs.ExplosiveExpert)
            .SetIsClassFeature(true)
            .Configure();
        Main.log.Log("Patching Mythic Inspiration mythic ability `isClassFeature`");
        FeatureConfigurator.For(FeatureRefs.MythicInspire)
            .SetIsClassFeature(true)
            .Configure();
        Main.log.Log("Patching Spellcaster's Onslaught mythic ability `isClassFeature`");
        FeatureConfigurator.For(FeatureRefs.CasterOnslaught)
            .SetIsClassFeature(true)
            .Configure();
        Main.log.Log("Patching School Tolerance mythic ability `isClassFeature`");
        FeatureConfigurator.For(FeatureRefs.SchoolTolerance)
            .SetIsClassFeature(true)
            .Configure();
    }
}