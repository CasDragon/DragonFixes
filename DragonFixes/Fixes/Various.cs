using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using DragonFixes.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
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
                    .AddPrerequisiteFeature(FeatureRefs.SpellDanceFeature.Reference.Get(), false, Prerequisite.GroupType.Any, false)
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
            BlueprintFeature bp = FeatureRefs.ShieldBashFeature.Reference.Get();
            LibraryStuff.RemoveComponent(bp, bp.GetComponent<PrerequisiteNotProficient>());
            /*FeatureConfigurator.For(FeatureRefs.ShieldBashFeature)
                .RemoveComponents(c => c is PrerequisiteNotProficient)
                .Configure();*/
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
            BlueprintBuff bp = BuffRefs.BestialRagsBuff.Reference.Get();
            LibraryStuff.RemoveComponent(bp, bp.GetComponent<SpellDescriptorComponent>());
            /*BuffConfigurator.For(BuffRefs.BestialRagsBuff)
                .RemoveComponents(c => c is SpellDescriptorComponent)
                .Configure();*/
        }
        [DragonFix]
        public static void PatchInspiringCommand()
        {
            Main.log.Log("Patching Inspiring Command");
            AbilityConfigurator.For(AbilityRefs.NobilityDomainBaseAbility)
                .SetType(AbilityType.Supernatural)
                .Configure();
            AbilityConfigurator.For(AbilityRefs.NobilityDomainBaseAbilitySeparatist)
                .SetType(AbilityType.Supernatural)
                .Configure();
        }
        [DragonFix]
        public static void PatchNeophyteGloves()
        {
            Main.log.Log("Patching the Gloves of the Neophyte to add the missing spells");
            BlueprintFeature bp = FeatureConfigurator.For(FeatureRefs.GlovesOfNeophyteFeature)
                //.RemoveComponents(c => c is DiceDamageBonusOnSpell)
                .AddDiceDamageBonusOnSpell(spells: [
                    AbilityRefs.ShockingGraspEffect.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.IncendiaryRunes.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.AcidSplash.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.Jolt.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.RayOfFrost.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.BurningHands.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.CorrosiveTouch.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.CureLightWoundsDamage.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.EarPiercingScream.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.FirebellyAbility.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.MagicMissile.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.Snowball.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.DivineZap.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.Ignition.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.InflictLightWoundsDamage.Reference.Get().ToReference<BlueprintAbilityReference>()
                    ], 
                    mergeBehavior: BlueprintCore.Blueprints.CustomConfigurators.ComponentMerge.Replace)
                .Configure();
            LibraryStuff.RemoveComponent(bp, bp.GetComponent<DiceDamageBonusOnSpell>());
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
            BlueprintFeature bp = FeatureConfigurator.For(FeatureRefs.GnawingMagicFeature)
               // .RemoveComponents(c => c is AddAbilityUseTrigger)
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
            LibraryStuff.RemoveComponent(bp, bp.GetComponent<AddAbilityUseTrigger>());
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

        [DragonFix]
        public static void PatchFreeRein()
        {
            Main.log.Log("Patching Free Rein and Freest Rein");
            BlueprintBuff buff = BuffConfigurator.For(BuffRefs.BootsOfFreereinBuff)
                .AddBuffDescriptorImmunity(false, SpellDescriptor.Staggered)
                .Configure();
            BlueprintFeature bp = FeatureConfigurator.For(FeatureRefs.BootsOfFreestReinFeature)
                //.RemoveComponents(c => c is AddFactContextActions)
                .AddFactContextActions(activated: ActionsBuilder.New()
                            .ApplyBuffPermanent(buff, true),
                        deactivated: ActionsBuilder.New()
                            .RemoveBuff(buff))
                .Configure();
            LibraryStuff.RemoveComponent(bp, bp.GetComponent<AddFactContextActions>());
        }
        [DragonFix]
        public static void PatchFighterFinessDamageFeature()
        {
            Main.log.Log("Patching FighterFinessDamageFeature to be correctly removed upon respec");
            FeatureConfigurator.For(FeatureRefs.FighterFinessDamageFeature)
                .SetIsClassFeature(true)
                .Configure();
        }
    }
}
