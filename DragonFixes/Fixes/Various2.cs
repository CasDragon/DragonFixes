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
        public static void GiveAivuSwarmPoints()
        {
            Main.log.Log("Giving Aivu a value for swarm size");
            AnswerConfigurator.For("f85e4e6aee1ae964da765e705bbfbe95")
                .ModifyOnSelect(s => s.Actions = [.. s.Actions, ActionsBuilder.New().IncreaseSwarmThatWalksStrength(ContextValues.Constant(20)).Build().Actions[0]])
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
        public static void PatchScapeGoat()
        {
            Main.log.Log("Patching Scapegoat to work on allies?");
            DragonHelpers.RemoveComponent<AbilityTargetHasFact>(AbilityRefs.ScapegoatAbilityAlly.Reference.Get());
            DragonHelpers.RemoveComponent<AbilityTargetHasFact>(AbilityRefs.ScapegoatAbilityAllyPet.Reference.Get());
        }
    }
}
