using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonFixes.Util;
using DragonLibrary.BPCoreExtensions;
using DragonLibrary.NewComponents;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Utility;
using UnityEngine;

namespace DragonFixes.Fixes.Classes;

/// <summary>
/// The standard IncreaseCasterLevel has no ability type filter, so it would boost every ability
/// while the buff is active. Instead of that, this is a light custom component that restricts the
/// bonus to Type == Spell.
///
/// This matches Conduit Surge's own AfterCast removal trigger, so the bonus and the buff's
/// removal stay in sync.
/// </summary>


/// <summary>
/// Conduit Surge's own text boosts "her own spells" and staggers her "after performing a conduit
/// surge" for the level of "the spell cast" - both phrases only make sense for a real spell that
/// got cast. Vanilla ConduitSurge instead fires unconditionally on every RuleCastSpell. Scrolls, wands,
/// and potions counts, and a failed cast still staggers. This restricts the drawback, and the surge's
/// consumption, to a successfully cast non-item spell.
/// </summary>
[ComponentName("Conduit Surge drawback and consumption, real spells only")]
[AllowedOn(typeof(BlueprintUnitFact), false)]
[TypeId("a7c19e4d5b2f4c3a9e6d1f8b2c4a6e01")]
public class ConduitSurgeOnRealSpellCast : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCastSpell>
{
    [SerializeField]
    public ContextValue Value;
    [SerializeField]
    public BlueprintBuffReference Buff;

    public void OnEventAboutToTrigger(RuleCastSpell evt)
    {
    }

    public void OnEventDidTrigger(RuleCastSpell evt)
    {
        bool isRealSpell = SpellSourceTypeHelper.CheckSourceItemType(evt.Spell,
            SpellSourceTypeFlag.Potion | SpellSourceTypeFlag.Scroll | SpellSourceTypeFlag.Wand,
            exclude: true);
        if (!isRealSpell || !evt.Success)
        {
            return;
        }

        int spellLevel = evt.Spell.SpellLevel;
        int staggerDurationRounds = spellLevel * 10;
        int dc = 10 + spellLevel + Value.Calculate(Context);
        var savingThrow = new RuleSavingThrow(Owner, SavingThrowType.Fortitude, dc);
        Rulebook.Trigger(savingThrow);
        if (!savingThrow.IsPassed)
        {
            Owner.AddBuff(Buff.Get(), Owner, staggerDurationRounds.Rounds().Seconds);
        }

        Owner.Buffs.GetBuff(BuffRefs.LeyLineGuardianConduitSurgeBuff.Reference.Get())?.Remove();
        Owner.Buffs.GetBuff(BuffRefs.LeyLineGuardianConduitSurgeBuffEffect.Reference.Get())?.Remove();
    }
}

public class LeyLineGuardianConduitSurge
{
    /// <summary>
    /// Conduit Surge's caster level bonus never applies. RuleCastSpell freezes the spell's
    /// AbilityParams before the surge buff's AfterCast=false trigger applies the effect buff's
    /// IncreaseCasterLevel effect, so the bonus arrives one phase too late for the cast that
    /// triggered it.
    ///
    /// This moves the rank/roll/CL components onto the Surge buff instead, so they exist before
    /// the next spell's params are calculated. The effect buff still inherits the shared value
    /// from its parent context, so the save DC stays tied to the same roll that boosts CL.
    /// </summary>
    [DragonConfigure]
    public static void FixConduitSurgeCasterLevelTiming()
    {
        Main.log.Log("Fixing Ley Line Guardian's Conduit Surge to actually apply its caster level bonus.");
        var surgeBuff = BuffRefs.LeyLineGuardianConduitSurgeBuff.Reference.Get();
        var effect = BuffRefs.LeyLineGuardianConduitSurgeBuffEffect.Reference.Get();

        var ranks = effect.GetComponents<ContextRankConfig>().ToArray();
        var sharedValue = effect.GetComponent<ContextCalculateSharedValue>();
        var increaseCasterLevel = effect.GetComponent<IncreaseCasterLevel>();
        if (ranks.Length != 1 || sharedValue == null || increaseCasterLevel == null)
        {
            Main.log.Log("Conduit Surge: expected exactly one ContextRankConfig plus the other components on the effect buff, skipping fix.");
            return;
        }
        var rank = ranks[0];

        DragonHelpers.RemoveComponent(effect, increaseCasterLevel);
        var spellOnlyCasterLevel = new IncreaseCasterLevelForSpells
        {
            name = increaseCasterLevel.name,
            Value = increaseCasterLevel.Value,
            Descriptor = increaseCasterLevel.Descriptor,
        };

        BuffConfigurator.For(surgeBuff)
            .MoveComponent(effect, surgeBuff, rank)
            .MoveComponent(effect, surgeBuff, sharedValue)
            .MoveComponent(effect, surgeBuff, spellOnlyCasterLevel)
            .Configure();
    }

    /// <summary>
    /// Conduit Surge boosts and drains "her own spells". Its AddAbilityUseTrigger components
    /// and its ConduitSurge drawback (effect buff) never check the source item, though. A scroll,
    /// wand, or potion counts as "the next spell she casts" too, and the drawback fires even on a
    /// failed cast. This excludes item-sourced casts from the armed buff's triggers, and swaps
    /// the effect buff's drawback for one that also requires the cast to have succeeded.
    /// </summary>
    [DragonConfigure]
    public static void FixConduitSurgeRealSpellsOnly()
    {
        Main.log.Log(
            "Fixing Ley Line Guardian's Conduit Surge to ignore scrolls/wands/potions and failed casts.");
        var surgeBuff = BuffRefs.LeyLineGuardianConduitSurgeBuff.Reference.Get();
        var effect = BuffRefs.LeyLineGuardianConduitSurgeBuffEffect.Reference.Get();

        var drawbacks = effect.GetComponents<ConduitSurge>().ToArray();
        if (drawbacks.Length != 1)
        {
            Main.log.Log(
                "Conduit Surge: expected exactly one ConduitSurge component on the effect buff, skipping fix.");
            return;
        }
        var drawback = drawbacks[0];

        DragonHelpers.RemoveComponent(effect, drawback);
        var realSpellsOnlyDrawback = new ConduitSurgeOnRealSpellCast
        {
            name = drawback.name,
            Value = drawback.Value,
            Buff = drawback.m_Buff,
        };
        BuffConfigurator.For(effect)
            .MoveComponent(effect, effect, realSpellsOnlyDrawback)
            .Configure();

        BuffConfigurator.For(surgeBuff)
            .EditComponents<AddAbilityUseTrigger>(t =>
            {
                t.CheckSourceItemType = true;
                t.SourceItemTypeExclude = true;
                t.SourceItemType = SpellSourceTypeFlag.Potion | SpellSourceTypeFlag.Scroll |
                                   SpellSourceTypeFlag.Wand;
            }, t => true)
            .Configure();
    }
}