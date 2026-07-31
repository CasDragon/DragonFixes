using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.AVEx;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.DialogSystem;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.BasicEx;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using DragonLibrary.BPCoreExtensions;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UI.MVVM._ConsoleView.InGame;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TabletopTweaks.Core.MechanicsChanges.MetamagicExtention;

namespace DragonFixes.Fixes
{
    internal class Various2
    {
        [DragonConfigure]
        public static void PatchAbrikandilu_Feature_Mutilation()
        {
            Main.log.Log("Patching Abrikandilu_Feature_Mutilation to have correct DC.");
            FeatureConfigurator.For(FeatureRefs.Abrikandilu_Feature_Mutilation)
                .EditComponent<AddInitiatorAttackWithWeaponTrigger>(c =>
                    SetCustomDC(c.Action.Actions.OfType<ContextActionSavingThrow>().FirstOrDefault(), 13))
                .Configure();
        }
        [DragonConfigure]
        public static void PatchSchir_DiseaseFeature()
        {
            Main.log.Log("Patching Schir_DiseaseFeature to have correct DC.");
            BlueprintFeature x = FeatureConfigurator.For(FeatureRefs.Schir_DiseaseFeature)
                .EditComponent<AddInitiatorAttackWithWeaponTrigger>(c =>
                    SetCustomDC(c.Action.Actions.OfType<ContextActionSavingThrow>().FirstOrDefault(), 15))
                .Configure();
            DragonHelpers.RemoveComponent<ContextCalculateAbilityParams>(x);
            DragonHelpers.RemoveComponent<RecalculateOnStatChange>(x);
        }
        public static void SetCustomDC(ContextActionSavingThrow savingThrow, int dc)
        {
            savingThrow.HasCustomDC = true;
            savingThrow.CustomDC.Value = dc;
        }
        [DragonConfigure]
        public static void GiveAivuSwarmPoints()
        {
            Main.log.Log("Giving Aivu a value for swarm size");
            AnswerConfigurator.For("f85e4e6aee1ae964da765e705bbfbe95")
                .ModifyOnSelect(s => s.Actions = [.. s.Actions, ActionsBuilder.New().IncreaseSwarmThatWalksStrength(ContextValues.Constant(20)).Build().Actions[0]])
                .Configure();
        }
        [DragonConfigure]
        public static void PatchBodyBuffs()
        {
            Main.log.Log("Patching FieryBodyBuff, IceBodyBuff, IronBodyBuff to include ImprovedUnarmedStrike");
            BuffConfigurator.For(BuffRefs.FieryBodyBuff)
                .AddMechanicsFeature(AddMechanicsFeature.MechanicsFeatureType.ImprovedUnarmedStrike)
                .Configure();
            BuffConfigurator.For(BuffRefs.IceBodyBuff)
                .AddMechanicsFeature(AddMechanicsFeature.MechanicsFeatureType.ImprovedUnarmedStrike)
                .Configure();
            BuffConfigurator.For(BuffRefs.IronBodyBuff)
                .AddMechanicsFeature(AddMechanicsFeature.MechanicsFeatureType.ImprovedUnarmedStrike)
                .Configure();
        }
        [DragonConfigure]
        public static void PatchCrushAndTearFeature()
        {
            Main.log.Log("Patching CrushAndTearFeature to work at level 5");
            AddFeatureOnClassLevel c = FeatureRefs.CrushAndTearFeature.Reference.Get().GetComponent<AddFeatureOnClassLevel>(com => com.Level == 5);
            c.m_Feature = FeatureRefs.CrushAndTearFeatureLevelUp5.Reference.Get().ToReference<BlueprintFeatureReference>();
        }
        [DragonConfigure]
        public static void PatchCriticalMastery()
        {
            Main.log.Log("Patching CriticalMastery to include Bleeding/Flaying Critical");
            var x = FeatureRefs.CriticalMastery.Reference.Get().GetComponent<PrerequisiteFeaturesFromList>();
            x.m_Features = [.. x.m_Features, FeatureRefs.FlayingCriticalFeature.Reference.Get().ToReference<BlueprintFeatureReference>(),
                FeatureRefs.BleedingCriticalFeature.Reference.Get().ToReference<BlueprintFeatureReference>()];
        }
        [DragonConfigure]
        public static void PatchCreatePitArea()
        {
            Main.log.Log("Patching CreatePitArea to include more Wings features");
            var x = AbilityAreaEffectRefs.CreatePitArea.Reference.Get().GetComponent<AreaEffectPit>();
            x.m_EffectsImmunityFacts = [
                .. x.m_EffectsImmunityFacts, 
                FeatureRefs.ShifterGriffonWingsFeature.Reference.Get().ToReference<BlueprintUnitFactReference>(),
                FeatureRefs.ShifterFeyWingsFeature.Reference.Get().ToReference<BlueprintUnitFactReference>(),
                FeatureRefs.ShifterAspectFiendWingsFeature.Reference.Get().ToReference<BlueprintUnitFactReference>(),
                BuffRefs.ShifterWildShapeGriffonBuff.Reference.Get().ToReference<BlueprintUnitFactReference>(),
                BuffRefs.ShifterWildShapeGriffonBuff9.Reference.Get().ToReference<BlueprintUnitFactReference>(),
                BuffRefs.ShifterWildShapeGriffonBuff14.Reference.Get().ToReference<BlueprintUnitFactReference>(),
                BuffRefs.ShifterWildShapeGriffonGodBuff.Reference.Get().ToReference<BlueprintUnitFactReference>(),
                BuffRefs.ShifterWildShapeGriffonGodBuff9.Reference.Get().ToReference<BlueprintUnitFactReference>(),
                BuffRefs.ShifterWildShapeGriffonGodBuff14.Reference.Get().ToReference<BlueprintUnitFactReference>(),
                BuffRefs.ShifterWildShapeGriffonDemonBuff.Reference.Get().ToReference<BlueprintUnitFactReference>(),
                BuffRefs.ShifterWildShapeGriffonDemonBuff9.Reference.Get().ToReference<BlueprintUnitFactReference>(),
                BuffRefs.ShifterWildShapeGriffonDemonBuff14.Reference.Get().ToReference<BlueprintUnitFactReference>()
            ];
        }
        [DragonConfigure]
        public static void PatchBurningEntangleArea()
        {
            Main.log.Log("Patching BurningEntangleArea to not damage on succeeding save");
            Conditional x = (Conditional)AbilityAreaEffectRefs.BurningEntangleArea.Reference.Get().GetComponent<AbilityAreaEffectRunAction>()
                .Round.Actions[0];
            ContextActionSavingThrow y = (ContextActionSavingThrow)x.IfFalse.Actions[0];
            var z = y.Actions.Actions[1];
            ContextActionConditionalSaved x1 = (ContextActionConditionalSaved)y.Actions.Actions[0];
            x1.Failed.Actions = [.. x1.Failed.Actions, z];
            x.IfFalse.Actions = [x1];
        }
        [DragonConfigure]
        public static void PatchScapeGoat()
        {
            Main.log.Log("Patching Scapegoat to work on allies?");
            DragonHelpers.RemoveComponent<AbilityTargetHasFact>(AbilityRefs.ScapegoatAbilityAlly.Reference.Get());
            DragonHelpers.RemoveComponent<AbilityTargetHasFact>(AbilityRefs.ScapegoatAbilityAllyPet.Reference.Get());
        }
    }
}
