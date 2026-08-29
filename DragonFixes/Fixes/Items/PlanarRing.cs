using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Designers.Mechanics.Facts;

namespace DragonFixes.Fixes.Items;

public class PlanarRing
{
    [DragonConfigure]
    public static void PatchFeature()
    {
        Main.log.Log("Patching PortalStonePlanarRingFeature to maximize the correct levels");
        FeatureConfigurator.For(FeatureRefs.PortalStonePlanarRingFeature)
            .EditComponent<AutoMetamagic>(c => c.MaxSpellLevel = 6)
            .Configure();
    }
}