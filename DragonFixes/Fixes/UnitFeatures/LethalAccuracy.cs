using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.BPCoreExtensions;
using DragonLibrary.Utils;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;

namespace DragonFixes.Fixes.UnitFeatures;

public class LethalAccuracy
{
    [DragonConfigure]
    public static void AddThrownWeaponDamage()
    {
        Main.log.Log("Fixing Lethal Accuracy to apply to all weapons in Lethal Stance");
        var x = BuffRefs.LethalAccuracyEffectBuff.Reference.Get();
        DragonHelpers.RemoveComponent<AttackTypeCriticalMultiplierIncrease>(x);
        BuffConfigurator.For(x)
            .AddCriticalMultiplierBonus(bonus: 1, bonusDescriptor: ModifierDescriptor.Competence)
            .Configure();
    }
}