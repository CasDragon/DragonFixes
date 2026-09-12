using System;
using DragonLibrary.Utils;
using HarmonyLib;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;

namespace DragonFixes.Patches;

/*[HarmonyPatch]
[HarmonyPatchCategory(SettingName)]
public class KensaiIaijutsuFocusPatch
{
    private const string SettingName = "lethalfocuspatch";
    private const string SettingDescription = "Change the logic of Lethal Focus to check for flat foot during the attack, not before. May fix issue with Final Feint?";

    public static void ChangePatchStatus(bool enabled)
    {
        if (enabled)
        {
            Main.log.Log("KensaiIaijutsuFocusPatch enabled");
            Main.HarmonyInstance.PatchCategory(SettingName);
        }
        else
        {
            Main.log.Log("KensaiIaijutsuFocusPatch disabled");
            Main.HarmonyInstance.UnpatchCategory(SettingName);
        }
    }

    [DragonSetting(SettingCategories.None, SettingName, SettingDescription, typeof(KensaiIaijutsuFocusPatch), nameof(ChangePatchStatus))]
    [DragonConfigure]
    private static void ApplyPatch()
    {
        ChangePatchStatus(SettingsAction.GetSetting<bool>(SettingName));
    }
    
    [HarmonyPatch(typeof(KensaiIaijutsuFocus), nameof(KensaiIaijutsuFocus.OnEventAboutToTrigger))]
    [HarmonyPrefix]
    public static bool SkipEvent()
    {
        return false;
    }
    [HarmonyPatch(typeof(KensaiIaijutsuFocus), nameof(KensaiIaijutsuFocus.OnEventDidTrigger))]
    [HarmonyPrefix]
    public static void OnEventAboutToTrigger(RuleAttackWithWeapon evt, KensaiIaijutsuFocus __instance)
    {
        RuleCheckTargetFlatFooted ruleCheckTargetFlatFooted = Rulebook.Trigger<RuleCheckTargetFlatFooted>(new RuleCheckTargetFlatFooted(evt.Initiator, evt.Target));
        if (!KensaiChosenWeapon.CheckWeaponEquipped(__instance.Owner, __instance.ChosenWeaponBlueprint)) return;
        WeaponCategory category = evt.Weapon.Blueprint.Category;
        WeaponCategory? chosenWeapon = KensaiChosenWeapon.GetChosenWeapon(__instance.Owner, __instance.ChosenWeaponBlueprint);
        if (((category == chosenWeapon.GetValueOrDefault()) & (chosenWeapon != null)) && ruleCheckTargetFlatFooted.IsFlatFooted)
        {
            evt.WeaponStats.AddDamageModifier(Math.Max(0, __instance.Owner.Stats.Intelligence.Bonus), __instance.Fact, ModifierDescriptor.UntypedStackable);
        }
    }
}*/