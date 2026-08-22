using System.Linq;
using DragonLibrary.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Items;
using Kingmaker.UnitLogic;

namespace DragonFixes.Patches;

[HarmonyPatch]
[HarmonyPatchCategory("scrollownerfix")]
public class ModifiableValuePatch
{
    private const string SettingName = "scrollownerfix";

    private const string SettingDescription =
        "Code fix for scroll buffs not counting as valid for features such as Master Shapeshifter after a save/load.";

    public static void ChangePatchStatus(bool enabled)
    {
        if (enabled)
        {
            Main.log.Log("IsModifierValidPostfix enabled");
            Main.HarmonyInstance.PatchCategory("scrollownerfix");
        }
        else
        {
            Main.log.Log("IsModifierValidPostfix disabled");
            Main.HarmonyInstance.UnpatchCategory("scrollownerfix");
        }
    }

    [DragonSetting(SettingCategories.None, SettingName, SettingDescription, typeof(ModifiableValuePatch), nameof(ChangePatchStatus))]
    [DragonConfigure]
    private static void ApplyPatch()
    {
        ChangePatchStatus( SettingsAction.GetSetting<bool>(SettingName));
    }
    
    [HarmonyPatch(typeof(ModifiableValue), nameof(ModifiableValue.IsModifierValid))]
    [HarmonyPrefix]
    public static bool IsModifierValidPostfix(ref bool __result, UnitDescriptor owner, ModifiableValue.Modifier mod)
    {
        ItemEntity itemSource = mod.ItemSource;
        if (itemSource != null)
        {
            if (itemSource is ItemEntityUsable)
            {
                __result = true;
                return false;
            }
            if (itemSource.Collection != owner.Inventory || owner != itemSource.Wielder)
            {
                __result = false;
                return false;
            }
        }
        __result = mod.Source == null || (mod.Source.Active && (mod.Source is ItemEnchantment || owner.HasFact(mod.Source)) 
                            && (string.IsNullOrWhiteSpace(mod.SourceComponent) || 
                            mod.Source.SelectComponents((BlueprintComponent c) 
                                => c.name == mod.SourceComponent).Any()));
        return false;
    }
}