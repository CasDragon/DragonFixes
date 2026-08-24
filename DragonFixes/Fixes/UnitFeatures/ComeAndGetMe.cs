using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;

namespace DragonFixes.Fixes.UnitFeatures;

public class ComeAndGetMe
{
    [DragonConfigure]
    public static void PatchComeAndGetMeSwitchBuff()
    {
        Main.log.Log("Patching ComeAndGetMeSwitchBuff to include Skald rage.");
        BuffConfigurator.For(BuffRefs.ComeAndGetMeSwitchBuff)
            .EditComponent<AddFactContextActions>(DoThing)
            .Configure();
    }

    private static void DoThing(AddFactContextActions component)
    {
        var condtion = component.Activated.Actions.First(c => c is Conditional) as Conditional;
        if (condtion == null)
            return;
        var newcomp = new ContextConditionHasFact()
        {
            m_Fact = BuffRefs.InspiredRageEffectBuffBeforeMasterSkald.Reference.Get()
                .ToReference<BlueprintUnitFactReference>(),
            Not = false
        };
        condtion.ConditionsChecker.Conditions = [.. condtion.ConditionsChecker.Conditions, newcomp];
    }
}