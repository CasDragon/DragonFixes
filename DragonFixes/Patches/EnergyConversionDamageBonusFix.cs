using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Parts;

namespace DragonFixes.Patches
{
    /// <summary>
    /// AdditionalDiceOnDamage (used by Ring of Pyromania) reads the RuleDealDamage bundle's
    /// EnergyType in OnEventAboutToTrigger, before ChangeSpellElementalDamage's nested
    /// RulePrepareDamage rule rewrites it. A spell converted to Fire never procs the bonus, and
    /// a spell converted away from Fire still procs it, since the check always sees the
    /// pre-conversion type.
    ///
    /// This backfills the same rewrite early whenever the caster has an active elemental
    /// conversion, so the vanilla check runs against the correct data.
    /// </summary>
    [HarmonyPatch]
    internal class EnergyConversionDamageBonusFix
    {
        [HarmonyPatch(typeof(AdditionalDiceOnDamage), nameof(AdditionalDiceOnDamage.OnEventAboutToTrigger), typeof(RuleDealDamage)), HarmonyPrefix]
        private static void ApplyConversionEarly(AdditionalDiceOnDamage __instance, RuleDealDamage evt)
        {
            if (!__instance.CheckEnergyDamageType)
            {
                return;
            }

            BlueprintAbility blueprintAbility = evt.Reason.Ability?.Blueprint ?? evt.Reason.Context?.SourceAbility;
            if (blueprintAbility is not { IsSpell: true })
            {
                return;
            }

            SpellDescriptorComponent descriptorComponent = blueprintAbility.GetComponent<SpellDescriptorComponent>();
            if (descriptorComponent == null)
            {
                return;
            }

            SpellDescriptor sourceDescriptor = descriptorComponent.Descriptor.Value;
            SpellDescriptor actualDescriptor = UnitPartChangeSpellElementalDamage.ReplaceSpellDescriptorIfCan(evt.Initiator, sourceDescriptor);
            if (actualDescriptor == sourceDescriptor)
            {
                return;
            }

            DamageEnergyType? target = ToDamageEnergyType(actualDescriptor);
            if (target == null)
            {
                return;
            }

            foreach (BaseDamage item in evt.DamageBundle)
            {
                if (item is EnergyDamage energyDamage && energyDamage.EnergyType != target)
                {
                    energyDamage.ReplaceEnergy(target.Value);
                }
            }
        }

        private static DamageEnergyType? ToDamageEnergyType(SpellDescriptor descriptor)
        {
            if (descriptor.HasAnyFlag(SpellDescriptor.Fire)) return DamageEnergyType.Fire;
            if (descriptor.HasAnyFlag(SpellDescriptor.Cold)) return DamageEnergyType.Cold;
            if (descriptor.HasAnyFlag(SpellDescriptor.Electricity)) return DamageEnergyType.Electricity;
            if (descriptor.HasAnyFlag(SpellDescriptor.Acid)) return DamageEnergyType.Acid;
            if (descriptor.HasAnyFlag(SpellDescriptor.Sonic)) return DamageEnergyType.Sonic;
            return null;
        }
    }
}
