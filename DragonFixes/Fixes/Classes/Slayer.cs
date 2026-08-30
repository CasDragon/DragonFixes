using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;

namespace DragonFixes.Fixes.Classes
{
    internal class Slayer
    {
        [DragonConfigure]
        public static void PatchSlayerTalents()
        {
            Main.log.Log("Adding PetrifyingStrike to Slayer Talents");
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.SlayerTalentSelection2)
                .AddToAllFeatures(FeatureRefs.PetrifyingStrike.ToString())
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.SlayerTalentSelection6)
                .AddToAllFeatures(FeatureRefs.PetrifyingStrike.ToString())
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.SlayerTalentSelection10)
                .AddToAllFeatures(FeatureRefs.PetrifyingStrike.ToString())
                .Configure();
        }
        [DragonConfigure]
        public static void PatchBiteofthevampireffect()
        {
            Main.log.Log("Patching Biteofthevampireffect to work on Sneak Attacks");
            FeatureConfigurator.For(FeatureRefs.Biteofthevampireffect)
                .EditComponent<AddInitiatorAttackWithWeaponTrigger>(c => c.NotSneakAttack = false)
                .Configure();
        }

        [DragonConfigure]
        public static void PatchIroriFeature()
        {
            Main.log.Log("Patching IroriFeature to include SlayerClass for Deliverer.");
            FeatureConfigurator.For(FeatureRefs.IroriFeature)
                .EditComponent<AddFeatureOnClassLevel>(c => c.m_AdditionalClasses = [.. c.m_AdditionalClasses, 
                    CharacterClassRefs.SlayerClass.Reference.Get().ToReference<BlueprintCharacterClassReference>()])
                .Configure();
        }

        [DragonConfigure]
        public static void PatchStudyTarget()
        {
            Main.log.Log("Patching SlayerStudyTargetBuff to correctly use AND logic");
            BuffConfigurator.For(BuffRefs.SlayerStudyTargetBuff)
                .EditComponent<AddFactContextActions>(c => c.Activated.Actions
                        .OfType<Conditional>()
                        .First(x => x.ConditionsChecker.Conditions
                            .OfType<ContextConditionCasterHasFact>()
                            .First()
                            .m_Fact.deserializedGuid == FeatureRefs.ExecutionerFocusedKiller.Reference.deserializedGuid)
                        .IfTrue.Actions
                        .OfType<Conditional>()
                        .First()
                        .ConditionsChecker.Operation = Operation.And
                )
                .Configure();
        }
    }
}
