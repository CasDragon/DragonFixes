using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic;

namespace DragonFixes.Fixes.Items;

public class CloakOfBloodscent
{
    [DragonConfigure]
    public static void PatchCloakOfBloodscentFeature()
    {
        Main.log.Log("Patching CloakOfBloodscentFeature to include Skald rage.");
        FeatureConfigurator.For(FeatureRefs.CloakOfBloodScentFeature)
            .AddBuffExtraEffects(
                checkedBuff: BuffRefs.InspiredRageEffectBuffBeforeMasterSkald.ToString(),
                extraEffectBuff: BuffRefs.CloakOfBloodScentBuff.ToString()
            )
            .AddBuffExtraEffects(
                checkedBuff: BuffRefs.InspiredRageEffectBuffMythic.Reference.Get(),
                extraEffectBuff: BuffRefs.CloakOfBloodScentBuff.Reference.Get()
            )
            .Configure();
    }
}