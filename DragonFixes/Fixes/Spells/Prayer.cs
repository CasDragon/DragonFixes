using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.AVEx;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using DragonLibrary.Utils;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components;

namespace DragonFixes.Fixes.Spells;

public class Prayer
{
    [DragonConfigure]
    public static void PatchPrayer()
    {
        Main.log.Log("Patching Prayer to be extendable.");
        var pray = AbilityRefs.Prayer.Reference.Get();
        DragonHelpers.RemoveComponent<AbilityEffectRunAction>(pray);
        AbilityConfigurator.For(pray)
            .AddAbilityEffectRunAction(savingThrowType: SavingThrowType.Unknown,
                actions: ActionsBuilder.New()
                    .Conditional(conditions: ConditionsBuilder.New()
                            .IsAlly().Build(),
                        ifTrue: ActionsBuilder.New()
                            .SpawnFx("8bd36267b09ec344f9ab532a20b6bbf1")
                            .ApplyBuff(BuffRefs.PrayerBuff.Reference.Get(),
                                ContextDuration.Variable(ContextValues.Rank(AbilityRankType.Default), isExtendable: true),
                                asChild: true),
                        ifFalse: ActionsBuilder.New()
                            .SpawnFx("dc41ce9fbc811194abad15f2e7db6f53")
                            .ApplyBuff(BuffRefs.PrayerDebuff.Reference.Get(),
                                ContextDuration.Variable(ContextValues.Rank(AbilityRankType.Default), isExtendable: true),
                                asChild: true)
                    )
            )
            .AddContextRankConfig(ContextRankConfigs.CasterLevel())
            .AddToAvailableMetamagic(Metamagic.Extend)
            .Configure();
    }
}