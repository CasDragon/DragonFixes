using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;

namespace DragonFixes.Fixes.Items;

public class ManglingFrenzy
{
    [DragonConfigure]
    public static void PatchFeature()
    {
        Main.log.Log("Patching ManglingFrenzyFeature to include more rages");
        var buff = BuffRefs.ManglingFrenzyBuff.ToString();
        FeatureConfigurator.For(FeatureRefs.ManglingFrenzyFeature)
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
                checkedBuff: BuffRefs.BloodragerStandartRageBuff.ToString(),
                extraEffectBuff: buff
            )
            .Configure();
    }
}