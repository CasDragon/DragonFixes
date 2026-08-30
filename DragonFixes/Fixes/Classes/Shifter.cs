using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace DragonFixes.Fixes.Classes;

public class Shifter
{
    [DragonConfigure]
    public static void PatchWyrmShifterRedBreath()
    {
        Main.log.Log("Patching Wyrm Shifter's level 20 breath to correctly be fire damage instead of cold");
        AbilityConfigurator.For(AbilityRefs.FinalWyrmshifterRedBreathWeaponAbility)
            .EditComponent<AbilityEffectRunAction>(c => c.Actions.Actions
                .OfType<ContextActionDealDamage>()
                .First()
                .DamageType
                .Energy = Kingmaker.Enums.Damage.DamageEnergyType.Fire)
            .Configure();
    }
    [DragonConfigure]
    public static void PatchIRageshaperDevastatingFormAbility()
    {
        Main.log.Log("Patching RageshaperDevastatingFormAbility to include more rages");
        AbilityConfigurator.For(AbilityRefs.RageshaperDevastatingFormAbility)
            .EditComponent<AbilityTargetHasFact>(EditAbilityTargetHasFact)
            .Configure();
    }

    public static void EditAbilityTargetHasFact(AbilityTargetHasFact component)
    {
        component.m_CheckedFacts =
        [
            .. component.m_CheckedFacts,
            BuffRefs.StandartFocusedRageBuff.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.InciteRageEffectBuff.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.ElementalRampagerRampageBuff.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.RageshaperDevastatingFormBuff.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.RageSpellBuff.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.InspiredRageEffectBuff.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.InspiredRageEffectBuffMythic.Reference.Get().ToReference<BlueprintUnitFactReference>()
        ];
    }
}