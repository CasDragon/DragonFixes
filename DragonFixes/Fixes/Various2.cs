using BlueprintCore.Actions.Builder;
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
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.UI.MVVM._ConsoleView.InGame;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes
{
    internal class Various2
    {
        [DragonConfigure]
        public static void PatchAnimateDead()
        {
            bool isDC = ModCompat.IsModEnabled("DarkCodex");
            if (!isDC)
            {
                Main.log.Log("Patching AnimateDead (and Lesser) to include NecromancersStaffFeature buff");
                AbilityConfigurator.For(AbilityRefs.AnimateDead)
                    .EditComponent<AbilityEffectRunAction>(c => dothing(c))
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.AnimateDeadLesser)
                    .EditComponent<AbilityEffectRunAction>(c => dothing(c))
                    .Configure();
            }
        }

        public static void dothing(AbilityEffectRunAction action)
        {
            ContextActionSpawnMonster mob = action.Actions.Actions.OfType<ContextActionSpawnMonster>().FirstOrDefault();
            ConditionsChecker x = ConditionsBuilder.New()
                .CasterHasFact(FeatureRefs.NecromancersStaffFeature.Reference.Get()).Build();
            ActionList y = ActionsBuilder.New()
                .Conditional(x,
                    ifTrue: ActionsBuilder.New()
                        .ApplyBuffPermanentFixed(BuffRefs.NecromancersStaffBuff.Reference.Get(), asChild: true)).Build();
            mob.AfterSpawn.Actions = [ .. mob.AfterSpawn.Actions , y.Actions[0] ];
        }
        [DragonConfigure]
        public static void PatchThousandBitesBuff()
        {
            Main.log.Log("Patching ThousandBitesBuff to correctly buff CorruptedDragonForm");
            BuffConfigurator.For(BuffRefs.ThousandBitesBuff)
                .AddBuffExtraEffects(checkedBuff: BuffRefs.CorruptedDragonFormBuff.Reference.Get(), 
                        extraEffectBuff: BuffRefs.ThousandBitesBuffEffect.Reference.Get())
                .Configure();
        }
        [DragonConfigure]
        public static void PatchSpindleInfusion()
        {
            Main.log.Log("Patching Spindle / Exploding Arrows infusions to use InfusionBurnCost instead of BlastBurnCost.");
            Blueprint<BlueprintReference<BlueprintAbility>>[] abilities = [AbilityRefs.SpindleAirBlastAbility, AbilityRefs.SpindleBlizzardBlastAbility,
                AbilityRefs.SpindleBloodBlastAbility, AbilityRefs.SpindleBlueFlameBlastAbility, AbilityRefs.SpindleChargedWaterBlastAbility,
                AbilityRefs.SpindleColdBlastAbility, AbilityRefs.SpindleEarthBlastAbility, AbilityRefs.SpindleElectricBlastAbility,
                AbilityRefs.SpindleFireBlastAbility, AbilityRefs.SpindleIceBlastAbility, AbilityRefs.SpindleMagmaBlastAbility,
                AbilityRefs.SpindleMetalBlastAbility, AbilityRefs.SpindleMudBlastAbility, AbilityRefs.SpindlePlasmaBlastAbility,
                AbilityRefs.SpindleSandstormBlastAbility, AbilityRefs.SpindleSteamBlastAbility, AbilityRefs.SpindleThunderstormBlastAbility,
                AbilityRefs.SpindleWaterBlastAbility, AbilityRefs.ExplodingArrowsAirBlastAbility, AbilityRefs.ExplodingArrowsBlizzardBlastAbility,
                AbilityRefs.ExplodingArrowsBlueFlameBlastAbility, AbilityRefs.ExplodingArrowsChargedWaterBlastAbility, AbilityRefs.ExplodingArrowsColdBlastAbility,
                AbilityRefs.ExplodingArrowsEarthBlastAbility, AbilityRefs.ExplodingArrowsElectricBlastAbility, AbilityRefs.ExplodingArrowsFireBlastAbility,
                AbilityRefs.ExplodingArrowsIceBlastAbility, AbilityRefs.ExplodingArrowsMagmaBlastAbility, AbilityRefs.ExplodingArrowsMetalBlastAbility,
                AbilityRefs.ExplodingArrowsMudBlastAbility, AbilityRefs.ExplodingArrowsPlasmaBlastAbility, AbilityRefs.ExplodingArrowsSandstormBlastAbility,
                AbilityRefs.ExplodingArrowsSteamBlastAbility, AbilityRefs.ExplodingArrowsThunderstormBlastAbility, AbilityRefs.ExplodingArrowsWaterBlastAbility];
            foreach(var ability in abilities)
            {
                AbilityConfigurator.For(ability)
                    .EditComponent<AbilityKineticist>(c => changeinfusions(c))
                    .Configure();
            }
        }
        public static void changeinfusions(AbilityKineticist component)
        {
            component.BlastBurnCost = 0;
            component.InfusionBurnCost = 2;
        }
        [DragonConfigure]
        public static void PatchDragonWrath()
        {
            Main.log.Log("Buffing DragonWrath spell to have available metamagics.");
            Blueprint<BlueprintReference<BlueprintAbility>>[] spells = [AbilityRefs.DragonWrath, AbilityRefs.DragonWrathGold, AbilityRefs.DragonWrathGoldCorrupted];
            Metamagic metas = Metamagic.CompletelyNormal | Metamagic.Reach | Metamagic.Empower | Metamagic.Bolstered | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Intensified;
            foreach (var spell in spells)
            {
                AbilityConfigurator.For(spell)
                    .SetAvailableMetamagic(metas)
                    .Configure();
            }
        }
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
    }
}
