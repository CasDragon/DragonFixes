using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonFixes.Fixes.Items;

public class MaskOfNothing
{
    [DragonConfigure]
    public static void PatchBuff()
    {
        Main.log.Log("Patching MaskOfNothingBuff to remove the immunity to TrueSeeing");
        var x = BuffRefs.MaskOfNothingBuff.Reference.Get();
        DragonHelpers.RemoveComponent<AddConditionImmunity>(x);
    }
}