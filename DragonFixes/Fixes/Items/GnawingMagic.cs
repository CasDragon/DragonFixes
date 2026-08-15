using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;

namespace DragonFixes.Fixes.Items;

public class GnawingMagic
{
    [DragonConfigure]
    public static void PatchGnawingHunger()
    {
        Main.log.Log("Patching Gnawing Hunger to actually apply debuff to enemy?");
        BlueprintFeature bp = FeatureRefs.GnawingMagicFeature.Reference.Get();
        DragonHelpers.RemoveComponent(bp, bp.GetComponent<AddAbilityUseTrigger>());
        FeatureConfigurator.For(bp)
            .AddAbilityUseTrigger(action:
                ActionsBuilder.New().ApplyBuff(BuffRefs.GnawingMagicBuffEnemy.Reference.Get(),
                        new ContextDurationValue()
                        {
                            Rate = DurationRate.Rounds,
                            DiceType = DiceType.Zero,
                            DiceCountValue = ContextValues.Constant(0),
                            BonusValue = ContextValues.Constant(3)
                        }, asChild: true, toCaster: false)
                    .ApplyBuff(BuffRefs.GnawingMagicBuffSelf.Reference.Get(),
                        new ContextDurationValue()
                        {
                            Rate = DurationRate.Rounds,
                            DiceType = DiceType.Zero,
                            DiceCountValue = ContextValues.Constant(0),
                            BonusValue = ContextValues.Constant(3)
                        }, asChild: true, toCaster: true),
                actionsOnTarget: true,
                checkAbilityType: true,
                type: AbilityType.Spell)
            .Configure();
    }
}