using System;
using BlueprintCore.Blueprints.References;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items.Slots;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;

namespace DragonFixes.Patches;

[HarmonyPatch(typeof(RuleCalculateAttacksCount))]
public static class RuleCalculateAttacksCountPatches
{
    private static BlueprintFeature _flurry;
    private static BlueprintFeature Flurry => _flurry ??= FeatureRefs.FlurryOfBlows.Reference.Get(); 
    private static BlueprintFeature _twf;
    private static BlueprintFeature TWF => _twf ??= FeatureRefs.TwoWeaponFighting.Reference.Get(); 
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(RuleCalculateAttacksCount), nameof(RuleCalculateAttacksCount.CalculateSecondaryHand))]
    public static void CalculateSecondaryHand_Postfix(RuleCalculateAttacksCount __instance, bool isPrimaryTwoHanded,
        BlueprintItemWeapon offWeapon, BlueprintItemWeapon mainWeapon, HandSlot offHand, int penalizedAttacksCount)
    {
        if (isPrimaryTwoHanded || offWeapon == null || mainWeapon == null)
            return;
        if (!offWeapon.IsUnarmed || !mainWeapon.IsUnarmed)
            return;
        if (__instance.Result.SecondaryHand.AdditionalAttacks > 0)
            return;

        if (!UnitTesting(__instance.Initiator))
            return;

        __instance.Result.SecondaryHand.AdditionalAttacks++;

        if (!offHand.Owner.State.Features.ImprovedUnarmedStrike)
            __instance.Result.SecondaryHand.PenalizedAttacks += Math.Max(0, penalizedAttacksCount);
    }

    private static bool UnitTesting(UnitEntityData unit)
    {
        if (unit.GetFeature(Flurry) != null)
            return false;
        if (unit.GetFeature(TWF) != null)
            return true;
        return false;
    }
}