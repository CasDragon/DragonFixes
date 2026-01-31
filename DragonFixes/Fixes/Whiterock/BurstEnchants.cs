using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonFixes.Fixes.Whiterock
{
    internal class BurstEnchants
    {
        [DragonConfigure]
        public static void PatchBurstEnchants()
        {
            Main.log.Log("Patching Burst Enchants to have approriate 1d6 and also correct values.");
            // WeaponBondFlamingBurstEnchant
            BlueprintWeaponEnchantment bond = WeaponEnchantmentRefs.WeaponBondFlamingBurstEnchant.Reference.Get();
            DragonHelpers.RemoveComponent<WeaponEnergyDamageDice>(bond);
            // ElderIcyBurst
            WeaponEnchantmentConfigurator.For(WeaponEnchantmentRefs.ElderIcyBurst)
                .AddWeaponEnergyDamageDice(element: DamageEnergyType.Cold,
                    energyDamageDice: new DiceFormula()
                    {
                        m_Dice = DiceType.D6,
                        m_Rolls = 1
                    })
                .Configure();
            // CorrosiveBurst
            WeaponEnchantmentConfigurator.For(WeaponEnchantmentRefs.CorrosiveBurst)
                .AddWeaponEnergyDamageDice(element: DamageEnergyType.Acid,
                    energyDamageDice: new DiceFormula()
                    {
                        m_Dice = DiceType.D6,
                        m_Rolls = 1
                    })
                .Configure();
            // ThunderingBurst
            WeaponEnchantmentConfigurator.For(WeaponEnchantmentRefs.ThunderingBurst)
                .AddWeaponEnergyDamageDice(element: DamageEnergyType.Sonic,
                    energyDamageDice: new DiceFormula()
                    {
                        m_Dice = DiceType.D6,
                        m_Rolls = 1
                    })
                .EditComponent<WeaponEnergyBurst>(c => c.Dice = DiceType.D10)
                .Configure();
            // ElderFlamingBurst
            WeaponEnchantmentConfigurator.For(WeaponEnchantmentRefs.ElderFlamingBurst)
                .AddWeaponEnergyDamageDice(element: DamageEnergyType.Fire,
                    energyDamageDice: new DiceFormula()
                    {
                        m_Dice = DiceType.D6,
                        m_Rolls = 1
                    })
                .Configure();
            // ElderShockingBurst
            WeaponEnchantmentConfigurator.For(WeaponEnchantmentRefs.ElderShockingBurst)
                .AddWeaponEnergyDamageDice(element: DamageEnergyType.Electricity,
                    energyDamageDice: new DiceFormula()
                    {
                        m_Dice = DiceType.D6,
                        m_Rolls = 1
                    })
                .Configure();
            // ElderCorrosiveBurst
            WeaponEnchantmentConfigurator.For(WeaponEnchantmentRefs.ElderCorrosiveBurst)
                .AddWeaponEnergyDamageDice(element: DamageEnergyType.Acid,
                    energyDamageDice: new DiceFormula()
                    {
                        m_Dice = DiceType.D6,
                        m_Rolls = 1
                    })
                .Configure();
            // HellknightFLamingBurstBuff
            BuffConfigurator.For(BuffRefs.HellknightFLamingBurstBuff)
                .RemoveComponents(c => c is BuffEnchantAnyWeapon d && d.m_EnchantmentBlueprint.guid == WeaponEnchantmentRefs.Flaming.Reference.guid)
                .Configure();
            // BloodragerInfernalHellfireStrikeBuff2
            BuffConfigurator.For(BuffRefs.BloodragerInfernalHellfireStrikeBuff2)
                .RemoveComponents(c => c is BuffEnchantAnyWeapon d && d.m_EnchantmentBlueprint.guid == WeaponEnchantmentRefs.Flaming.Reference.guid)
                .Configure();
            // SorcerousClawsFlamingBurstBuff
            BuffConfigurator.For(BuffRefs.SorcerousClawsFlamingBurstBuff)
                .RemoveComponents(c => c is BuffEnchantAnyWeapon d && d.m_EnchantmentBlueprint.guid == WeaponEnchantmentRefs.Flaming.Reference.guid)
                .Configure();
            // SorcerousClawsCorrosiveBurstBuff
            BuffConfigurator.For(BuffRefs.SorcerousClawsCorrosiveBurstBuff)
                .RemoveComponents(c => c is BuffEnchantAnyWeapon d && d.m_EnchantmentBlueprint.guid == WeaponEnchantmentRefs.Corrosive.Reference.guid)
                .Configure();
            // SorcerousClawsIcyBurstBuff
            BuffConfigurator.For(BuffRefs.SorcerousClawsIcyBurstBuff)
                .RemoveComponents(c => c is BuffEnchantAnyWeapon d && d.m_EnchantmentBlueprint.guid == WeaponEnchantmentRefs.Frost.Reference.guid)
                .Configure();
            // SorcerousClawsShockingBurstBuff
            BuffConfigurator.For(BuffRefs.SorcerousClawsShockingBurstBuff)
                .RemoveComponents(c => c is BuffEnchantAnyWeapon d && d.m_EnchantmentBlueprint.guid == WeaponEnchantmentRefs.Shock.Reference.guid)
                .Configure();
            PatchRemoveOtherEnchants();
        }
        public static void PatchRemoveOtherEnchants()
        {
            ItemWeaponConfigurator.For(ItemWeaponRefs.BrutalDecayItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Corrosive.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.RottenDissectorItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Corrosive.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.RustedDawnItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Corrosive.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.EyeGougerItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Corrosive.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.MageSlayerItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Corrosive.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.RovagugRelicScorpionBattlaxeItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Corrosive.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.RovagugRelicScorpionBattlaxeItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Corrosive.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.RovagugRelicScorpionGreataxeItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Corrosive.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.RovagugRelicScorpionHandaxeItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Corrosive.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.SpeedCorrosiveBusrtJavelinPlus5)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Corrosive.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.SacrificialElderCorrosiveBurstSpearPlus5)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.ElderCorrosive.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.DevitalizerItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.ElderCorrosive.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.ElderFlamingBurstLightCrossbowPlus5)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.ElderFlaming.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.TidebringerItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.ElderFrost.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.CruelElderShockingBurstDwarvenWaraxePlus5)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.ElderShock.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.ViciousElderShockingBurstLightPickPlus5)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.ElderShock.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.FlamingBurstAdamantineDwarvenUrgroshPlus3)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Flaming.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.FlamingBurstAdamantineDwarvenUrgroshSecondPlus3)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Flaming.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.IgnitionItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Flaming.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.BloodBoilerItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Flaming.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.AshmakerItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Flaming.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.RapscallionItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Flaming.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.SkyScorcherItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Flaming.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.ExplosiveFervorItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Flaming.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.FierySpellWeaverItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Flaming.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.SpitefulMockeryItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Flaming.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.NemaryStigmaBardiche_MagicSealingItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Flaming.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.NemaryStigmaSlingStaff_MagicSealingItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Flaming.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.NemaryStigmaTrident_MagicSealingItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Flaming.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.AdamantineFrostBurstStarknifePlus3)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Frost.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.BaneOutsiderChaoticIcyBurstJavelinPlus1)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Frost.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.FrostEmbraceItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Frost.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.HeartOfIcebergItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Frost.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.IcyBurstKeenColdIronBastardSwordPlus3Item)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Frost.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.FrostbiteItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Frost.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.NordicWelcomeItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Frost.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.BloodFreezerItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Frost.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.HandaxeThievesMagicPoisonousItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Shock.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.AbruptEndItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Shock.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.GlaringThunderItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Shock.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.PocketLightningItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Shock.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.IndomitablePunisherItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Shock.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.HeavyCrossbowOfDegradationItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Thundering.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.ScreamOfPainItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Thundering.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.ThunderingBurstDualswordPlus3Item)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Thundering.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.StormcallerItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Thundering.Reference.Get())
                .Configure();

            ItemWeaponConfigurator.For(ItemWeaponRefs.OakOfThunderItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Thundering.Reference.Get())
                .Configure();
        }
    }
}
