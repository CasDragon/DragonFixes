using HarmonyLib;
using Kingmaker.Enums;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Buffs.Components;

namespace DragonFixes.Patches;

[HarmonyPatch]
public class WeaponAttackTypeDamageBonusPatch
{
    [HarmonyPatch(typeof(WeaponAttackTypeDamageBonus), nameof(WeaponAttackTypeDamageBonus.OnEventAboutToTrigger))]
    [HarmonyPrefix]
    public static bool DescriptorFix(RuleCalculateWeaponStats evt, WeaponAttackTypeDamageBonus __instance)
    {
        if (__instance.Fact.MaybeContext == null) return false;
        int num = __instance.Value.Calculate(__instance.Fact.MaybeContext) * __instance.AttackBonus;
        if (__instance.Type.IsSuitableAttackType(evt.Weapon.Blueprint.AttackType))
        {
            evt.AddDamageModifier(num * __instance.Fact.GetRank(), __instance.Fact, __instance.Descriptor);
        }
        return false;
    }
}