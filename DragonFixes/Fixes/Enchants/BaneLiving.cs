using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;

namespace DragonFixes.Fixes.Enchants;

public class BaneLiving
{
    [DragonConfigure]
    public static void PatchBaneLivingEnchant()
    {
        Main.log.Log("Patching BaneLiving to correctly use AND logic");
        WeaponEnchantmentConfigurator.For(WeaponEnchantmentRefs.BaneLiving)
            .EditComponent<WeaponConditionalEnhancementBonus>(c => c.Conditions.Operation = Operation.And) 
            .EditComponent<WeaponConditionalDamageDice>(c => c.Conditions.Operation = Operation.And)
            .Configure();
    }
}