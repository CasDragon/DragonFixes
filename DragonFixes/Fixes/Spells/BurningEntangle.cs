using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace DragonFixes.Fixes.Spells;

public class BurningEntangle
{
    [DragonConfigure]
    public static void PatchBurningEntangleArea()
    {
        Main.log.Log("Patching BurningEntangleArea to not damage on succeeding save");
        Conditional x = (Conditional)AbilityAreaEffectRefs.BurningEntangleArea.Reference.Get().GetComponent<AbilityAreaEffectRunAction>()
            ?.Round.Actions[0];
        ContextActionSavingThrow y = (ContextActionSavingThrow)x.IfFalse.Actions[0];
        var z = y.Actions.Actions[1];
        ContextActionConditionalSaved x1 = (ContextActionConditionalSaved)y.Actions.Actions[0];
        x1.Failed.Actions = [.. x1.Failed.Actions, z];
        x.IfFalse.Actions = [x1];
    }
}