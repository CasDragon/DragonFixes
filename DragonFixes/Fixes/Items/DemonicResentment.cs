using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;

namespace DragonFixes.Fixes.Items;

public class DemonicResentment
{
    [DragonConfigure]
    public static void PatchFeature()
    {
        Main.log.Log("Patching DemonicResentmentFeature to include more rages");
        var buff = BuffRefs.DemonicResentmentBuff.ToString();
        FeatureConfigurator.For(FeatureRefs.DemonicResentmentFeature)
            .AddBuffExtraEffects(
                checkedBuff: BuffRefs.InciteRageEffectBuff.ToString(),
                extraEffectBuff: buff
            )
            .AddBuffExtraEffects(
                checkedBuff: BuffRefs.ElementalRampagerRampageBuff.ToString(),
                extraEffectBuff: buff
            )
            .AddBuffExtraEffects(
                checkedBuff: BuffRefs.RageshaperDevastatingFormBuff.ToString(),
                extraEffectBuff: buff
            )
            .AddBuffExtraEffects(
                checkedBuff: BuffRefs.RageSpellBuff.ToString(),
                extraEffectBuff: buff
            )
            .AddBuffExtraEffects(
                checkedBuff: BuffRefs.Gorum_Buff.ToString(),
                extraEffectBuff: buff
            )
            .AddBuffExtraEffects(
                checkedBuff: BuffRefs.InspiredRageEffectBuffMythic.ToString(),
                extraEffectBuff: buff
            )
            .AddBuffExtraEffects(
                checkedBuff: BuffRefs.DemonRageBuff.ToString(),
                extraEffectBuff: buff
            )
            .Configure();
    }
}