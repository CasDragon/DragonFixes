using HarmonyLib;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;

namespace DragonFixes.Patches;

/*[HarmonyPatch(typeof(CavalierMountedMastery))]
public class CavalierMountedMasteryPatch
{
    private static ModifiableValue.Modifier modifier;
    
    [HarmonyPatch(typeof(CavalierMountedMastery), nameof(CavalierMountedMastery.OnEventAboutToTrigger))]
    [HarmonyPrefix]
    public static bool OnEventAboutToTriggerFix(CavalierMountedMastery __instance, RuleAttackWithWeapon evt)
    {
        if (evt.Weapon == null || evt.Initiator.GetSaddledUnit() == null || !evt.IsCharge) return false;
        
        modifier = evt.Initiator.Stats.AdditionalDamage.AddModifier(
            evt.Initiator.GetSaddledUnit()!.Stats.Strength.Bonus,
            __instance.Runtime,
            __instance.Descriptor);
        evt.AddTemporaryModifier(modifier);

        return false;
    }
    [HarmonyPatch(typeof(CavalierMountedMastery), nameof(CavalierMountedMastery.OnEventDidTrigger))]
    [HarmonyPostfix]
    public static void OnEventDidTriggerFix(CavalierMountedMastery __instance, RuleAttackWithWeapon evt)
    {
        if (evt.Weapon != null && evt.Initiator.GetSaddledUnit() != null && evt.IsCharge)
        {
            evt.Initiator.Stats.AdditionalDamage.RemoveModifier(modifier);
        }
    }
}*/