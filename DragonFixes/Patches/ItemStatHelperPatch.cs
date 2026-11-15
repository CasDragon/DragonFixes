using HarmonyLib;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.UnitLogic.Parts;

namespace DragonFixes.Patches;

[HarmonyPatch(typeof(ItemStatHelper))]
public class ItemStatHelperPatch
{
    // surely this will work first try with 0 testing, surely 
    [HarmonyPatch(typeof(ItemStatHelper), nameof(ItemStatHelper.GetCasterLevel), [typeof(ItemEntity), typeof(UnitEntityData)])]
    [HarmonyPostfix]
    public static void EnhancePotionCheckFix(ref int __result, ItemEntity item, UnitEntityData caster)
    {
        if (item.Blueprint is not BlueprintItemEquipmentUsable blueprintItemEquipment)
            return;
        UnitPartEnhancePotion unitPartEnhancePotion = caster.Get<UnitPartEnhancePotion>();
        if (unitPartEnhancePotion == null || blueprintItemEquipment.Type != UsableItemType.Potion)
            return;
        int potionLevel = blueprintItemEquipment.CasterLevel;
        if (unitPartEnhancePotion.classData.Level > potionLevel)
            return;
        __result = potionLevel;
    }
}