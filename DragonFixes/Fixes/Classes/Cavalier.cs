using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonFixes.Util;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;

namespace DragonFixes.Fixes.Classes
{
    internal class Cavalier
    {
        [DragonConfigure]
        public static void PatchDeadlyCharge()
        {
            Main.log.Log("Patching DIscipleOfThePikeDeadlyChargeBuff to include double damage.");
            BuffRefs.DIscipleOfThePikeDeadlyChargeBuff.Reference.Get().Components = BuffRefs.CavalierSupremeChargeBuff.Reference.Get().Components;
        }

        [DragonConfigure]
        public static void PatchAbsoluteOrder()
        {
            Main.log.Log("Patching AbsoluteOrder to allow more targets.");
            BlueprintAbility approach = AbilityRefs.AbsoluteOrderApproach.Reference.Get();
            DragonHelpers.RemoveComponent<AbilityTargetHasFact>(approach);
            BlueprintAbility fall = AbilityRefs.AbsoluteOrderFall.Reference.Get();
            DragonHelpers.RemoveComponent<AbilityTargetHasFact>(fall);
            BlueprintAbility flee = AbilityRefs.AbsoluteOrderFlee.Reference.Get();
            DragonHelpers.RemoveComponent<AbilityTargetHasFact>(flee);
            BlueprintAbility halt = AbilityRefs.AbsoluteOrderHalt.Reference.Get();
            DragonHelpers.RemoveComponent<AbilityTargetHasFact>(halt);
        }
        
        // Mounted Mastery new BP stuff
        
        [DragonConfigure]
        public static void PatchMountedMastery()
        {
            Main.log.Log("Patching MountedMastery to fix stacking buff.");
            var buff = BuffConfigurator.New("mountedmasterbuff", Guids.MountedMasterBuff)
                .AddDerivativeStatBonus(StatType.AdditionalAttackBonus,
                    StatType.Strength,
                    ModifierDescriptor.UntypedStackable)
                .Configure();
            
            BlueprintFeature x = FeatureRefs.CavalierMountedMastery.Reference.Get();
            DragonHelpers.RemoveComponent<CavalierMountedMastery>(x);
            FeatureConfigurator.For(x)
                .AddBuffExtraEffects(BuffRefs.ChargeBuff.ToString(),
                    extraEffectBuff: buff)
                .Configure();
        }
    }
}
