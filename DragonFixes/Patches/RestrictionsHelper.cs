using BlueprintCore.Blueprints.References;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;

namespace DragonFixes.Patches;

[HarmonyPatch]
public class RestrictionsHelperPatches
{
    private static readonly BlueprintFeature flurry = FeatureRefs.FlurryOfBlows.Reference.Get();
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(RestrictionsHelper), nameof(RestrictionsHelper.CheckHasTwoWeapon))]
    public static void CheckHasTwoWeapons_Postfix(ref bool __result, UnitEntityData unit)
    {
        if (__result)
            return;
        if (unit == null)
            return;
        if (unit.GetFeature(flurry) is not null)
            return;
        var weapon1 = unit.Body.PrimaryHand.MaybeWeapon;
        var weapon2 = unit.Body.SecondaryHand.MaybeWeapon;
        var x = weapon1 == unit.Body.EmptyHandWeapon;
        var y = weapon2 == unit.Body.EmptyHandWeapon;
        var z = false;
        if (weapon1 != null)
            z = weapon1.Blueprint.IsNatural;
        var v = false;
        if (weapon2 != null)
            v = weapon2.Blueprint.IsNatural;
        if (z || v)
            return;
        __result = x && y;
    }
}