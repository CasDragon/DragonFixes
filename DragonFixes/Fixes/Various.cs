using System.Linq;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.DialogSystem;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes
{
    internal class Various3
    {
        [DragonConfigure]
        public static void PatchAbundantArcanePool()
        {
            Main.log.Log("Patching Abundant Arcane Pool for Spell Dancer");
            FeatureConfigurator.For(FeatureRefs.AbundantArcanePool)
                .AddPrerequisiteFeature(FeatureRefs.SpellDanceFeature.Reference.Get(), false, Prerequisite.GroupType.Any, false)
                .Configure();
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
        public static void PatchTieflingHeritageDemodand()
        {
            Main.log.Log("Patching TieflingHeritageDemodand to remove AND condition.");
            FeatureConfigurator.For(FeatureRefs.TieflingHeritageDemodand)
                .EditComponent<AttackBonusConditional>(c => c.Conditions.Operation = Kingmaker.ElementsSystem.Operation.Or)
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


        public const string breetypo =
            "The silver dragon Terendelev fell in battle — hardly surprising, as she had to fight the demon lord Deskari himself. He willed the land to part and swallow all who dared to stand in his way. But the war was still far from over.";

        [DragonLocalizedString(breetypokey, breetypo)]
        public const string breetypokey = "bree_typo.one";

        [DragonConfigure]
        public static void LocalizationNonsense()
        {
            CueConfigurator.For("0df3b5e250906534eac207b3dc5a5d07")
                .SetText(breetypokey)
                .Configure();
        }
    }
}
