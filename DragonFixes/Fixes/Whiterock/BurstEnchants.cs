using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using TabletopTweaks.Core.Utilities;

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
            bond.RemoveComponent(bond.GetComponent<WeaponEnergyDamageDice>());
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
        }
        [DragonConfigure]
        public static void PatchMageSlayerItem()
        {
            Main.log.Log("Patching MageSlayerItem to not have Corrosive enchant");
            ItemWeaponConfigurator.For(ItemWeaponRefs.MageSlayerItem)
                .RemoveFromEnchantments(WeaponEnchantmentRefs.Corrosive.Reference.Get())
                .Configure();
        }
    }
}
