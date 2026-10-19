using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;

namespace DragonFixes.Fixes.Items;

public class ScorchingBracers
{
    [DragonConfigure]
    public static void FixCharges()
    {
        Main.log.Log("Fixing Scorching Bracers charges amount");
        ItemEquipmentWristConfigurator.For(ItemEquipmentWristRefs.ScorchingBracersWristItem)
            .SetCharges(3)
            .Configure();
    }
}