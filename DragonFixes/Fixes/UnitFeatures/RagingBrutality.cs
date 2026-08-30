using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.UnitFeatures;

public class RagingBrutality
{
    [DragonConfigure]
    public static void PatchRagingBrutalityAbility()
    {
        Main.log.Log("Patching RagingBrutalityAbility to include more rages.");
        AbilityConfigurator.For(AbilityRefs.RagingBrutalityAbility)
            .EditComponent<AbilityTargetHasFact>(EditAbilityTargetHasFact)
            .Configure();
    }

    public static void EditAbilityTargetHasFact(AbilityTargetHasFact component)
    {
        component.m_CheckedFacts =
        [
            .. component.m_CheckedFacts,
            BuffRefs.InciteRageEffectBuff.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.ElementalRampagerRampageBuff.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.RageSpellBuff.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.Gorum_Buff.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.InspiredRageEffectBuffMythic.Reference.Get().ToReference<BlueprintUnitFactReference>()
        ];
    }
}