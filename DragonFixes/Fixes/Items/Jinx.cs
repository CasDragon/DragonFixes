using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.Items;

public class Jinx
{
    [DragonConfigure]
    public static void FixEnchant()
    {
        Main.log.Log("Fix Jinx enchant to proc only on weapon attacks.");
        WeaponEnchantmentConfigurator.For(WeaponEnchantmentRefs.JinxEnchantment)
            .EditComponent<AdditionalDiceOnAttack>(c => c.AttackType = AdditionalDiceOnAttack.WeaponOptions.OnlyWeaponAttacks)
            .Configure();
    }
}