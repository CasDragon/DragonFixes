using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Designers.Mechanics.Facts;

namespace DragonFixes.Fixes.Items;

public class ShadowBlood
{
    [DragonConfigure]
    public static void blah()
    {
        Main.log.Log("Patching Shadowbloodbuff because Whiterock told me to");
        BuffConfigurator.For(BuffRefs.ShadowbloodBuff)
            .EditComponent<SavingThrowBonusAgainstDescriptor>(c => c.OnlyPositiveValue = false)
            .Configure();
    }
}