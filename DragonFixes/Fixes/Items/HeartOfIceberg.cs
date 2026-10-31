using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;

namespace DragonFixes.Fixes.Items;

public class HeartOfIceberg
{
    [DragonConfigure]
    public static void AddText()
    {
        Main.log.Log("Adding name and description to HeartOfIcebergAbility");
        var weapon = ItemWeaponRefs.HeartOfIcebergItem.Reference.Get();
        AbilityConfigurator.For(AbilityRefs.HeartOfIcebergAbility)
            .SetDisplayName(weapon.NameSafe())
            .SetDescription(weapon.Description)
            .Configure();
    }
}