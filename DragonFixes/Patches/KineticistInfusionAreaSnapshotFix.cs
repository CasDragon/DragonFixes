using HarmonyLib;
using Kingmaker.ElementsSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Patches
{
    /// <summary>
    /// Switching a kineticist's substance infusion while a Wall, Cloud, or other area is
    /// already up changes that area's ongoing behaviour to the new infusion instead of the one
    /// active when it was cast. The switch never pays burn either. The same defect also runs the
    /// other way. A standing area keeps applying the infusion it was cast with to the caster's
    /// other, unrelated blasts and areas.
    ///
    /// AddKineticistInfusionDamageTrigger reacts to damage both from the live infusion buff on
    /// the caster and from a snapshot AreaEffectEntityData of that buff when the area
    /// spawns. Its check for skipping the live buff on area damage only applies when
    /// TriggerOnDirectDamage is false. That's never true here. Both copies always fire, and the
    /// snapshot has no check of its own to confirm the damage came from its own area.
    ///
    /// This patch skips the live buff's copy for damage that came from any area, and skips a
    /// snapshot's copy for damage that did not come from the specific area it belongs to.
    /// AreaEffectContextData identifies which area, if any, is currently running its own effect.
    /// </summary>
    [HarmonyPatch]
    internal class KineticistInfusionAreaSnapshotFix
    {
        [HarmonyPatch(typeof(AddKineticistInfusionDamageTrigger), nameof(AddKineticistInfusionDamageTrigger.Apply)), HarmonyPrefix]
        private static bool Apply_Prefix(RulebookTargetEvent evt, MechanicsContext context)
        {
            bool isSnapshotInvocation = context?.AssociatedBlueprint is BlueprintAbilityAreaEffect;

            if (isSnapshotInvocation)
            {
                var runningArea = ContextData<AreaEffectContextData>.Current?.Entity;
                if (runningArea?.Context != context)
                    return false;
            }
            else
            {
                bool damageFromArea = (evt as RuleDealDamage)?.SourceArea != null
                    || evt?.Reason?.Context?.AssociatedBlueprint is BlueprintAbilityAreaEffect;
                if (damageFromArea)
                    return false;
            }

            return true;
        }
    }
}
