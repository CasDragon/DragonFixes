using System;
using HarmonyLib;
using Kingmaker.Controllers.Units;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.UnitLogic.Parts;
using TurnBased.Controllers;

namespace DragonFixes.Patches
{
    /// <summary>
    /// Fix 1: a mounted rider drops its own attacks in turn-based mode — the pair rides up,
    /// only the mount attacks, and the tooltip shows e.g. "0+3".
    ///
    /// The rider and mount run separate UnitAttack commands with separate approach radii that
    /// differ by weapon range, so a long-reach mount can stop in a ring where it reaches the
    /// target but the rider doesn't - the rider then fails its closeness gate. We clamp the mount's
    /// radius down to the rider's so the ride-up always ends with the rider in reach (the game
    /// already does exactly this in one branch of TickDelegateRiderToMount
    /// (SaddledUnitController.cs:166)). We cover the paths it misses.
    /// </summary>
    [HarmonyPatch]
    internal class MountedRiderAttackFix
    {
        private static readonly AccessTools.FieldRef<UnitAttack, float?> OverrideApproachRadius =
            AccessTools.FieldRefAccess<UnitAttack, float?>("m_OverrideApproachRadius");

        [HarmonyPatch(typeof(SaddledUnitController), "TickDelegateMountToRider"), HarmonyPostfix]
        private static void TickDelegateMountToRider_Postfix(UnitPartSaddled mountPart)
        {
            ClampMountRadiusToRider(mountPart);
        }

        [HarmonyPatch(typeof(SaddledUnitController), "TickDelegateRiderToMount"), HarmonyPostfix]
        private static void TickDelegateRiderToMount_Postfix(UnitPartSaddled mountPart)
        {
            ClampMountRadiusToRider(mountPart);
        }

        private static void ClampMountRadiusToRider(UnitPartSaddled mountPart)
        {
            try
            {
                if (!CombatController.IsInTurnBasedCombat())
                    return;

                // The mount's attack command, and the rider's paired command
                UnitAttack mountAttack = mountPart?.Owner?.Commands?.Attack;
                if (mountAttack == null || !(mountAttack.RiderCommand is UnitAttack riderAttack))
                    return;

                // Only clamp when the mount reaches farther than the rider; this also skips
                // ranged riders for free, since their radius dwarfs a melee mount's
                float riderRadius = riderAttack.ApproachRadius;
                if (mountAttack.ApproachRadius <= riderRadius)
                    return;

                // Property is what the path-commit reads; the override field stops
                // UnitAttack.UpdateTarget from recomputing it away mid-approach
                mountAttack.ApproachRadius = riderRadius;
                OverrideApproachRadius(mountAttack) = riderRadius;
            }
            catch (Exception e)
            {
                Main.log.Log("MountedRiderAttackFix error: " + e);
            }
        }
    }
}
