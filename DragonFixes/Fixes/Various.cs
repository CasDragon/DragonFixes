using System.Linq;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Armors;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using DragonFixes.Util;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Utility;

namespace DragonFixes.Fixes
{
    internal class Various
    {
        [DragonConfigure]
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
        [DragonConfigure]
        public static void PatchMartialProf()
        {
            Main.log.Log("Patching MartialProf to add Spiked Shields, owlcat plz");
            FeatureConfigurator.For(FeatureRefs.MartialWeaponProficiency)
                .AddProficiencies(weaponProficiencies: [WeaponCategory.WeaponLightShield, WeaponCategory.SpikedHeavyShield,
                                                        WeaponCategory.WeaponHeavyShield, WeaponCategory.SpikedLightShield])
                .Configure();
            BlueprintFeature bp = FeatureRefs.ShieldBashFeature.Reference.Get();
            DragonHelpers.RemoveComponent(bp, bp.GetComponent<PrerequisiteNotProficient>());
        }
        [DragonConfigure]
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
        [DragonConfigure]
        public static void PatchBestialRags()
        {
            Main.log.Log("Patching bestial rags");
            BlueprintBuff bp = BuffRefs.BestialRagsBuff.Reference.Get();
            DragonHelpers.RemoveComponent(bp, bp.GetComponent<SpellDescriptorComponent>());
        }
        [DragonConfigure]
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
        [DragonConfigure]
        public static void PatchNeophyteGloves()
        {
            Main.log.Log("Patching the Gloves of the Neophyte to add the missing spells");
            BlueprintFeature bp = FeatureRefs.GlovesOfNeophyteFeature.Reference.Get();
            DragonHelpers.RemoveComponent(bp, bp.GetComponent<DiceDamageBonusOnSpell>());
            FeatureConfigurator.For(bp)
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
        }

