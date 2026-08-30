using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace DragonFixes.Fixes.Classes;

public class Barbarian
{
    [DragonConfigure]
    public static void PatchCrushAndTearFeature()
    {
        Main.log.Log("Patching CrushAndTearFeature to work at level 5");
        AddFeatureOnClassLevel c = FeatureRefs.CrushAndTearFeature.Reference.Get().GetComponent<AddFeatureOnClassLevel>(com => com.Level == 5);
        c!.m_Feature = FeatureRefs.CrushAndTearFeatureLevelUp5.Reference.Get().ToReference<BlueprintFeatureReference>();
    }
    
    [DragonConfigure]
    public static void PatchConsumeFleshDragonFeature()
    {
        Main.log.Log("Patching ConsumeFleshDragonFeature*Raging to include more rages");
        var StandartFocusedRageBuff = BuffRefs.StandartFocusedRageBuff.ToString();
        var InciteRageEffectBuff = BuffRefs.InciteRageEffectBuff.ToString();
        var ElementalRampagerRampageBuff = BuffRefs.ElementalRampagerRampageBuff.ToString();
        var RageshaperDevastatingFormBuff = BuffRefs.RageshaperDevastatingFormBuff.ToString();
        var RageBuff = BuffRefs.RageBuff.ToString();
        var RageSpellBuff = BuffRefs.RageSpellBuff.ToString();
        var Gorum_Buff = BuffRefs.Gorum_Buff.ToString();
        var InspiredRageEffectBuff = BuffRefs.InspiredRageEffectBuff.ToString();
        var InspiredRageEffectBuffMythic = BuffRefs.InspiredRageEffectBuffMythic.ToString();
        Blueprint<BlueprintReference<BlueprintFeature>>[] consume = 
        [
            FeatureRefs.ConsumeFleshDragonFeatureAcidlRaging, FeatureRefs.ConsumeFleshDragonFeatureColdRaging,
            FeatureRefs.ConsumeFleshDragonFeatureElectricityRaging, FeatureRefs.ConsumeFleshDragonFeatureFireRaging,
            FeatureRefs.ConsumeFleshFeytFeatureRaging, FeatureRefs.ConsumeFleshMagicalBeastFeatureRaging,
            FeatureRefs.ConsumeFleshOutsiderFeatureChaosRaging, FeatureRefs.ConsumeFleshOutsiderFeatureEvilRaging,
            FeatureRefs.ConsumeFleshOutsiderFeatureGoodlRaging, FeatureRefs.ConsumeFleshOutsiderFeatureLawRaging,
            FeatureRefs.ConsumeFleshUndeadFeatureRaging, FeatureRefs.PowerInTheFleshFeature
        ];
        Blueprint<BlueprintReference<BlueprintBuff>>[] buffs = 
        [
            BuffRefs.ConsumeFleshDragonBuffEffectAcid, BuffRefs.ConsumeFleshDragonBuffEffectCold,
            BuffRefs.ConsumeFleshDragonBuffEffectElectricity, BuffRefs.ConsumeFleshDragonBuffEffectFire,
            BuffRefs.ConsumeFleshFeytBuffEffect, BuffRefs.ConsumeFleshMagicalBeastBuffEffect,
            BuffRefs.ConsumeFleshOutsiderBuffEffectChaos, BuffRefs.ConsumeFleshOutsiderBuffEffectEvil,
            BuffRefs.ConsumeFleshOutsiderBuffEffectGood, BuffRefs.ConsumeFleshOutsiderBuffEffectLaw,
            BuffRefs.ConsumeFleshUndeadBuffEffect, BuffRefs.PowerInTheFleshBuff
        ];
        for (var index = 0; index < consume.Length; index++)
        {
            var feature = consume[index];
            var buff = buffs[index].ToString();
            FeatureConfigurator.For(feature)
                .AddBuffExtraEffects(
                    checkedBuff: StandartFocusedRageBuff,
                    extraEffectBuff: buff
                )
                .AddBuffExtraEffects(
                    checkedBuff: InciteRageEffectBuff,
                    extraEffectBuff: buff
                )
                .AddBuffExtraEffects(
                    checkedBuff: ElementalRampagerRampageBuff,
                    extraEffectBuff: buff
                )
                .AddBuffExtraEffects(
                    checkedBuff: RageshaperDevastatingFormBuff,
                    extraEffectBuff: buff
                )
                .AddBuffExtraEffects(
                    checkedBuff: RageBuff,
                    extraEffectBuff: buff
                )
                .AddBuffExtraEffects(
                    checkedBuff: RageSpellBuff,
                    extraEffectBuff: buff
                )
                .AddBuffExtraEffects(
                    checkedBuff: Gorum_Buff,
                    extraEffectBuff: buff
                )
                .AddBuffExtraEffects(
                    checkedBuff: InspiredRageEffectBuff,
                    extraEffectBuff: buff
                )
                .AddBuffExtraEffects(
                    checkedBuff: InspiredRageEffectBuffMythic,
                    extraEffectBuff: buff
                )
                .Configure();
        }
        var unbound = BuffRefs.UnboundRageBuff.ToString();
        BuffConfigurator.For(BuffRefs.UnboundRageExtraBuff)
            .AddBuffExtraEffects(
                checkedBuff: StandartFocusedRageBuff,
                extraEffectBuff: unbound
            )
            .AddBuffExtraEffects(
                checkedBuff: InciteRageEffectBuff,
                extraEffectBuff: unbound
            )
            .AddBuffExtraEffects(
                checkedBuff: ElementalRampagerRampageBuff,
                extraEffectBuff: unbound
            )
            .AddBuffExtraEffects(
                checkedBuff: RageshaperDevastatingFormBuff,
                extraEffectBuff: unbound
            )
            .AddBuffExtraEffects(
                checkedBuff: RageBuff,
                extraEffectBuff: unbound
            )
            .AddBuffExtraEffects(
                checkedBuff: RageSpellBuff,
                extraEffectBuff: unbound
            )
            .AddBuffExtraEffects(
                checkedBuff: Gorum_Buff,
                extraEffectBuff: unbound
            )
            .AddBuffExtraEffects(
                checkedBuff: InspiredRageEffectBuff,
                extraEffectBuff: unbound
            )
            .AddBuffExtraEffects(
                checkedBuff: InspiredRageEffectBuffMythic,
                extraEffectBuff: unbound
            )
            .Configure();
    }
}