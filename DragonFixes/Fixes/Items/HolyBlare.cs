using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;

namespace DragonFixes.Fixes.Items;

public class HolyBlare
{
    [DragonConfigure]
    public static void AddEnchant()
    {
        Main.log.Log("Adding the enchant to Holy Blare.");
        ItemWeaponConfigurator.For(ItemWeaponRefs.HolyBlareItem)
            .AddToEnchantments(WeaponEnchantmentRefs.HolyBlareEnchantment.ToString())
            .Configure();
    }
}