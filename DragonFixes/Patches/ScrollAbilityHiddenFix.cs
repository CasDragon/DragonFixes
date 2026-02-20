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
                    y.Type == UsableItemType.Scroll &&
                    y.Ability != __instance.Blueprint)
            {
                __result = false;
                __instance.Data.SourceItemEquipmentBlueprint.SpendCharges = false;
            }
        }
    }
}
