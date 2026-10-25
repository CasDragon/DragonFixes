using System.Linq;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.Items;

public class HolyBlare
{
    [DragonConfigure]
    public static void AddEnchant()
    {
        Main.log.Log("Adding the enchant to Holy Blare.");
        var enchant =
            WeaponEnchantmentRefs.EarthBreakerHolyEnchantment.Reference.Get();
        var ability = AbilityRefs.HolyBlareAbility.Reference.Get();
        // add enchant
        ItemWeaponConfigurator.For(ItemWeaponRefs.HolyBlareItem)
            .AddToEnchantments(enchant)
            .Configure();
        // fix enchant
        if (enchant.Components.FirstOrDefault(c => c is AddInitiatorAttackWithWeaponTrigger) 
            is not AddInitiatorAttackWithWeaponTrigger comp)
        {
            Main.log.Log("no component on Holy Blare, skipping");
            return;
        }
        var castSpell = comp!.Action.Actions.First(a => a is ContextActionCastSpell) as ContextActionCastSpell;
        castSpell?.m_Spell = ability.ToReference<BlueprintAbilityReference>();
        // fix ability
        var abilityRun = ability.Components.First(c => c is AbilityEffectRunAction) as AbilityEffectRunAction;
        var actions = ActionsBuilder.New().
            Conditional(
                ConditionsBuilder.New().
                    Alignment(AlignmentComponent.Evil, checkCaster: false),
                        ifTrue: ActionsBuilder.New()
                            .ConditionalSaved(
                            ActionsBuilder.New()
                                .ApplyBuff(BuffRefs.HolyBlareBuff.ToString(),
                                    durationValue: ContextDuration.FixedDice(DiceType.D4, 1, rate: DurationRate.Rounds),
                                    asChild: true))).Build();
        abilityRun!.Actions = actions;
    }
}