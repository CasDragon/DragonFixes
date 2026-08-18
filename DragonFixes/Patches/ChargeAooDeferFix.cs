using System;
using System.Collections.Generic;
using DragonLibrary.Utils;
using HarmonyLib;
using Kingmaker;
using Kingmaker.Controllers.Combat;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.UnitLogic.Commands.Base;

namespace DragonFixes.Patches
{
    /// <summary>
    /// A unit that hasn't delivered its charge attack yet shouldn't stop mid-charge to take a
    /// reaction. It takes it right after the charge lands instead.
    ///
    /// An attack of opportunity granted mid-charge resolves first. Outflank off a mount's critical
    /// hit is the usual trigger. This fix defers at UnitCombatState.AttackOfOpportunity, which
    /// every AoO source funnels through.
    /// </summary>
    [HarmonyPatch]
    internal static class ChargeAooDeferFix
    {
        // A backstop. Tick_Prefix below normally releases the reaction as soon as the charge
        // attack acts. As an un-started command never ticks, the engine's own 9s check can't
        // rescue one that stalls. We cap at roughly a round.
        private const double MaxDeferSeconds = 6.0;

        private const string SettingName = "chargeaoodefer";
        private const string SettingDescription =
            "Charge: a free attack granted mid-charge waits for the charge to land first";

        public static void Test(bool enabled)
        {
            //
        }

        [DragonSetting(SettingCategories.None, SettingName, SettingDescription, typeof(ChargeAooDeferFix), nameof(Test))]
        private static bool Enabled()
        {
            return SettingsAction.GetSetting<bool>(SettingName);
        }

        private sealed class PendingAoO
        {
            public UnitEntityData Attacker;
            public UnitEntityData Target;
            public bool Trickster;
            // Mobility defensive casting
            public bool AfterDefensivelyFail;
            public TimeSpan Deadline;
        }

        private static readonly List<PendingAoO> Deferred = new List<PendingAoO>();

        // Set while Tick_Prefix re-issues a deferred AoO, so the prefix lets it through. Without
        // it, an entry released by deadline while its charge attack was still pending would defer
        // itself forever.
        private static bool Refiring;

        [HarmonyPatch(typeof(UnitCombatState), nameof(UnitCombatState.AttackOfOpportunity)), HarmonyPrefix]
        private static bool AttackOfOpportunity_Prefix(UnitCombatState __instance, UnitEntityData target,
            bool tricksterAttack, bool simulate, bool disengaging, bool afterDefensivelyFail, ref bool __result)
        {
            try
            {
                if (Refiring || simulate)
                    return true;

                // Disengage reactions are tied to a unit leaving reach right now. Hold one until
                // the charge resolves and it often never lands at all. That's worse than losing a
                // charge bonus to it.
                if (disengaging)
                    return true;

                UnitEntityData attacker = __instance?.Unit;
                if (attacker == null || target == null)
                    return true;

                if (!Enabled())
                    return true;

                if (!HasPendingChargeAttack(attacker))
                    return true;

                if (!IsAlreadyDeferred(attacker, target))
                {
                    Deferred.Add(new PendingAoO
                    {
                        Attacker = attacker,
                        Target = target,
                        Trickster = tricksterAttack,
                        AfterDefensivelyFail = afterDefensivelyFail,
                        Deadline = Game.Instance.TimeController.GameTime + TimeSpan.FromSeconds(MaxDeferSeconds)
                    });
                }

                __result = false;
                return false;
            }
            catch (Exception e)
            {
                Main.log.Log("ChargeAooDeferFix error: " + e);
                return true;
            }
        }

        [HarmonyPatch(typeof(UnitCombatEngagementController), nameof(UnitCombatEngagementController.Tick)), HarmonyPrefix]
        private static void Tick_Prefix()
        {
            if (Deferred.Count == 0)
                return;

            try
            {
                TimeSpan now = Game.Instance.TimeController.GameTime;

                for (int i = Deferred.Count - 1; i >= 0; i--)
                {
                    PendingAoO pending = Deferred[i];

                    // Purged rather than fired. Usually this is because the charge attack killed
                    // the target, so the reaction was surplus and vanilla would have refused it anyway.
                    if (!IsUsable(pending.Attacker) || !IsUsable(pending.Target))
                    {
                        Deferred.RemoveAt(i);
                        continue;
                    }

                    if (HasPendingChargeAttack(pending.Attacker) && now < pending.Deadline)
                        continue;

                    // Remove before firing so the re-issued call can't re-add itself
                    Deferred.RemoveAt(i);
                    Refire(pending);
                }
            }
            catch (Exception e)
            {
                Main.log.Log("ChargeAooDeferFix (tick) error: " + e);
            }
        }

        private static void Refire(PendingAoO pending)
        {
            Refiring = true;
            try
            {
                pending.Attacker.CombatState.AttackOfOpportunity(
                    pending.Target,
                    pending.Trickster,
                    simulate: false,
                    disengaging: false,
                    afterDefensivelyFail: pending.AfterDefensivelyFail
                );
            }
            catch (Exception e)
            {
                Main.log.Log("ChargeAooDeferFix (refire) error: " + e);
            }
            finally
            {
                Refiring = false;
            }
        }

        private static bool HasPendingChargeAttack(UnitEntityData unit)
        {
            return unit.Commands?.Attack is UnitAttack attack
                && attack.IsCharge
                && !attack.IsActed
                && attack.Result == UnitCommand.ResultType.None;
        }

        private static bool IsAlreadyDeferred(UnitEntityData attacker, UnitEntityData target)
        {
            foreach (var t in Deferred)
            {
                if (t.Attacker == attacker && t.Target == target)
                    return true;
            }

            return false;
        }

        private static bool IsUsable(UnitEntityData unit)
        {
            return unit != null && unit.IsInState && !unit.State.IsDead;
        }
    }
}
