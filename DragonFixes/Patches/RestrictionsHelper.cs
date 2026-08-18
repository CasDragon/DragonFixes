using HarmonyLib;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;

namespace DragonFixes.Patches;

[HarmonyPatch]
public class RestrictionsHelperPatches
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(RestrictionsHelper), nameof(RestrictionsHelper.CheckHasTwoWeapon))]
    public static void CheckHasTwoWeapons_Postfix(ref bool __result, UnitEntityData unit)
    {
        if (__result)
            return;
        var x = unit.Body.PrimaryHand.MaybeWeapon == unit.Body.EmptyHandWeapon;
        var y = unit.Body.SecondaryHand.MaybeWeapon == unit.Body.EmptyHandWeapon;
        __result = x && y;
    }
}