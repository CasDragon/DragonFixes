using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;

namespace DragonFixes.Fixes.Items;

public class FlamesWill
{
    [DragonConfigure]
    public static void AddEnchants()
    {
        Main.log.Log("Adding enchants to NemaryStigmaSlingStaff_ObedienceItem");
        ItemWeaponConfigurator.For(ItemWeaponRefs.NemaryStigmaSlingStaff_ObedienceItem)
            .AddToEnchantments(WeaponEnchantmentRefs.Axiomatic.ToString(), 
                WeaponEnchantmentRefs.NemaryStigma_ObedienceEnchantment.ToString())
            .Configure();
    }
}