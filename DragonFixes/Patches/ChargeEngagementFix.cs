using System;
using DragonLibrary.Utils;
using HarmonyLib;
using Kingmaker.Armies.TacticalCombat;
using Kingmaker.Controllers.Combat;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items.Slots;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Commands;

namespace DragonFixes.Patches
{
    /// <summary>
    /// If the opening action of combat is a charge, the attack resolves before the
    /// notice/join/engage cascade runs (the enemy's group only learns of the attacker from the
    /// attack roll itself), so engagement registers a frame late and Outflank, Opportunist etc
    /// effect that needs EngagedBy then silently skips the opportunity attack. This sets engagement
    /// for the charging unit and its rider/mount partner right before the charge attack's rules
    /// trigger, using the same reach and concealment checks UnitCombatEngagementController.TickUnit
    /// itself uses, except for its IsInCombat check, which is the part that cannot be satisfied yet
    /// on a combat opener.
    /// </summary>
    [HarmonyPatch]
    [HarmonyPatchCategory(SettingName)]
    internal class ChargeEngagementFix
    {
        private const string SettingName = "chargeengagement";
        private const string SettingDescription = "Charge: attacks that open combat register engagement immediately, so Outflank and other teamwork feats still trigger";

        public static void ChangePatchStatus(bool enabled)
        {
            if (enabled)
            {
                Main.log.Log("ChargeEngagementFix enabled");
                Main.HarmonyInstance.PatchCategory(SettingName);
            }
            else
            {
                Main.log.Log("ChargeEngagementFix disabled");
                Main.HarmonyInstance.UnpatchCategory(SettingName);
            }
        }

        [DragonSetting(SettingCategories.None, SettingName, SettingDescription, typeof(ChargeEngagementFix), nameof(ChangePatchStatus))]
        [DragonConfigure]
        private static void ApplyPatch()
        {
            ChangePatchStatus(SettingsAction.GetSetting<bool>(SettingName));
        }

        [HarmonyPatch(typeof(UnitAttack), "OnAction"), HarmonyPrefix]
        private static void OnAction_Prefix(UnitAttack __instance)
        {
            try
            {
                if (!__instance.IsCharge || TacticalCombatHelper.IsActive)
                    return;

                UnitEntityData target = __instance.TargetUnit;
                UnitEntityData attacker = __instance.Executor;
                if (target == null || attacker == null)
                    return;

                EnsureEngaged(attacker, target);

                // Flanking needs EngagedBy.Count > 1, and Outflank needs the partner in EngagedBy,
                // so the half that is not swinging right now has to be engaged too.
                UnitEntityData partner = attacker.GetRider() ?? attacker.GetSaddledUnit();
                if (partner != null)
                    EnsureEngaged(partner, target);
            }
            catch (Exception e)
            {
                Main.log.Log("ChargeEngagementFix error: " + e);
            }
        }

        // Mirrors UnitCombatEngagementController.TickUnit for one unit/enemy pair, except the unit
        // does not need to be in combat yet. The engagement controller confirms or clears the
        // entry on its own once the join controller catches up a frame later.
        private static void EnsureEngaged(UnitEntityData unit, UnitEntityData target)
        {
            if (!unit.IsInState || !target.IsInState)
                return;
            if (unit.Descriptor.State.IsDead || !target.Descriptor.State.IsConscious)
                return;
            if (!unit.IsEnemy(target) || !unit.Memory.Contains(target))
                return;
            if (unit.CombatState.IsEngage(target))
                return;

            bool canEngage = unit.Descriptor.State.CanAct;
            float threatRange = 0f;
            if (canEngage)
            {
                WeaponSlot threatHand = unit.GetThreatHand();
                float? range = threatHand != null ? unit.GetThreatRange(threatHand) : null;
                canEngage &= range.HasValue;
                if (range.HasValue)
                    threatRange = range.Value;
            }

            if (!unit.IsEngage(target, canEngage, threatRange))
                return;

            unit.CombatState.Engage(target);
        }
    }
}
