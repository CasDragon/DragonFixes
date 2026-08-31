using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;

namespace DragonFixes.Fixes.UnitFeatures;

public class PowerfulStance
{
    [DragonConfigure]
    public static void PatchPowerfulStanceSwitchBuff()
    {
        Main.log.Log("Patching PowerfulStanceSwitchBuff to include more rages.");
        BuffConfigurator.For(BuffRefs.PowerfulStanceSwitchBuff)
            .AddBuffExtraEffects(
                checkedBuff: BuffRefs.PowerfulStanceSwitchBuff.ToString(),
                extraEffectBuff: BuffRefs.PowerfulStanceEffectBuff.ToString()
            )
            .Configure();
    }
}