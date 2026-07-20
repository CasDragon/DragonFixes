using System;
using HarmonyLib;
using Kingmaker.Enums.Damage;

namespace DragonFixes.Fixes.Classes
{
    /// <summary>
    /// Kinetic Sharpshooter bow blasts deal damage via ContextActionDealDamage, not the normal
    /// weapon-attack path. On a confirmed crit, the path ultimately leads through
    /// DamageCriticalModifierType, an enum, which only contains x2, x3 and x4 values. All other
    /// attacks use an int throughout for crit multipliers without this enum.
    ///
    /// The fix simply has the enum return the modifier value.
    ///
    /// Now this would also blow up if GetName or a switch is done on the enum, but it has precisely
    /// one call site, in ContextActionDealDamage.
    /// </summary>
    [HarmonyPatch]
    internal class KineticSharpshooterCritFix
    {
        [HarmonyPatch(typeof(DamageCriticalModifierTypeExtension), nameof(DamageCriticalModifierTypeExtension.FromInt)), HarmonyPostfix]
        private static void FromInt_Postfix(int modifier, ref DamageCriticalModifierType? __result)
        {
            try
            {
                if (modifier > 4 && __result.HasValue)
                    __result = (DamageCriticalModifierType)modifier;
            }
            catch (Exception e)
            {
                Main.log.Log("KineticSharpshooterCritFix error: " + e);
            }
        }
    }
}
