using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using DragonFixes.Util;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.Items;

public class Devitalizer
{
    internal const string devitalizerbuffname = "Devitalizer";
    internal const string devitalizerbuffdescription = "+2 Circumstance bonus to attack and damage rolls against Exhausted enemies.";

    [DragonLocalizedString(devitalizerbuffnamekey, devitalizerbuffname)]
    internal const string devitalizerbuffnamekey = "devitalizerbuff.name";

    [DragonLocalizedString(devitalizerbuffdescriptionkey, devitalizerbuffdescription, true)]
    internal const string devitalizerbuffdescriptionkey = "devitalizerbuff.description";

    [DragonConfigure]
        public static void PatchDevitalizer()
        {
            Main.log.Log("Patching Devitalizer to actually work?");
            BlueprintBuff buff = BuffConfigurator.New("devitalizerbuff", Guids.DevitalizerBuff)
                .SetDisplayName(devitalizerbuffnamekey)
                .SetDescription(devitalizerbuffdescriptionkey)
                .AddContextStatBonus(StatType.AdditionalAttackBonus, ContextValues.Constant(2), ModifierDescriptor.Circumstance)
                .AddContextStatBonus(StatType.AdditionalDamage, ContextValues.Constant(2), ModifierDescriptor.Circumstance)
                .AddRemoveBuffOnAttack()
                .Configure();
            BlueprintWeaponEnchantment enchant = WeaponEnchantmentRefs.DevitalizerEnchantment.Reference.Get();
            DragonHelpers.RemoveComponent(enchant, enchant.GetComponent<AddInitiatorAttackWithWeaponTrigger>());
            WeaponEnchantmentConfigurator.For(enchant)
                .AddInitiatorAttackWithWeaponTrigger(onlyHit: true, action:
                    ActionsBuilder.New()
                        .Conditional(ConditionsBuilder.New()
                                .HasBuffWithDescriptor(spellDescriptor: SpellDescriptor.Exhausted),
                            ifTrue:
                            ActionsBuilder.New()
                                .ApplyBuff(buff, new ContextDurationValue()
                                    {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = ContextValues.Constant(0),
                                        BonusValue = ContextValues.Constant(1)
                                    },
                                    asChild: true, toCaster: true)),
                    triggerBeforeAttack: true)
                .Configure();
        }
}