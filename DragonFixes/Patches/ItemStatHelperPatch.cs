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
        if (item.Blueprint is not BlueprintItemEquipment blueprintItemEquipment)
            return;
        UnitPartEnhancePotion unitPartEnhancePotion = caster.Get<UnitPartEnhancePotion>();
        UsableItemType? usableItemType = (blueprintItemEquipment as BlueprintItemEquipmentUsable)?.Type;
        if (unitPartEnhancePotion == null || usableItemType != UsableItemType.Potion)
            return;
        if (unitPartEnhancePotion.classData.Level <= item.GetSpellLevel())
            return;
        __result = unitPartEnhancePotion.classData.Level;
    }
}