        [DragonConfigure]
        public static void PatchAspectofAsp()
        {
            Main.log.Log("Patching Aspect of Asep enchant to work");
            FeatureConfigurator.For(FeatureRefs.AspectOfTheAspFeature)
                .EditComponent<AdditionalDiceOnAttack>(c =>
                            c.AttackType = AdditionalDiceOnAttack.WeaponOptions.AllAttacks)
                .Configure();
        }
        [DragonConfigure]
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
        [DragonConfigure]
        public static void PatchGnawingHunger()
        {
            Main.log.Log("Patching Gnawing Hunger to actually apply debuff to enemy?");
            BlueprintFeature bp = FeatureRefs.GnawingMagicFeature.Reference.Get();
            DragonHelpers.RemoveComponent(bp, bp.GetComponent<AddAbilityUseTrigger>());
            FeatureConfigurator.For(bp)
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
        internal const string abruptendbuffname = "Abrupt End";
        internal const string abruptendbuffdescription = "+2 Insight bonus to attack rolls.";
        [DragonLocalizedString(abruptendbuffnamekey, abruptendbuffname)]
        internal const string abruptendbuffnamekey = "abruptendbuff.name";
        [DragonLocalizedString(abruptendbuffdescriptionkey, abruptendbuffdescription, true)]
        internal const string abruptendbuffdescriptionkey = "abruptendbuff.description";

        [DragonConfigure]
        public static void PatchAbruptEndEnchant()
        {
            Main.log.Log("Patching Abrupt End to actually work?");
            BlueprintBuff buff = BuffConfigurator.New("abruptendbuff", Guids.EbruptEndBuff)
                .SetDisplayName(abruptendbuffnamekey)
                .SetDescription(abruptendbuffdescriptionkey)
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
        internal const string devitalizerbuffname = "Devitalizer";
        internal const string devitalizerbuffdescription = "+2 Circumstance bonus to attack and damage rolls against Exhausted enemies.";
        [DragonLocalizedString(devitalizerbuffnamekey, devitalizerbuffname)]
        internal const string devitalizerbuffnamekey = "abruptendbuff.name";
        [DragonLocalizedString(devitalizerbuffdescriptionkey, devitalizerbuffdescription, true)]
        internal const string devitalizerbuffdescriptionkey = "abruptendbuff.description";
        [DragonConfigure]
        public static void PatchDevitalizer()
        {
            Main.log.Log("Patching Abrupt End to actually work?");
            BlueprintBuff buff = BuffConfigurator.New("devitalizerbuff", Guids.DevitalizerBuff)
                .SetDisplayName(devitalizerbuffnamekey)
                .SetDescription(devitalizerbuffdescriptionkey)
                .AddContextStatBonus(StatType.AdditionalAttackBonus, ContextValues.Constant(2), ModifierDescriptor.Circumstance)
                .AddContextStatBonus(StatType.AdditionalDamage, ContextValues.Constant(2), ModifierDescriptor.Circumstance)
                .AddRemoveBuffOnAttack()
                .Configure();
            BlueprintWeaponEnchantment enchant = WeaponEnchantmentRefs.DevitalizerEnchantment.Reference.Get();
            DragonHelpers.RemoveComponent(enchant, enchant.GetComponent<AddInitiatorAttackWithWeaponTrigger>());
            WeaponEnchantmentConfigurator.For(enchant)
                .AddInitiatorAttackWithWeaponTrigger(onlyHit: true, action:
                    ActionsBuilder.New()
                        .Conditional(ConditionsBuilder.New()
                            .HasBuffWithDescriptor(spellDescriptor: SpellDescriptor.Exhausted),
                            ifTrue:
                                ActionsBuilder.New()
                                    .ApplyBuff(buff, new ContextDurationValue()
                                    {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = ContextValues.Constant(0),
                                        BonusValue = ContextValues.Constant(1)
                                    },
                                    asChild: true, toCaster: true)),
                    triggerBeforeAttack: true)
                .Configure();
        }

        [DragonConfigure]
        public static void PatchFreeRein()
        {
            if (ModCompat.tttbase && TTTSettingChecker.CheckStaggerRemoval())
            {
                Main.log.Log("TTT installed and stagger setting is enabled, disabling Free Rein fix");
                return;
            }
            Main.log.Log("Patching Free Rein and Freest Rein");
            BlueprintBuff buff = BuffConfigurator.For(BuffRefs.BootsOfFreereinBuff)
                .AddBuffDescriptorImmunity(false, SpellDescriptor.Staggered)
                .Configure();
            BlueprintFeature bp = FeatureRefs.BootsOfFreestReinFeature.Reference.Get();
            DragonHelpers.RemoveComponent(bp, bp.GetComponent<AddFactContextActions>());
            FeatureConfigurator.For(bp)
                .AddFactContextActions(activated: ActionsBuilder.New()
                            .ApplyBuffPermanent(buff, true),
                        deactivated: ActionsBuilder.New()
                            .RemoveBuff(buff))
                .Configure();
        }
        [DragonConfigure]
        public static void PatchFighterFinessDamageFeature()
        {
            Main.log.Log("Patching FighterFinessDamageFeature to be correctly removed upon respec");
            FeatureConfigurator.For(FeatureRefs.FighterFinessDamageFeature)
                .SetIsClassFeature(true)
                .Configure();
        }

        [DragonConfigure]
        public static void PatchTrueSeeingCast()
        {
            Main.log.Log("Patching TrueSeeingCast to allow for Extend metamagic.");
            AbilityConfigurator.For(AbilityRefs.TrueSeeingCast)
                .AddToAvailableMetamagic(Kingmaker.UnitLogic.Abilities.Metamagic.Extend)
                .Configure();
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
        [DragonConfigure]
        public static void PatchDeadlyFascination()
        {
            Main.log.Log("Patching MantisZealotDeadlyFascinationAbility to include MindEffecting descriptor.");
            AbilityConfigurator.For(AbilityRefs.MantisZealotDeadlyFascinationAbility)
                .SetSpellDescriptor(SpellDescriptor.MindAffecting | SpellDescriptor.Charm | SpellDescriptor.Daze)
                .Configure();
        }
        [DragonConfigure]
        public static void PatchTieflingHeritageDemodand()
        {
            Main.log.Log("Patching TieflingHeritageDemodand to remove AND condition.");
            FeatureConfigurator.For(FeatureRefs.TieflingHeritageDemodand)
                .EditComponent<AttackBonusConditional>(c => c.Conditions.Operation = Kingmaker.ElementsSystem.Operation.Or)
                .Configure();
        }
        [DragonConfigure]
        public static void PatchCursedArmor()
        {
            Main.log.Log("Patching cursed armor");
            BuffConfigurator.For(BuffRefs.CursedDelameresArmorBuff)
                .EditComponent<ContextSetAbilityParams>(c => c.CasterLevel.Value = 7)
                .Configure();
            BuffConfigurator.For(BuffRefs.CursedDelameresBowCurse)
                .EditComponent<ContextSetAbilityParams>(c => c.CasterLevel.Value = 7)
                .Configure();
            BuffConfigurator.For(BuffRefs.MaskOfNothingBuff)
                .EditComponent<ContextSetAbilityParams>(c => c.CasterLevel.Value = 10)
                .Configure();
            BuffConfigurator.For(BuffRefs.StorytellerAreshkaMaskBuff)
                .EditComponent<ContextSetAbilityParams>(c => c.CasterLevel.Value = 10)
                .Configure();
            BuffConfigurator.For(BuffRefs.TheTyranyOfMindCurseBuff)
                .EditComponent<ContextSetAbilityParams>(c => c.CasterLevel.Value = 4)
                .Configure();
            BuffConfigurator.For(BuffRefs.WickedKukriBuff)
                .EditComponent<ContextSetAbilityParams>(c => c.CasterLevel.Value = 9)
                .Configure();
        }
        [DragonConfigure]
        public static void PatchStinkingCloud()
        {
            if (Settings.GetSetting<bool>("stinkycloud"))
            {
                Main.log.Log("Patching StinkingCloudArea to actually trigger");
                AbilityAreaEffectConfigurator.For(AbilityAreaEffectRefs.StinkingCloudArea)
                    .EditComponent<AbilityAreaEffectRunAction>(c =>
                        c.UnitEnter.Actions.OfType<Conditional>().FirstOrDefault()
                            .ConditionsChecker.Conditions.OfType<ContextConditionHasBuff>().FirstOrDefault()
                            .Not = true)
                    .Configure();
            }
        }
        [DragonConfigure]
        public static void PatchLeadBladesBuff()
        {
            Main.log.Log("Patching LeafBladeBuff to include Sawtooth Sabre.");
            BuffConfigurator.For(BuffRefs.LeafBladesBuff)
                .EditComponent<IncreaseDiceSizeOnAttack>(c => c.Categories = [.. c.Categories, WeaponCategory.SawtoothSabre])
                .Configure();
        }
        [DragonConfigure]
        public static void PatchUncannyDodgeTalent()
        {
            Main.log.Log("Removing circular logic from UncannyDodgeTalent.");
            BlueprintFeature x = FeatureRefs.UncannyDodgeTalent.Reference.Get();
            DragonHelpers.RemoveComponent<RecalculateOnFactsChange>(x);
            DragonHelpers.RemoveComponent<AddFeatureIfHasFact>(x);
            DragonHelpers.RemoveComponent<AddFeatureIfHasFact>(x);
            FeatureConfigurator.For(x)
                .AddFacts([FeatureRefs.UncannyDodgeChecker.Reference.Get()])
                .Configure();
        }
        [DragonConfigure]
        public static void PatchIroriFeature()
        {
            Main.log.Log("Patching IroriFeature to include SlayerClass for Deliverer.");
            FeatureConfigurator.For(FeatureRefs.IroriFeature)
                .EditComponent<AddFeatureOnClassLevel>(c => c.m_AdditionalClasses = [.. c.m_AdditionalClasses, CharacterClassRefs.SlayerClass.Reference.Get().ToReference<BlueprintCharacterClassReference>()])
                .Configure();
        }

        [DragonConfigure]
        public static void PatchUndeadImmunities()
        {
            Main.log.Log("Patching UndeadImmunities to include RecalculateOnStatChange component.");
            FeatureConfigurator.For(FeatureRefs.UndeadImmunities)
                .AddRecalculateOnStatChange(stat: StatType.Charisma)
                .Configure();
        }
        [DragonConfigure]
        public static void PatchConstrucImmunities()
        {
            Main.log.Log("Patching ConstructImmunities to include immunity to energy dragin component.");
            FeatureConfigurator.For(FeatureRefs.ConstructImmunities)
                .AddImmunityToEnergyDrain()
                .Configure();
        }
    }
}
