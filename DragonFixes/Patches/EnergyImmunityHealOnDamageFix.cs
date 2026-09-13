using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonFixes.Patches
{
    /// <summary>
    /// AddEnergyDamageImmunity's HealOnDamage (Fiery Body, the Dragonblood Shifter aspects) is meant
    /// to heal a fraction of the damage it prevented. RuleCalculateDamage.CalculateDamageValue zeroes
    /// out immune damage before it ever reaches DamageValue.FinalValue, so the healing handler on
    /// RuleDealDamage is always zero and never triggers.
    ///
    /// The fix lets CalculateDamageValue run against the original value whenever the immunity came
    /// from a HealOnDamage component, stashes that value, then nullifies the result to reproduce
    /// vanilla's own output everywhere else, then replaces the heal calculation to use the stash.
    /// </summary>
    [HarmonyPatch]
    internal class EnergyImmunityHealOnDamageFix
    {
        // Value plus which facts have already healed off it.
        private sealed class PreImmunityStash
        {
            public int Value;
            public readonly HashSet<object> Consumers = [];
        }

        private static readonly ConditionalWeakTable<BaseDamage, PreImmunityStash> PreImmunityValues =
            new ConditionalWeakTable<BaseDamage, PreImmunityStash>();

        [HarmonyPatch(typeof(RuleCalculateDamage), "CalculateDamageValue"), HarmonyPrefix]
        private static void CalculateDamageValue_Prefix(BaseDamage damage, out ValueWithSource<DamageDeclineType> __state)
        {
            __state = null;
            try
            {
                if (damage is not EnergyDamage energyDamage || damage.IgnoreImmunities)
                {
                    return;
                }

                if (!damage.Immune)
                {
                    return;
                }

                if (!HasHealOnDamageSource(damage, energyDamage.EnergyType))
                {
                    return;
                }

                __state = damage.m_Decline;
                damage.m_Decline = new ValueWithSource<DamageDeclineType>(DamageDeclineType.None);
            }
            catch (Exception e)
            {
                Main.log.Log("EnergyImmunityHealOnDamageFix prefix error: " + e);
                __state = null;
            }
        }

        [HarmonyPatch(typeof(RuleCalculateDamage), "CalculateDamageValue"), HarmonyPostfix]
        private static void CalculateDamageValue_Postfix(BaseDamage damage, ValueWithSource<DamageDeclineType> __state, ref DamageValue __result)
        {
            if (__state == null)
            {
                return;
            }

            // Restore the real decline first, unconditionally, so damage's cached state can never be
            // left wrong for the rest of the pipeline even if the stash/rebuild below throws.
            damage.m_Decline = __state;

            try
            {
                PreImmunityStash stash = PreImmunityValues.GetOrCreateValue(damage);
                stash.Value = __result.ValueWithoutReduction;
                stash.Consumers.Clear();
            }
            catch (Exception e)
            {
                Main.log.Log("EnergyImmunityHealOnDamageFix postfix error: " + e);
            }

            __result = new DamageValue(damage, 0, __result.RollAndBonusValue, __result.RollResult, __result.TacticalCombatDRModifier);
        }

        [HarmonyPatch(typeof(AddEnergyDamageImmunity), nameof(AddEnergyDamageImmunity.OnEventDidTrigger), typeof(RuleDealDamage)), HarmonyPrefix]
        private static bool OnEventDidTrigger_Prefix(AddEnergyDamageImmunity __instance, RuleDealDamage evt)
        {
            if (!__instance.HealOnDamage)
            {
                return true;
            }

            try
            {
                // Damage previews/tooltips reuse this same handler chain with IsFake set, so healing
                // should not apply here. Calculate and ResultList null covers the untargetable
                // target and empty bundle guard clauses in RuleDealDamage.OnTrigger.
                if (evt.IsFake || evt.Calculate == null || evt.ResultList == null)
                {
                    return false;
                }

                int total = 0;
                foreach (DamageValue result in evt.ResultList)
                {
                    if ((result.Source as EnergyDamage)?.EnergyType != __instance.EnergyType)
                    {
                        continue;
                    }

                    if (result.Source.Immune && !result.Source.IgnoreImmunities
                        && PreImmunityValues.TryGetValue(result.Source, out PreImmunityStash stash)
                        && stash.Consumers.Add(__instance.Fact))
                    {
                        int maximum = result.Source.MaximumValue ?? int.MaxValue;
                        total += Math.Max(0, Math.Min(maximum, stash.Value - result.Reduction));
                    }
                    else
                    {
                        total += result.FinalValue;
                    }
                }

                int healValue = AddEnergyDamageImmunity.GetHealValue(total, __instance.m_HealRate);
                if (healValue > 0)
                {
                    Rulebook.Trigger(new RuleHealDamage(__instance.Owner, __instance.Owner, healValue)
                    {
                        SourceFact = __instance.Fact
                    });
                }

                return false;
            }
            catch (Exception e)
            {
                Main.log.Log("EnergyImmunityHealOnDamageFix OnEventDidTrigger error: " + e);
                return true;
            }
        }

        private static bool HasHealOnDamageSource(BaseDamage damage, DamageEnergyType energyType)
        {
            foreach (DamageDecline decline in damage.Declines)
            {
                if (decline.Type < DamageDeclineType.Total || decline.SourceFact == null)
                {
                    continue;
                }

                foreach (AddEnergyDamageImmunity component in decline.SourceFact.SelectComponents<AddEnergyDamageImmunity>())
                {
                    if (component.HealOnDamage && component.EnergyType == energyType)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
