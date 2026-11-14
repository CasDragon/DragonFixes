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
    private static BlueprintFeature _flurry;
    private static BlueprintFeature Flurry => _flurry ??= FeatureRefs.FlurryOfBlows.Reference.Get();
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(RestrictionsHelper), nameof(RestrictionsHelper.CheckHasTwoWeapon))]
    public static void CheckHasTwoWeapon_Postfix(ref bool __result, UnitEntityData unit)
    {
        if (__result || unit?.Body == null || unit.GetFeature(Flurry) != null)
            return;

        var body = unit.Body;

        if ((body.IsPolymorphed && !body.IsPolymorphKeepSlots) || !body.HandsAreEnabled)
            return;

        var main = body.PrimaryHand.MaybeWeapon;
        var off  = body.SecondaryHand.MaybeWeapon;
        var fist = body.EmptyHandWeapon;

        if (main == null || off == null || main != fist || off != fist)
            return;

        __result = true;
    }
}