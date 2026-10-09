using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Kingmaker.Items.Slots;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.UnitLogic.Parts;

namespace DragonFixes.Patches
{
    /// <summary>
    /// A charging Triceratops or Elk with Powerful Charge runs up and never swings. Their gore
    /// weapon sits in the primary hand, and UnitAttack.CreateSingleAttack tests a hand slot
    /// charge weapon against a null attack count result, throwing before the attack list is
    /// built. A charge weapon on an additional limb, or no charge weapon at all, never reaches
    /// that branch.
    /// </summary>
    [HarmonyPatch]
    internal static class ChargeWeaponSingleAttackFix
    {
        [HarmonyPatch(typeof(UnitAttack), nameof(UnitAttack.CreateSingleAttack)), HarmonyPrefix]
        private static bool CreateSingleAttack_Prefix(UnitAttack __instance, ref List<AttackHandInfo> __result)
        {
            try
            {
                var executor = __instance.Executor;
                WeaponSlot chargeSlot = __instance.IsCharge ? executor.Get<UnitPartChargeWeapon>()?.WeaponSlot : null;

                // Anything other than a hand slot charge weapon is safe to leave to the original
                // method (no charge weapon fact, or one that resolved to an additional limb).
                if (!(chargeSlot is HandSlot))
                    return true;

                RuleCalculateAttacksCount.ResultData counts =
                    Rulebook.Trigger(new RuleCalculateAttacksCount(executor)).Result;

                if (CanAttack(chargeSlot, counts))
                {
                    __result = new List<AttackHandInfo> { new AttackHandInfo(chargeSlot, 0) };
                    return false;
                }

                HandSlot hand = CanAttack(executor.Body.PrimaryHand, counts) ? executor.Body.PrimaryHand
                    : CanAttack(executor.Body.SecondaryHand, counts) ? executor.Body.SecondaryHand
                    : null;
                if (hand != null)
                {
                    __result = new List<AttackHandInfo> { new AttackHandInfo(hand, 0) };
                    return false;
                }

                WeaponSlot limb = executor.Body.AdditionalLimbs.FirstOrDefault(l => CanAttack(l, counts));
                __result = limb == null ? null : new List<AttackHandInfo> { new AttackHandInfo(limb, 0) };
                return false;
            }
            catch (Exception e)
            {
                Main.log.Log("ChargeWeaponSingleAttackFix error: " + e);
                return true;
            }
        }

        // UnitAttack.CreateSingleAttack's own slot test, with the attack counts always supplied
        // instead of the original's null literal.
        private static bool CanAttack(WeaponSlot slot, RuleCalculateAttacksCount.ResultData counts)
        {
            if (slot == null || !slot.HasWeapon)
                return false;
            if (!(slot is HandSlot hand))
                return true;
            return hand.IsPrimaryHand ? counts.PrimaryHand.MainAttacks > 0 : counts.SecondaryHand.MainAttacks > 0;
        }
    }
}
