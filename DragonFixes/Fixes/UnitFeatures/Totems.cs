using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace DragonFixes.Fixes.UnitFeatures;

public class Totems
{
    [DragonConfigure]
    public static void PatchTotemFeatues()
    {
        Main.log.Log("Patching various totem features to include StandartFocusedRageBuff");
        Blueprint<BlueprintReference<BlueprintFeature>>[] features =
        [
            FeatureRefs.CelestialTotemFeature, FeatureRefs.CelestialTotemGreaterFeature,
            FeatureRefs.CelestialTotemLesserFeature, FeatureRefs.DaemonTotemFeature,
            FeatureRefs.DaemonTotemGreaterFeature, FeatureRefs.DaemonTotemLesserFeature,
        ];
        Blueprint<BlueprintReference<BlueprintBuff>>[] buffs =
        [
            BuffRefs.CelestialTotemAreaBuff, BuffRefs.CelestialTotemGreaterBuff,
            BuffRefs.CelestialTotemLesserBuff, BuffRefs.DaemonTotemBuff,
            BuffRefs.DaemonTotemGreaterBuff, BuffRefs.DaemonTotemLesserBaseBuff,
        ];
        var StandartFocusedRageBuff = BuffRefs.StandartFocusedRageBuff.ToString();
        for (var index = 0; index < features.Length; index++)
        {
            var feature = features[index];
            FeatureConfigurator.For(feature)
                .AddBuffExtraEffects(
                    checkedBuff: StandartFocusedRageBuff,
                    extraEffectBuff: buffs[index].ToString()
                )
                .Configure();
        }
    }
}