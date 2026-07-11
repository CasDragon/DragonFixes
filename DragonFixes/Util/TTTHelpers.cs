using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.ElementsSystem;
using System;

namespace DragonFixes.Util;

public static class TTTHelpers
{
    public static IEnumerable<GameAction> FlattenAllActions(this BlueprintScriptableObject blueprint) {
        List<GameAction> actions = [];
        foreach (var component in blueprint.ComponentsArray.Where(c => c is not null)) {
            Type type = component.GetType();
            var foundActions = AccessTools.GetDeclaredFields(type)
                .Where(f => f.FieldType == typeof(ActionList))
                .SelectMany(field => ((ActionList)field.GetValue(component)).Actions);
            actions.AddRange(FlattenAllActions(foundActions));
        }
        return actions;
    }
    
    public static IEnumerable<GameAction> FlattenAllActions(this IEnumerable<GameAction> actions) {
        List<GameAction> newActions = new List<GameAction>();
        foreach (var action in actions) {
            Type type = action?.GetType();
            var foundActions = AccessTools.GetDeclaredFields(type)?
                .Where(f => f?.FieldType == typeof(ActionList))
                .SelectMany(field => ((ActionList)field.GetValue(action)).Actions);
            if (foundActions != null) { newActions.AddRange(foundActions); }
        }
        if (newActions.Count > 0) {
            return actions.Concat(FlattenAllActions(newActions));
        }
        return actions;
    }

    // Equivalent to TTT's FlattenAllActions but startable from an arbitrary subtree
    // (FlattenAllActions' recursive overload is not public).
    public static IEnumerable<GameAction> FlattenActions(ActionList list)
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