using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.BasicEx;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;

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
    }
}
