using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace DragonFixes.Fixes.Items;

public class BestialRags
{
    [DragonConfigure]
    public static void PatchBestialRags()
    {
        Main.log.Log("Patching bestial rags");
        BlueprintBuff bp = BuffRefs.BestialRagsBuff.Reference.Get();
        DragonHelpers.RemoveComponent(bp, bp.GetComponent<SpellDescriptorComponent>());
    }
}