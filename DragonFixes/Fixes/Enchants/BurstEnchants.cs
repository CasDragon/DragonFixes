using System.Linq;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonFixes.Fixes.Enchants
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
            // corrosive
            var corrosive = WeaponEnchantmentRefs.Corrosive.ToString();
            Blueprint<BlueprintReference<BlueprintItemWeapon>>[] corrosiveItems = 
            [
                ItemWeaponRefs.BrutalDecayItem, ItemWeaponRefs.RottenDissectorItem, ItemWeaponRefs.RustedDawnItem,
                ItemWeaponRefs.EyeGougerItem, ItemWeaponRefs.MageSlayerItem, ItemWeaponRefs.RovagugRelicScorpionBattlaxeItem,
                ItemWeaponRefs.RovagugRelicScorpionBattlaxeItem,ItemWeaponRefs.RovagugRelicScorpionGreataxeItem,
                ItemWeaponRefs.RovagugRelicScorpionHandaxeItem, ItemWeaponRefs.SpeedCorrosiveBusrtJavelinPlus5
            ];
            foreach (var item in corrosiveItems)
            {
                var x = item.Reference.Get();
                ItemWeaponConfigurator.For(item)
                    .RemoveFromEnchantments(corrosive)
                    .Configure();
                x.m_Enchantments = [.. x.m_Enchantments.Where(e => e.deserializedGuid != corrosive)];
            }
            // elder corrosive
            var elderCorrosive = WeaponEnchantmentRefs.ElderCorrosive.ToString();
            Blueprint<BlueprintReference<BlueprintItemWeapon>>[] elderCorrosiveItems =
            [
                ItemWeaponRefs.SacrificialElderCorrosiveBurstSpearPlus5, ItemWeaponRefs.DevitalizerItem
            ];
            foreach (var item in elderCorrosiveItems)
            {
                var x = item.Reference.Get();
                ItemWeaponConfigurator.For(item)
                    .RemoveFromEnchantments(elderCorrosive)
                    .Configure();
                x.m_Enchantments = [.. x.m_Enchantments.Where(e => e.deserializedGuid != elderCorrosive)];
            }
            // elder flaming
            var elderFlaming = WeaponEnchantmentRefs.ElderFlaming.ToString();
            Blueprint<BlueprintReference<BlueprintItemWeapon>>[] elderFlamingItems =
            [
                ItemWeaponRefs.ElderFlamingBurstLightCrossbowPlus5
            ];
            foreach (var item in elderFlamingItems)
            {
                var x = item.Reference.Get();
                ItemWeaponConfigurator.For(item)
                    .RemoveFromEnchantments(elderFlaming)
                    .Configure();
                x.m_Enchantments = [.. x.m_Enchantments.Where(e => e.deserializedGuid != elderFlaming)];
            }
            // elder frost
            var elderFrost = WeaponEnchantmentRefs.ElderFrost.ToString();
            Blueprint<BlueprintReference<BlueprintItemWeapon>>[] elderFrostItems =
            [
                ItemWeaponRefs.TidebringerItem
            ];
            foreach (var item in elderFrostItems)
            {
                var x = item.Reference.Get();
                ItemWeaponConfigurator.For(item)
                    .RemoveFromEnchantments(elderFrost)
                    .Configure();
                x.m_Enchantments = [.. x.m_Enchantments.Where(e => e.deserializedGuid != elderFrost)];
            }
            // elder shock
            var elderShock = WeaponEnchantmentRefs.ElderShock.ToString();
            Blueprint<BlueprintReference<BlueprintItemWeapon>>[] elderShockItems =
            [
                ItemWeaponRefs.CruelElderShockingBurstDwarvenWaraxePlus5, ItemWeaponRefs.ViciousElderShockingBurstLightPickPlus5
            ];
            foreach (var item in elderShockItems)
            {
                var x = item.Reference.Get();
                ItemWeaponConfigurator.For(item)
                    .RemoveFromEnchantments(elderShock)
                    .Configure();
                x.m_Enchantments = [.. x.m_Enchantments.Where(e => e.deserializedGuid != elderShock)];
            }
            // flaming
            var flaming = WeaponEnchantmentRefs.Flaming.ToString();
            Blueprint<BlueprintReference<BlueprintItemWeapon>>[] flamingItems = 
            [
                ItemWeaponRefs.FlamingBurstAdamantineDwarvenUrgroshPlus3, ItemWeaponRefs.FlamingBurstAdamantineDwarvenUrgroshSecondPlus3,
                ItemWeaponRefs.IgnitionItem, ItemWeaponRefs.BloodBoilerItem, ItemWeaponRefs.AshmakerItem,
                ItemWeaponRefs.RapscallionItem, ItemWeaponRefs.SkyScorcherItem, ItemWeaponRefs.ExplosiveFervorItem,
                ItemWeaponRefs.FierySpellWeaverItem, ItemWeaponRefs.SpitefulMockeryItem,
                ItemWeaponRefs.NemaryStigmaBardiche_MagicSealingItem, ItemWeaponRefs.NemaryStigmaSlingStaff_MagicSealingItem,
                ItemWeaponRefs.NemaryStigmaTrident_MagicSealingItem
            ];
            foreach (var item in flamingItems)
            {
                var x = item.Reference.Get();
                ItemWeaponConfigurator.For(item)
                    .RemoveFromEnchantments(flaming)
                    .Configure();
                x.m_Enchantments = [.. x.m_Enchantments.Where(e => e.deserializedGuid != flaming)];
            }
            // frost
            var frost = WeaponEnchantmentRefs.Frost.ToString();
            Blueprint<BlueprintReference<BlueprintItemWeapon>>[] frostItems = 
            [
                ItemWeaponRefs.AdamantineFrostBurstStarknifePlus3, ItemWeaponRefs.BaneOutsiderChaoticIcyBurstJavelinPlus1,
                ItemWeaponRefs.FrostEmbraceItem, ItemWeaponRefs.HeartOfIcebergItem, ItemWeaponRefs.FrostbiteItem,
                ItemWeaponRefs.IcyBurstKeenColdIronBastardSwordPlus3Item, ItemWeaponRefs.NordicWelcomeItem,
                ItemWeaponRefs.BloodFreezerItem
            ];
            foreach (var item in frostItems)
            {
                var x = item.Reference.Get();
                ItemWeaponConfigurator.For(item)
                    .RemoveFromEnchantments(frost)
                    .Configure();
                x.m_Enchantments = [.. x.m_Enchantments.Where(e => e.deserializedGuid != frost)];
            }
            // shock
            var shock = WeaponEnchantmentRefs.Shock.ToString();
            Blueprint<BlueprintReference<BlueprintItemWeapon>>[] shockItems = 
            [
                ItemWeaponRefs.HandaxeThievesMagicPoisonousItem, ItemWeaponRefs.AbruptEndItem,
                ItemWeaponRefs.GlaringThunderItem, ItemWeaponRefs.PocketLightningItem, ItemWeaponRefs.IndomitablePunisherItem
            ];
            foreach (var item in shockItems)
            {
                var x = item.Reference.Get();
                ItemWeaponConfigurator.For(item)
                    .RemoveFromEnchantments(shock)
                    .Configure();
                x.m_Enchantments = [.. x.m_Enchantments.Where(e => e.deserializedGuid != shock)];
            }
            // thundering
            var thundering = WeaponEnchantmentRefs.Thundering.ToString();
            Blueprint<BlueprintReference<BlueprintItemWeapon>>[] thunderingItems = 
            [
                ItemWeaponRefs.HeavyCrossbowOfDegradationItem, ItemWeaponRefs.ScreamOfPainItem, ItemWeaponRefs.OakOfThunderItem,
                ItemWeaponRefs.ThunderingBurstDualswordPlus3Item, ItemWeaponRefs.StormcallerItem
            ];
            foreach (var item in thunderingItems)
            {
                var x = item.Reference.Get();
                ItemWeaponConfigurator.For(item)
                    .RemoveFromEnchantments(thundering)
                    .Configure();
                x.m_Enchantments = [.. x.m_Enchantments.Where(e => e.deserializedGuid != thundering)];
            }
        }
    }
}
