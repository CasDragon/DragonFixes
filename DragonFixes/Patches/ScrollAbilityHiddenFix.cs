using HarmonyLib;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Items;

namespace DragonFixes.Patches
{
    [HarmonyPatch]
    internal class ScrollAbilityHiddenFix
    {
        [HarmonyPatch(typeof(Ability), nameof(Ability.Hidden), MethodType.Getter)]
        [HarmonyPostfix]
        public static void HiddenPostfix(Ability __instance, ref bool __result)
        {
            if (__result &&
                    __instance.m_Hidden == false &&
                    __instance.Blueprint.Hidden == false &&
                    __instance.Data.IsVisible() == true &&
                    __instance.SourceItem?.Blueprint is BlueprintItemEquipmentUsable y &&
                    (y.Type == UsableItemType.Scroll || y.Type == UsableItemType.Wand) &&
                    y.Ability != __instance.Blueprint)
            {
                __result = false;
                if (y.Type == UsableItemType.Scroll && __instance.Data.SourceItem.Charges == 0)
                    __instance.Data.SourceItem.Charges = 1;
            }
        }
        /*[HarmonyPatch(typeof(AbilityCastRateUtils), nameof(AbilityCastRateUtils.GetChargesCount))]
        [HarmonyPostfix]
        public static void ChargesPostFix(AbilityData ability, ref int __result)
        {
            if (__result == 0 && ability.SourceItem != null && ability.SourceItem.IsSpendCharges &&
                    ability.SourceItem?.Blueprint is BlueprintItemEquipmentUsable y &&
                    (y.Type == UsableItemType.Scroll || y.Type == UsableItemType.Wand) &&
                    y.Ability != ability.Blueprint)
                __result = -1;
        }*/
    }
}
