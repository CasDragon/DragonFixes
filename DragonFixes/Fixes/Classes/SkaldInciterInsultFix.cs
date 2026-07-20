using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonFixes.Util;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace DragonFixes.Fixes.Classes;

public class SkaldInciterInsultFix
{
    /// <summary>
    /// The Inciter's Insult aura rolls its Will save with HasCustomDC pointing at
    /// ProvocateurInsultProperty (a plain 10 + 1/2 Skald level + CHA stat sum), which never
    /// triggers RuleCalculateAbilityParams. This means caster-side DC bonuses (Glass Amulet of
    /// Clarity's +2 vs mind-affecting, Demon Rage's +2 ability DCs, Aeon gaze, etc.) can
    /// never apply to it.
    ///
    /// The base DC formula (unbuffed) is unchanged.
    ///
    /// Caustic Ridicule's rider and Hit a Nerve's save for half is also fixed this way.
    /// </summary>
    [DragonConfigure]
    public static void FixInsultDc()
    {
        Main.log.Log("Fixing Inciter's Insult DC to go through the rules engine, so DC bonuses (e.g. Glass Amulet of Clarity) apply.");
        var area = AbilityAreaEffectRefs.InsultAreaEffect.Reference.Get();
        foreach (var save in area.FlattenAllActions().OfType<ContextActionSavingThrow>())
        {
            save.HasCustomDC = false;
        }
        AbilityAreaEffectConfigurator.For(area)
            .AddSpellDescriptorComponent(SpellDescriptor.Sonic | SpellDescriptor.MindAffecting | SpellDescriptor.Emotion)
            .AddContextCalculateAbilityParamsBasedOnClass(
                statType: StatType.Charisma,
                characterClass: CharacterClassRefs.SkaldClass.Reference.Get())
            .Configure();
    }
}
