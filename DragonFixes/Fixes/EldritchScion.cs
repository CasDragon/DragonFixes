using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Abilities;
using DragonFixes.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using Kingmaker.RuleSystem;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System.Linq;
using System.Security.AccessControl;
using Kingmaker.UnitLogic.Abilities.Blueprints;

namespace DragonFixes.Fixes
{
    internal class EldritchScion
    {
        [DragonFix]
        public static void AddArcaneAccuracy()
        {
            string buffname = "ESArcaneAccuracy";
            if (Settings.GetSetting<bool>("esarcaneaccuracy"))
            {
                Main.log.Log("Adding new ES Arance Accuray buff");
                BlueprintBuff newbuff = BuffConfigurator.New(buffname, Guids.ESAccurayBuff)
                    .CopyFrom(BuffRefs.ArcaneAccuracyBuff, c=> c is not AddContextStatBonus)
                    .AddContextStatBonus(stat: StatType.AdditionalAttackBonus,
                        descriptor: ModifierDescriptor.Insight,
                        multiplier: 1,
                        minimal: 0,
                        value: new ContextValue
                        {
                            ValueType = ContextValueType.TargetProperty,
                            Value = 2,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.StatBonus,
                            Property = UnitProperty.StatBonusCharisma,
                            m_AbilityParameter = AbilityParameterType.Level,
                            PropertyName = ContextPropertyName.Value1
                        })
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.EldritchArcaneAccuracyAbility)
                    .EditComponent<AbilityEffectRunAction>(a => a.Actions.Actions
                                .OfType<ContextActionApplyBuff>()
                                .First()
                                .m_Buff = newbuff.ToReference<BlueprintBuffReference>())
                    .Configure();
            }
            else
            {
                Main.log.Log("ES Arcane Accurry change disabled, creating dummy buff");
                BuffConfigurator.New(buffname, Guids.ESAccurayBuff)
                    .Configure();
            }
        }
        [DragonFix]
        public static void PatchPrescientDuration()
        {
            if (Settings.GetSetting<bool>("esprescientduration"))
            {
                Main.log.Log("Patching ES Prescient duration");
                BlueprintAbility bp = AbilityRefs.EldritchPrescientAttackAbility.Reference.Get();
                bp.GetComponent<AbilityEffectRunAction>()
                    .Actions.Actions
                    .OfType<ContextActionApplyBuff>()
                    .First()
                    .DurationValue
                    .BonusValue
                    .Value = 2;
            }
            else
            {
                Main.log.Log("ES Prescient patch disabled, skipping.");
            }
        }
        [DragonFix]
        public static void AddESExtraArcanaSelection()
        {
            if (Settings.GetSetting<bool>("esextraarcanaselection"))
            {
                Main.log.Log("Patching ES Arcanas with prereq for ExtraArcanaSelection");
                var ESAA = FeatureConfigurator.For(FeatureRefs.EldritchArcaneAccuracyFeature)
                    .AddPrerequisiteArchetypeLevel(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get(),
                        level: 1)
                    .Configure();
                var ESBB = FeatureConfigurator.For(FeatureRefs.EldritchBaneBladeFeature)
                    .AddPrerequisiteArchetypeLevel(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get(),
                        level: 15)
                    .Configure();
                var ESDB = FeatureConfigurator.For(FeatureRefs.EldritchDevotedBladeFeature)
                    .AddPrerequisiteArchetypeLevel(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get(),
                        level: 12)
                    .Configure();
                var ESDS = FeatureConfigurator.For(FeatureRefs.EldritchDimensionStrikeFeature)
                    .AddPrerequisiteArchetypeLevel(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get(),
                        level: 9)
                    .Configure();
                var ESEB = FeatureConfigurator.For(FeatureRefs.EldritchEnduringBladeFeature)
                    .AddPrerequisiteArchetypeLevel(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get(),
                        level: 6)
                    .Configure();
                var ESGB = FeatureConfigurator.For(FeatureRefs.EldritchGhostBladeFeature)
                    .AddPrerequisiteArchetypeLevel(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get(),
                        level: 9)
                    .Configure();
                var ESHA = FeatureConfigurator.For(FeatureRefs.EldritchHastedAssaultFeature)
                    .AddPrerequisiteArchetypeLevel(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get(),
                        level: 9)
                    .Configure();
                var ESPA = FeatureConfigurator.For(FeatureRefs.EldritchPrescientAttackFeature)
                    .AddPrerequisiteArchetypeLevel(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get(),
                        level: 6)
                    .Configure();
                var ESWM = FeatureConfigurator.For(FeatureRefs.EldritchWandMastery)
                    .AddPrerequisiteArchetypeLevel(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get(),
                        level: 1)
                    .Configure();
                var ESWW = FeatureConfigurator.For(FeatureRefs.EldritchWandWielder)
                    .AddPrerequisiteArchetypeLevel(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get(),
                        level: 1)
                    .Configure();
                FeatureConfigurator.For(FeatureRefs.ArcaneAccuracyFeature)
                    .AddPrerequisiteNoArchetype(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get())
                    .Configure();
                FeatureConfigurator.For(FeatureRefs.BaneBladeFeature)
                    .AddPrerequisiteNoArchetype(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get())
                    .Configure();
                FeatureConfigurator.For(FeatureRefs.DevotedBladeFeature)
                    .AddPrerequisiteNoArchetype(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get())
                    .Configure();
                FeatureConfigurator.For(FeatureRefs.DimensionStrikeFeature)
                    .AddPrerequisiteNoArchetype(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get())
                    .Configure();
                FeatureConfigurator.For(FeatureRefs.EnduringBladeFeature)
                    .AddPrerequisiteNoArchetype(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get())
                    .Configure();
                FeatureConfigurator.For(FeatureRefs.GhostBladeFeature)
                    .AddPrerequisiteNoArchetype(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get())
                    .Configure();
                FeatureConfigurator.For(FeatureRefs.HastedAssaultFeature)
                    .AddPrerequisiteNoArchetype(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get())
                    .Configure();
                FeatureConfigurator.For(FeatureRefs.PrescientAttackFeature)
                    .AddPrerequisiteNoArchetype(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get())
                    .Configure();
                FeatureConfigurator.For(FeatureRefs.WandMastery)
                    .AddPrerequisiteNoArchetype(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get())
                    .Configure();
                FeatureConfigurator.For(FeatureRefs.WandWielder)
                    .AddPrerequisiteNoArchetype(archetype: ArchetypeRefs.EldritchScionArchetype.Reference.Get(),
                        characterClass: CharacterClassRefs.MagusClass.Reference.Get())
                    .Configure();
                Main.log.Log("Patching ExtraArcana selection for ES");
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.ExtraArcanaSelection)
                    .AddToAllFeatures([ESAA, ESBB, ESDB, ESDS, ESEB, ESGB, ESHA, ESPA, ESWM, ESWW])
                    .Configure();
            }
            else
            {
                Main.log.Log("ES Arcana patch disabled, skipping.");
            }
        }
    }
}
