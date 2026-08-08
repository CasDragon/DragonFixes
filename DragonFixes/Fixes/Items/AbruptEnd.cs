using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonFixes.Util;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;

namespace DragonFixes.Fixes.Items;

public class AbruptEnd
{
    internal const string abruptendbuffname = "Abrupt End";
    internal const string abruptendbuffdescription = "+2 Insight bonus to attack rolls.";

    [DragonLocalizedString(abruptendbuffnamekey, abruptendbuffname)]
    internal const string abruptendbuffnamekey = "abruptendbuff.name";

    [DragonLocalizedString(abruptendbuffdescriptionkey, abruptendbuffdescription, true)]
    internal const string abruptendbuffdescriptionkey = "abruptendbuff.description";

    [DragonConfigure]
    public static void PatchAbruptEndEnchant()
    {
        Main.log.Log("Patching Abrupt End to actually work?");
        BlueprintBuff buff = BuffConfigurator.New("abruptendbuff", Guids.EbruptEndBuff)
            .SetDisplayName(abruptendbuffnamekey)
            .SetDescription(abruptendbuffdescriptionkey)
            .AddContextStatBonus(StatType.AdditionalAttackBonus, ContextValues.Constant(2), ModifierDescriptor.Insight)
            .AddRemoveBuffOnAttack()
            .Configure();
        BlueprintWeaponEnchantment enchant = WeaponEnchantmentConfigurator.For(WeaponEnchantmentRefs.AbruptEndEnchantment)
            .AddInitiatorAttackWithWeaponTrigger(onlyHit: true, action:
                ActionsBuilder.New()
                    .ApplyBuff(buff, new ContextDurationValue()
                        {
                            Rate = DurationRate.Rounds,
                            DiceType = DiceType.Zero,
                            DiceCountValue = ContextValues.Constant(0),
                            BonusValue = ContextValues.Constant(1)
                        },
                        asChild: true, toCaster: true), triggerBeforeAttack: true)
            .Configure();
    }
}