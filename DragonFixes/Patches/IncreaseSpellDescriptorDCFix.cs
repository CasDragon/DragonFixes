using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;

namespace DragonFixes.Patches
{
    // IncreaseSpellDescriptorDC (Glass Amulet of Clarity, Aspect of the Asp, etc.) reads spell
    // descriptors (Mind-affection, poison, etc) from evt.Spell is declared as a BlueprintAbility.
    // However some abilities are not defined using BlueprintAbility. Examples:
    // * Auras
    //   * Thundercaller - Storm Call (not buffed by elec descriptors)
    //   * Thundercaller/Skald - Incite Rage (not buffed by mind-affecting)
    //   * Skald - Insult/Caustic Ridicule/Hit a Nerve (mind-affecting again)
    //   * Aivu - Frightful Presence Aura
    //   * Druid - Auras on elemental shifts
    // * Buffs (this are _quite_ niche)
    //   * Flesheater - poison enemies after eating
    //   * Alchemist - Nauseating Flesh
    //
    // The evt.Spell was effectively null in these cases. We fallback to Blueprint here to help
    // fix this problem
    [HarmonyPatch]
    internal class IncreaseSpellDescriptorDCFix
    {
        [HarmonyPatch(typeof(IncreaseSpellDescriptorDC), nameof(IncreaseSpellDescriptorDC.OnEventAboutToTrigger)), HarmonyPostfix]
        private static void NonAbilityDescriptorDC(RuleCalculateAbilityParams evt, IncreaseSpellDescriptorDC __instance)
        {
            // Already done by the original class
            if (evt.Spell != null)
            {
                return;
            }
            // Glass Amulet of Clarity should work because it has the SpellsOnly false flag
            // We need to check again here so we don't expand the scope of SpellsOnly true items
            if (__instance.SpellsOnly && evt.Spellbook == null)
            {
                return;
            }
            // Ensuring the Blueprint actually cares about SpellDescriptors in the first place
            SpellDescriptorComponent component = evt.Blueprint.GetComponent<SpellDescriptorComponent>();
            if (component == null)
            {
                return;
            }
            // Apply our fix
            SpellDescriptor descriptor = UnitPartChangeSpellElementalDamage.ReplaceSpellDescriptorIfCan(__instance.Owner, component.Descriptor.Value);
            if (descriptor.HasAnyFlag(__instance.Descriptor))
            {
                evt.AddBonusDC(__instance.BonusDC, __instance.ModifierDescriptor);
            }
        }

        // Same null-Spell error in the ContextValue-based sibling component
        [HarmonyPatch(typeof(IncreaseSpellContextDescriptorDC), nameof(IncreaseSpellContextDescriptorDC.OnEventAboutToTrigger)), HarmonyPostfix]
        private static void NonAbilityContextDescriptorDC(RuleCalculateAbilityParams evt, IncreaseSpellContextDescriptorDC __instance)
        {
            // Already done by the original class
            if (evt.Spell != null)
            {
                return;
            }
            // Glass Amulet of Clarity should work because it has the SpellsOnly false flag
            // We need to check again here so we don't expand the scope of SpellsOnly true items
            if (__instance.SpellsOnly && evt.Spellbook == null)
            {
                return;
            }
            // Ensuring the Blueprint actually cares about SpellDescriptors in the first place
            SpellDescriptorComponent component = evt.Blueprint.GetComponent<SpellDescriptorComponent>();
            if (component == null)
            {
                return;
            }
            // Apply our fix
            SpellDescriptor descriptor = UnitPartChangeSpellElementalDamage.ReplaceSpellDescriptorIfCan(__instance.Owner, component.Descriptor.Value);
            if (descriptor.HasAnyFlag(__instance.Descriptor))
            {
                evt.AddBonusDC(__instance.Value.Calculate(__instance.Context), __instance.ModifierDescriptor);
            }
        }
    }
}
