using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Abilities.Components;
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
}