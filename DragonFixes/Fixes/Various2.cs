using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.BasicEx;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace DragonFixes.Fixes
{
    internal class Various2
    {
        [DragonConfigure]
        public static void PatchAnimateDead()
        {
            Main.log.Log("Patching AnimateDead (and Lesser) to include NecromancersStaffFeature buff");
            AbilityConfigurator.For(AbilityRefs.AnimateDead)
                .EditComponent<AbilityEffectRunAction>(c => dothing(c))
                .Configure();
            AbilityConfigurator.For(AbilityRefs.AnimateDeadLesser)
                .EditComponent<AbilityEffectRunAction>(c => dothing(c))
                .Configure();
        }

        public static void dothing(AbilityEffectRunAction action)
        {
            ContextActionSpawnMonster mob = action.Actions.Actions.OfType<ContextActionSpawnMonster>().FirstOrDefault();
            ConditionsChecker x = ConditionsBuilder.New()
                .CasterHasFact(FeatureRefs.NecromancersStaffFeature.Reference.Get()).Build();
            ActionList y = ActionsBuilder.New()
                .Conditional(x,
                    ifTrue: ActionsBuilder.New()
                        .ApplyBuffPermanent(BuffRefs.NecromancersStaffBuff.Reference.Get(), asChild: true)).Build();
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
    }
}
