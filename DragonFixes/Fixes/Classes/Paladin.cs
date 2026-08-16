using System.Linq;
using BlueprintCore.Blueprints.References;
using DragonLibrary.NewComponents;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Mechanics.Conditions;

namespace DragonFixes.Fixes.Classes;

public class Paladin
{
    // TorturedCrusader
    [DragonConfigure]
    public static void noFunAllowed()
    {
        Main.log.Log("Fixing Tortured Crusader's Alone in the Dark - Last Man Standing feature to not scale off summoned spiders.");
        var x = new ContextConditionHasBuff()
        {
            m_Buff = BuffRefs.ClemencyOfShadowsExclusionBuff.Reference.Get().ToReference<BlueprintBuffReference>(),
            Not = true
        };
        var z = new ContextConditionHasBuff()
        {
            m_Buff = BuffRefs.NaturalAllyCreatureVisual.Reference.Get().ToReference<BlueprintBuffReference>(),
            Not = true
        };
        var y = new ConditionIsFaction()
        {
            Faction = FactionRefs.Summoned.Reference.Get(),
            Not = true
        };
        var runAction = AbilityAreaEffectRefs.LastManAreaEffect.Reference.Get()
            .GetComponent<AbilityAreaEffectRunAction>();
        var condition1 = runAction!.UnitEnter.Actions.First(c => c is Conditional) as Conditional;
        condition1!.ConditionsChecker.Conditions = [.. condition1.ConditionsChecker.Conditions, x, y, z];
        var condition2 = runAction!.Round.Actions.First(c => c is Conditional) as Conditional;
        condition2!.ConditionsChecker.Conditions = [.. condition2.ConditionsChecker.Conditions, x, y, z];
    }
}