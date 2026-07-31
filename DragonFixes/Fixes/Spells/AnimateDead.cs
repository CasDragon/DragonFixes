using System;
using System.Linq;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using DragonLibrary.BPCoreExtensions;
using DragonLibrary.Utils;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace DragonFixes.Fixes.Spells;

public class AnimateDead
{
    [DragonConfigure]
    public static void PatchAnimateDead()
    {
        bool isDC = ModCompat.IsModEnabled("DarkCodex");
        if (!isDC)
        {
            try
            {
                Main.log.Log("Patching AnimateDead (and Lesser) to include NecromancersStaffFeature buff");
                AbilityConfigurator.For(AbilityRefs.AnimateDead)
                    .EditComponent<AbilityEffectRunAction>(c => dothing(c))
                    .Configure();
                AbilityConfigurator.For(AbilityRefs.AnimateDeadLesser)
                    .EditComponent<AbilityEffectRunAction>(c => dothing(c))
                    .Configure();
            }
            catch (Exception e)
            {
                Main.log.Log("Error patching Animate Dead");
                Main.log.LogException(e);
            }
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
        mob!.AfterSpawn.Actions = [ .. mob.AfterSpawn.Actions , y.Actions[0] ];
    }
}