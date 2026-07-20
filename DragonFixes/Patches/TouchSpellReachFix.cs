using System;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.Utility;

namespace DragonFixes.Patches
{
    /// <summary>
    /// Touch spells (Shocking Grasp, Chill Touch, Cure Wounds, etc.) often fail to land
    /// after the caster walks right up to the target. Out of range, nothing happens, and the
    /// spell slot is already gone.
    ///
    /// A sticky-touch spell runs as two commands: the initial cast (which Owlcat gives a generous
    /// approach radius) and a separate TouchDeliveryAbility. This command misses every range safety
    /// branch the game has. IsTouchAbility() returns false for it, so neither the turn based
    /// float.MaxValue guard nor the real time generous radius applies. It falls back to
    /// GetApproachDistance(), which only grants a fixed 2-foot slack. Various nav errors can eat
    /// that slack entirely and fail.
    ///
    /// We pad the delivery command's ApproachRadius up to a 5-foot reach (a touch attack is,
    /// mechanically, an unarmed reach melee attack). This is padded at Init rather than in the
    /// closeness check because the radius also feeds turn-based pathfinding's end condition.
    /// </summary>
    [HarmonyPatch]
    internal class TouchSpellReachFix
    {
        // Bring the 2ft MinWeaponRange slack up to the 5ft touch reach
        private static readonly float TouchReachPadding = 3.Feet().Meters;

        [HarmonyPatch(typeof(UnitUseAbility), nameof(UnitUseAbility.Init)), HarmonyPostfix]
        private static void Init_Postfix(UnitUseAbility __instance, UnitEntityData executor)
        {
            try
            {
                AbilityData ability = __instance.Ability;
                if (ability == null)
                    return;

                // Only touch the touch range spells, ignoring Metamagic reach
                if (ability.Range != AbilityRange.Touch || ability.HasMetamagic(Metamagic.Reach))
                    return;
                if (ability.Blueprint.GetComponent<AbilityDeliverTouch>() == null)
                    return;

                __instance.ApproachRadius += TouchReachPadding;
            }
            catch (Exception e)
            {
                Main.log.Log("TouchSpellReachFix error: " + e);
            }
        }
    }
}
