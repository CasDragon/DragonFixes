using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace DragonFixes.Fixes.UnitFeatures;

public class PurgingFinale
{
    [DragonConfigure]
    public static void Configure()
    {
        Main.log.Log("Patching Purging Finale");
        Blueprint<BlueprintReference<BlueprintAbility>>[] x = [
            AbilityRefs.PurgingFinaleExhausted, AbilityRefs.PurgingFinaleParalyzed,
            AbilityRefs.PurgingFinaleShaken, AbilityRefs.PurgingFinaleStunned];
        foreach (var i in x)
        {
            AbilityConfigurator.For(i)
                .EditComponent<AbilityEffectRunAction>(DoThing)
                .Configure();
        }
        var dispel = TTTHelpers.CreateCopy(AbilityRefs.PurgingFinaleExhausted.Reference.Get().GetComponent<AbilityEffectRunAction>()
            ?.Actions.Actions.First(a => a is ContextActionDispelMagic) as ContextActionDispelMagic);
        dispel.Descriptor = SpellDescriptor.Daze;
        AbilityConfigurator.For(AbilityRefs.PurgingFinaleDazzled)
            .EditComponent<AbilityEffectRunAction>(c => c.Actions.Actions = [dispel, .. c.Actions.Actions])
            .Configure();
    }

    public static void DoThing(AbilityEffectRunAction action)
    {
        if (action.Actions.Actions.First(a => a is ContextActionDispelMagic) is ContextActionDispelMagic dispel) 
            dispel.CheckSchoolOrDescriptor = false;
    }
}