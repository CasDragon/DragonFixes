/*using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using BlueprintCore.Blueprints.References;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Items;
using Kingmaker.RuleSystem.Rules;

namespace DragonFixes.Patches;

[HarmonyPatch(typeof(RuleCalculateWeaponStats), nameof(RuleCalculateWeaponStats.OnTrigger))]
public class RuleCalculateWeaponStatsPatch
{
    private static BlueprintFeature _twf;
    private static BlueprintFeature TWF => _twf ??= FeatureRefs.TwoWeaponFighting.Reference.Get();
    
    private static readonly System.Reflection.MethodInfo OriginalMethod =
        AccessTools.Method(typeof(RuleCalculateWeaponStats), nameof(RuleCalculateWeaponStats.IsSecondaryWeapon));

    private static readonly Func<ItemEntityWeapon, bool> Original =
        AccessTools.MethodDelegate<Func<ItemEntityWeapon, bool>>(OriginalMethod);

    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> OnTrigger_Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var replacement = AccessTools.Method(typeof(RuleCalculateWeaponStatsPatch), nameof(IsSecondaryWeapon_Patched));

        return new CodeMatcher(instructions)
            .MatchStartForward(new CodeMatch(ci => ci.Calls(OriginalMethod)))
            .ThrowIfInvalid("RuleCalculateWeaponStats.OnTrigger: IsSecondaryWeapon call not found")
            .InsertAndAdvance(new CodeInstruction(OpCodes.Ldarg_0))        // push the rule after the weapon argument
            .SetInstruction(new CodeInstruction(OpCodes.Call, replacement)) // same static call, new target
            .InstructionEnumeration();
    }

    public static bool IsSecondaryWeapon_Patched(ItemEntityWeapon weapon, RuleCalculateWeaponStats rule)
    {
        if (Original(weapon))
            return true;

        return IsDoubleUnarmedOffHand(weapon, rule);
    }

    private static bool IsDoubleUnarmedOffHand(ItemEntityWeapon weapon, RuleCalculateWeaponStats rule)
    {
        var unit = weapon?.Wielder?.Unit;
        if (unit == null || !weapon.Blueprint.IsUnarmed)
            return false;

        var body = unit.Body;
        if (body.PrimaryHand.MaybeWeapon != weapon || body.SecondaryHand.MaybeWeapon != weapon)
            return false;

        if (!RuleCalculateAttacksCountPatches.UnitTesting(unit)) 
            return false;

        return IsOffHandAttack(rule.AttackWithWeapon);
    }

    private static bool IsOffHandAttack(RuleAttackWithWeapon attack)
    {
        // TODO 
        return false;
    }
}*/