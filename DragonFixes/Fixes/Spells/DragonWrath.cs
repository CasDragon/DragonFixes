using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using TabletopTweaks.Core.MechanicsChanges;

namespace DragonFixes.Fixes.Spells;

public class DragonWrath
{
        [DragonConfigure]
        public static void PatchDragonWrath()
        {
            Main.log.Log("Buffing DragonWrath spell to have available metamagics.");
            Metamagic metas = Metamagic.CompletelyNormal | Metamagic.Reach | Metamagic.Empower | Metamagic.Bolstered | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Intensified;
            if (ModCompat.tttbase)
            {
                metas = metas | (Metamagic)(MetamagicExtention.CustomMetamagic.Burning | MetamagicExtention.CustomMetamagic.ElementalAcid |
                    MetamagicExtention.CustomMetamagic.ElementalCold | MetamagicExtention.CustomMetamagic.ElementalElectricity |
                    MetamagicExtention.CustomMetamagic.ElementalFire | MetamagicExtention.CustomMetamagic.Flaring | MetamagicExtention.CustomMetamagic.Rime);
            }
            var ab = AbilityRefs.DragonWrath.Reference.Get();
            var gab = AbilityRefs.DragonWrathGold.Reference.Get();
            DragonHelpers.RemoveComponent<AbilityEffectRunAction>(ab);
            DragonHelpers.RemoveComponent<ContextRankConfig>(ab);
            AbilityConfigurator.For(ab)
                .SetAvailableMetamagic(metas)
                .AddAbilityEffectRunAction(savingThrowType: Kingmaker.EntitySystem.Stats.SavingThrowType.Unknown,
                    actions:
                        ActionsBuilder.New()
                            .Conditional(
                                conditions: ConditionsBuilder.New().CasterHasFact(FeatureRefs.CorruptedGoldenDragonFeature.Reference.Get()).Build(),
                                ifTrue: 
                                    ActionsBuilder.New()
                                        .DealDamage(damageType: DamageTypes.Energy(DamageEnergyType.Fire),
                                            ContextDice.Value(DiceType.D6, ContextValues.Rank()),
                                            half: true, addAdditionalDamage: true, addFavoredEnemyDamage: true,
                                            writeRawResultToSharedValue: true, resultSharedValue: AbilitySharedValue.Damage,
                                            criticalSharedValue: AbilitySharedValue.Damage)
                                        .DealDamage(damageType: DamageTypes.Energy(DamageEnergyType.Unholy),
                                            ContextDice.Value(DiceType.D6, ContextValues.Rank()),
                                            half: true, addAdditionalDamage: true, addFavoredEnemyDamage: true,
                                            writeRawResultToSharedValue: true, resultSharedValue: AbilitySharedValue.Damage,
                                            criticalSharedValue: AbilitySharedValue.Damage)
                                        .Add(new ContextActionDisableBonusForDamage()
                                        {
                                            DisableAdditionalDamage = false,
                                            DisableAdditionalDice = false,
                                            DisableFavoredEnemyDamage = false,
                                            DisableSneak = true
                                        }),
                                ifFalse:
                                    ActionsBuilder.New()
                                        .DealDamage(damageType: DamageTypes.Energy(DamageEnergyType.Fire),
                                            ContextDice.Value(DiceType.D6, ContextValues.Rank()),
                                            half: true, addAdditionalDamage: true, addFavoredEnemyDamage: true,
                                            writeRawResultToSharedValue: true, resultSharedValue: AbilitySharedValue.Damage,
                                            criticalSharedValue: AbilitySharedValue.Damage)
                                        .DealDamage(damageType: DamageTypes.Energy(DamageEnergyType.Holy),
                                            ContextDice.Value(DiceType.D6, ContextValues.Rank()),
                                            half: true, addAdditionalDamage: true, addFavoredEnemyDamage: true,
                                            writeRawResultToSharedValue: true, resultSharedValue: AbilitySharedValue.Damage,
                                            criticalSharedValue: AbilitySharedValue.Damage)
                                        .Add(new ContextActionDisableBonusForDamage()
                                        {
                                            DisableAdditionalDamage = false,
                                            DisableAdditionalDice = false,
                                            DisableFavoredEnemyDamage = false,
                                            DisableSneak = true
                                        })
                                        ))
                .AddComponent(gab.GetComponent<AbilityDeliverProjectile>())
                .AddComponent(gab.GetComponent<ContextRankConfig>())
                .Configure();
        }
}