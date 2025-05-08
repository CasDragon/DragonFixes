using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using DragonFixes.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Utility;
using System.Linq;

namespace DragonFixes.Fixes
{
    internal class Various
    {
        [DragonFix]
        public static void PatchAbundantArcanePool()
        {
            if (Settings.GetSetting<bool>("abundantarcanepool"))
            {
                Main.log.Log("Patching Abundant Arcane Pool for Spell Dancer");
                FeatureConfigurator.For(FeatureRefs.AbundantArcanePool)
                    .AddPrerequisiteFeature(FeatureRefs.SpellDanceFeature.Reference.Get())
                    .Configure();
            }
        }
        [DragonFix]
        public static void PatchMartialProf()
        {
            Main.log.Log("Patching MartialProf to add Spiked Shields, owlcat plz");
            FeatureConfigurator.For(FeatureRefs.MartialWeaponProficiency)
                .AddProficiencies(weaponProficiencies: [WeaponCategory.WeaponLightShield, WeaponCategory.SpikedHeavyShield,
                                                        WeaponCategory.WeaponHeavyShield, WeaponCategory.SpikedLightShield])
                .Configure();
            FeatureConfigurator.For(FeatureRefs.ShieldBashFeature)
                .RemoveComponents(c => c is PrerequisiteNotProficient)
                .Configure();
        }
        [DragonFix]
        public static void PatchWyrmShifterRedBreath()
        {
            Main.log.Log("Patching Wyrm Shifter's level 20 breath to correctly be fire damage instead of cold");
            AbilityConfigurator.For(AbilityRefs.FinalWyrmshifterRedBreathWeaponAbility)
                .EditComponent<AbilityEffectRunAction>(c => c.Actions.Actions
                        .OfType<ContextActionDealDamage>()
                        .First()
                        .DamageType
                        .Energy = Kingmaker.Enums.Damage.DamageEnergyType.Fire)
                .Configure();
        }
        [DragonFix]
        public static void PatchBestialRags()
        {
            Main.log.Log("Patching bestial rags");
            BuffConfigurator.For(BuffRefs.BestialRagsBuff)
                .RemoveComponents(c => c is SpellDescriptorComponent)
                .Configure();
        }
        [DragonFix]
        public static void PatchInspiringCommand()
        {
            Main.log.Log("Patching Inspiring Command");
            AbilityConfigurator.For(AbilityRefs.NobilityDomainBaseAbility)
                .SetType(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityType.Supernatural)
                .Configure();
            AbilityConfigurator.For(AbilityRefs.NobilityDomainBaseAbilitySeparatist)
                .SetType(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityType.Supernatural)
                .Configure();
        }
        [DragonFix]
        public static void PatchNeophyteGloves()
        {
            Main.log.Log("Patching the Gloves of the Neophyte to add the missing spells");
            FeatureConfigurator.For(FeatureRefs.GlovesOfNeophyteFeature)
                .EditComponent<DiceDamageBonusOnSpell>(c => dothisthing(c))
                .Configure();
        }

        public static void dothisthing(DiceDamageBonusOnSpell spell)
        {
            spell.m_Spells.Append(AbilityRefs.ShockingGraspEffect.Reference.Get().ToReference<BlueprintAbilityReference>());
            spell.m_Spells.Append(AbilityRefs.IncendiaryRunes.Reference.Get().ToReference<BlueprintAbilityReference>());
        }
        [DragonFix]
        public static void PatchAspectofAsp()
        {
            Main.log.Log("Patching Aspect of Asep enchant to work");
            FeatureConfigurator.For(FeatureRefs.AspectOfTheAspFeature)
                .EditComponent<AdditionalDiceOnAttack>(c =>
                            c.AttackType = AdditionalDiceOnAttack.WeaponOptions.AllAttacks)
                .Configure();
        }
        [DragonFix]
        public static void PatchJoyfulRapture()
        {
            Main.log.Log("Patching Joyful Rapture to correctly dispel Negative Emotion instead of petrified");
            AbilityConfigurator.For(AbilityRefs.JoyfulRapture)
                .EditComponent<AbilityEffectRunAction>(c => c.Actions.Actions
                            .OfType<ContextActionDispelMagic>()
                            .First()
                            .Descriptor = SpellDescriptor.NegativeEmotion)
                .Configure();
        }
        [DragonFix]
        public static void PatchGnawingHunger()
        {
            Main.log.Log("Patching Gnawing Hunger to actually apply debuff to enemy?");
            FeatureConfigurator.For(FeatureRefs.GnawingMagicFeature)
                .RemoveComponents(c => c is AddAbilityUseTrigger)
                .AddAbilityUseTrigger(action:
                    ActionsBuilder.New().ApplyBuff(BuffRefs.GnawingMagicBuffEnemy.Reference.Get(),
                            new ContextDurationValue()
                            {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = ContextValues.Constant(0),
                                BonusValue = ContextValues.Constant(3)
                            }, asChild: true, toCaster: false)
                        .ApplyBuff(BuffRefs.GnawingMagicBuffSelf.Reference.Get(),
                            new ContextDurationValue()
                            {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = ContextValues.Constant(0),
                                BonusValue = ContextValues.Constant(3)
                            }, asChild: true, toCaster: true), 
                        actionsOnTarget: true,
                        checkAbilityType: true,
                        type: AbilityType.Spell)
                .Configure();
        }
        [DragonFix]
        public static void PatchAbruptEndEnchant()
        {
            Main.log.Log("Patching Abrupt End to actually work?");
            BlueprintBuff buff = BuffConfigurator.New("abruptendbuff", Guids.EbruptEndBuff)
                .SetDisplayName("abruptendbuff.name")
                .SetDescription("abruptendbuff.description")
                .AddContextStatBonus(StatType.AdditionalAttackBonus, ContextValues.Constant(2), ModifierDescriptor.Insight)
                .AddRemoveBuffOnAttack()
                .Configure();
            BlueprintWeaponEnchantment enchant = WeaponEnchantmentConfigurator.For(WeaponEnchantmentRefs.AbruptEndEnchantment)
                .AddInitiatorAttackWithWeaponTrigger(onlyHit: true, action:
                    ActionsBuilder.New()
                            .ApplyBuff(buff, new ContextDurationValue()
                            {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = ContextValues.Constant(0),
                                BonusValue = ContextValues.Constant(1)
                            },
                            asChild: true, toCaster: true), triggerBeforeAttack: true)
                .Configure();
        }
    }
}
