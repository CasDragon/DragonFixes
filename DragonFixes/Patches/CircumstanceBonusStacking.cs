using DragonLibrary.Utils;
using HarmonyLib;
using Kingmaker.Enums;

namespace DragonFixes.Patches;

[HarmonyPatch]
[HarmonyPatchCategory(SettingName)]
public class CircumstanceBonusStacking
{ 

    private const string SettingName = "circumstancebonus";
    private const string SettingDescription =
        "Allows circumstance bonuses to stack";

    public static void ChangePatchStatus(bool enabled)
    {
        if (enabled)
        {
            Main.log.Log("CircumstanceBonusStacking enabled");
            Main.HarmonyInstance.PatchCategory(SettingName);
        }
        else
        {
            Main.log.Log("CircumstanceBonusStacking disabled");
            Main.HarmonyInstance.UnpatchCategory(SettingName);
        }
    }

    [DragonSetting(SettingCategories.None, SettingName, SettingDescription, typeof(CircumstanceBonusStacking), nameof(ChangePatchStatus))]
    [DragonConfigure]
    private static void ApplyPatch()
    {
        ChangePatchStatus( SettingsAction.GetSetting<bool>(SettingName));
    }
    
    [HarmonyPatch(typeof(ModifierDescriptorHelper), nameof(ModifierDescriptorHelper.IsStackable))]
    [HarmonyPostfix]
    public static void IsStackablePatch(ref bool __result, ModifierDescriptor descriptor)
    {
        if (descriptor == ModifierDescriptor.Circumstance)
            __result = true;
    }
    
}