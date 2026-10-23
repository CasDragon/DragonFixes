using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Designers.Mechanics.EquipmentEnchants;

namespace DragonFixes.Fixes.Enchants;

public class HeartSeekerEnchant
{
    [DragonConfigure]
    public static void RemoveIgnoreDR()
    {
        Main.log.Log("Removing the Ignore DR component on HeartSeeker enchant");
        DragonHelpers.RemoveComponent<IgnoreTargetDREnchantment>(WeaponEnchantmentRefs.Heartseeker.Reference.Get());
    }
}