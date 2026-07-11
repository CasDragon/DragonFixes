using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BlueprintCore.Utils;
using DragonFixes.Util;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using TabletopTweaks.Core.Utilities;

namespace DragonFixes.Fixes.VariousFixes;

// All of these fixes edit the single shared FeintAbility blueprint. The first two are gated behind
// the "slayerfeintfix" toggle because they only change behaviour for Slayer's Feint; the third
// (FixFeintDcUsesPerceptionWhenHigher) is a general feint fix and has its own toggle.
public class FeintFixes
{
    [DragonConfigure]
    public static void FixFeintCheckForCaster()
    {
        if (!Settings.GetSetting<bool>("slayerfeintfix"))
        {
            Main.log.Log("Slayer's Feint patch disabled, skipping.");
            return;
        }
        Main.log.Log("Fixing Slayer's Feint skill checks to roll for the caster instead of the target.");
        // CheckForCaster = true makes the caster of the ability roll the check, not the target.
        // Notably, this only happened For enemies that were more perceptive than BAB+Wis,
        // but that's most of them...
        var ability = BlueprintTool.Get<BlueprintAbility>(Guids.FeintAbility);
        foreach (var check in ability.FlattenAllActions().OfType<ContextActionSkillCheck>())
        {
            check.CheckForCaster = true;
        }
    }

    [DragonConfigure]
    public static void FixFinalFeintMobilityMarker()
    {
        if (!Settings.GetSetting<bool>("slayerfeintfix"))
        {
            Main.log.Log("Slayer's Feint patch disabled, skipping.");
            return;
        }
        // On the "Final Feint = true -> Slayer's Feint -> Mobility" path, FeintAbility applies the
        // ordinary feint marker instead of the Final Feint marker, so the target is never made
        // flat-footed to all attackers and the Final Feint benefit silently fails on that path
        // (the Persuasion path is unaffected).
        //
        // When using Final Feint + Slayer's Feint AND Mobility is higher than Persuasion, the
        // normal Feint debuff was applied instead of the Final Feint one.
        //
        // There's a lot of branches here though, so we need to be highly specific about which one
        // to swap. Even then, we need to recurse deeper still.

        var ability = BlueprintTool.Get<BlueprintAbility>(Guids.FeintAbility);
        var ordinaryMarker = BlueprintTool.Get<BlueprintBuff>(Guids.FeintMarkerBuff);
        var finalFeintMarker = BlueprintTool.Get<BlueprintBuff>(Guids.FinalFeintMarkerBuff)
            .ToReference<BlueprintBuffReference>();

        var swaps = 0;
        foreach (var gate in ability.FlattenAllActions().OfType<Conditional>().Where(IsFinalFeintGate))
        {
            foreach (var apply in FlattenActions(gate.IfTrue).OfType<ContextActionApplyBuff>()
                         .Where(a => a.Buff == ordinaryMarker))
            {
                apply.m_Buff = finalFeintMarker;
                swaps++;
            }
        }

        if (swaps == 2)
        {
            Main.log.Log("Fixed Final Feint marker on the Slayer's Feint Mobility path (swapped 2 buffs).");
        }
        else
        {
            Main.log.Error($"Final Feint marker fix: expected to swap 2 buffs but swapped {swaps}.");
        }
    }

    [DragonConfigure]
    public static void FixFeintDcUsesPerceptionWhenHigher()
    {
        if (!Settings.GetSetting<bool>("feintdcfix"))
        {
            Main.log.Log("Feint DC patch disabled, skipping.");
            return;
        }
        // The feint DC is meant to be the higher of two properties: 10+BAB+Wis and 10+Perception.
        // The comparison is done correctly, but when Perception is the higher of the two the check
        // still rolls against the 10+BAB+Wis property, making feints ~6 points easier than intended.
        // Retarget the DC in that (IfFalse) branch to the Perception property.
        var ability = BlueprintTool.Get<BlueprintAbility>(Guids.FeintAbility);
        var perceptionDc = BlueprintTool.Get<BlueprintUnitProperty>(Guids.FeintDcPerceptionProperty)
            .ToReference<BlueprintUnitPropertyReference>();

        var swaps = 0;
        foreach (var gate in ability.FlattenAllActions().OfType<Conditional>().Where(IsFeintDcComparison))
        {
            // The comparison is "BAB-DC >= Perception-DC"; its IfFalse branch is the "Perception is
            // higher" case, whose checks wrongly use the BAB-DC property. Only that branch is walked,
            // and only the check's DC is touched, so the comparison operands stay intact.
            foreach (var check in FlattenActions(gate.IfFalse).OfType<ContextActionSkillCheck>()
                         .Where(c => c.UseCustomDC
                                     && c.CustomDC?.m_CustomProperty != null
                                     && c.CustomDC.m_CustomProperty.deserializedGuid == Guids.FeintDcBabProperty))
            {
                check.CustomDC.m_CustomProperty = perceptionDc;
                swaps++;
            }
        }

        if (swaps == 6)
        {
            Main.log.Log("Fixed feint DC to use Perception when it is higher (swapped 6 checks).");
        }
        else
        {
            Main.log.Error($"Feint DC fix: expected to swap 6 DCs but swapped {swaps}.");
        }
    }

    // Final Feint is the top level of its subtree
    private static bool IsFinalFeintGate(Conditional conditional)
    {
        return conditional.ConditionsChecker?.Conditions != null
               && conditional.ConditionsChecker.Conditions
                   .OfType<ContextConditionCasterHasFact>()
                   .Any(c => !c.Not && c.m_Fact != null && c.m_Fact.deserializedGuid == Guids.FinalFeintFact);
    }

    // This predicates a CheckCondition that accepts the BAB/Per arguments
    private static bool IsFeintDcComparison(Conditional conditional)
    {
        return conditional.ConditionsChecker?.Conditions != null
               && conditional.ConditionsChecker.Conditions
                   .OfType<ContextConditionCompare>()
                   .Any(c => c.CheckValue?.m_CustomProperty != null
                             && c.TargetValue?.m_CustomProperty != null
                             && c.CheckValue.m_CustomProperty.deserializedGuid == Guids.FeintDcBabProperty
                             && c.TargetValue.m_CustomProperty.deserializedGuid == Guids.FeintDcPerceptionProperty);
    }

    // Equivalent to TTT's FlattenAllActions but startable from an arbitrary subtree
    // (FlattenAllActions' recursive overload is not public).
    private static IEnumerable<GameAction> FlattenActions(ActionList list)
    {
        if (list?.Actions == null)
        {
            yield break;
        }
        foreach (var action in list.Actions)
        {
            if (action == null)
            {
                continue;
            }
            yield return action;
            foreach (var field in action.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                if (field.FieldType == typeof(ActionList))
                {
                    foreach (var nested in FlattenActions(field.GetValue(action) as ActionList))
                    {
                        yield return nested;
                    }
                }
            }
        }
    }
}
