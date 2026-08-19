using System;
using System.Runtime.CompilerServices;
using DragonLibrary.Utils;
using HarmonyLib;
using Kingmaker;
using Kingmaker.Controllers.Units;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.UnitLogic.Commands.Base;

namespace DragonFixes.Patches
{
    /// <summary>
    /// Typically, when a mounted pair both attack, such as on a charge, the mount swings before the rider.
    ///
    /// Rider and mount commands link in pairs. The rider's carries .MountCommand, the mount's
    /// carries .RiderCommand back. ShouldStartCommand only ever gates on .MountCommand, holding
    /// the rider until the linked mount command stops approaching. The mount side has no
    /// equivalent gate, so it swings the instant its own approach finishes. This adds the missing
    /// check to the mount side, holding the mount until the rider's half has started.
    /// </summary>
    [HarmonyPatch]
    [HarmonyPatchCategory("mountedriderswingorder")]
    internal class MountedRiderSwingOrderFix
    {
        // A timeout so a stuck rider command can never stall the mount for good. Kept short
        // because holding the mount pre-Start widens the window where a status change, Nauseated
        // or loss of CanAct etc, lets TickCommandTurnBased force-finish its attack as a no-op
        // "Success" before it ever swings. Timing out just lets the mount go, which is the
        // original ordering bug as a worst case.
        private const float MaxWaitForRiderSeconds = 2f;

        private const string SettingName = "mountedriderswingorder";
        private const string SettingDescription = "When attacking on a mount, the rider attacks first. (Base game makes the mount attack first)";
        
        public static void ChangePatchStatus(bool enabled)
        {
            if (enabled)
            {
                Main.log.Log("MountedRiderSwingOrderFix enabled");
                Main.HarmonyInstance.PatchCategory("chargeaoodefer");
            }
            else
            {
                Main.log.Log("MountedRiderSwingOrderFix disabled");
                Main.HarmonyInstance.UnpatchCategory("chargeaoodefer");
            }
        }

        [DragonSetting(SettingCategories.None, SettingName, SettingDescription, typeof(MountedRiderSwingOrderFix), nameof(ChangePatchStatus))]
        [DragonConfigure]
        private static void ApplyPatch()
        {
             ChangePatchStatus(SettingsAction.GetSetting<bool>(SettingName));
        }

        private static readonly ConditionalWeakTable<UnitAttack, StrongBox<TimeSpan>> BlockedSince =
            new ConditionalWeakTable<UnitAttack, StrongBox<TimeSpan>>();

        [HarmonyPatch(typeof(UnitCommandController), nameof(UnitCommandController.ShouldStartCommand)), HarmonyPostfix]
        private static void ShouldStartCommand_Postfix(UnitCommand command, ref bool __result)
        {
            try
            {
                if (!__result || command.MountCommand != null)
                    return;

                if (!(command is UnitAttack mountAttack))
                    return;

                //if (!Enabled())
                //    return;

                UnitAttack riderAttack = command.RiderCommand as UnitAttack ?? FindUnlinkedRiderAttack(mountAttack);
                if (riderAttack == null)
                    return;

                if (riderAttack.IsStarted || riderAttack.Result != UnitCommand.ResultType.None)
                {
                    BlockedSince.Remove(mountAttack);
                    return;
                }

                var riderExecutor = riderAttack.Executor;
                if (riderExecutor == null || !riderExecutor.IsInState || riderExecutor.State.IsDead)
                {
                    BlockedSince.Remove(mountAttack);
                    return;
                }

                TimeSpan now = Game.Instance.TimeController.GameTime;
                StrongBox<TimeSpan> box = BlockedSince.GetOrCreateValue(mountAttack);
                if (box.Value == default)
                    box.Value = now;

                if ((now - box.Value).TotalSeconds > MaxWaitForRiderSeconds)
                {
                    BlockedSince.Remove(mountAttack);
                    return;
                }

                __result = false;
            }
            catch (Exception e)
            {
                Main.log.Log("MountedRiderSwingOrderFix error: " + e);
            }
        }

        // AbilityCustomCharge only links the two halves when the rider already holds an attack
        // command at the instant its charge coroutine ends. So a pair can reach us with no
        // .RiderCommand at all, and an unlinked mount is an ungated mount. We fall back to the
        // rider's own attack on the same target, which is the pairing the linker would have made.
        // Worst case we hold a mount for a rider attack that wasn't really paired with it, which
        // is still bounded by the timeout above.
        private static UnitAttack FindUnlinkedRiderAttack(UnitAttack mountAttack)
        {
            var rider = mountAttack.Executor?.GetRider();
            if (rider == null)
                return null;

            UnitAttack riderAttack = rider.Commands?.Attack;
            if (riderAttack == null || riderAttack.TargetUnit != mountAttack.TargetUnit)
                return null;

            return riderAttack;
        }
    }
}
