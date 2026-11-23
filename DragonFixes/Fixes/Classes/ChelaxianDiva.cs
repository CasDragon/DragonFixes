using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Mechanics.Conditions;

namespace DragonFixes.Fixes.Classes;
internal class ChelaxianDiva
{
    [DragonConfigure]
    public static void PatchTragedyOfFalseHopeArea()
    {
        Main.log.Log("Patching TragedyOfFalseHopeArea to include Mind Affecting descriptor");
        AbilityAreaEffectConfigurator.For(AbilityAreaEffectRefs.TragedyOfFalseHopeArea)
            .AddSpellDescriptorComponent(descriptor: SpellDescriptor.MindAffecting)
            .Configure();
    }
    [DragonConfigure]
    public static void BlazingRondoIsAlly()
    {
        Main.log.Log("Patching BlazingRondoAreaEffect to replace IsEnemy check with IsAlly to stop enemies from gaining it's effect");
        var areaEffect = AbilityAreaEffectRefs.BlazingRondoAreaEffect.Reference.Get();
        var abilityRun = areaEffect.Components.FirstOrDefault(c => c is AbilityAreaEffectRunAction) as AbilityAreaEffectRunAction;
        var newComp = new ContextConditionIsAlly();
        var conditional1 = abilityRun?.UnitEnter.Actions.FirstOrDefault(c => c is Conditional) as Conditional;
        var conditional2 = abilityRun?.UnitExit.Actions.FirstOrDefault(c => c is Conditional) as Conditional;
        conditional1?.ConditionsChecker.Conditions = [newComp];
        conditional2?.ConditionsChecker.Conditions = [newComp];
    }
}
