using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.BPCoreExtensions;
using DragonLibrary.Utils;
using Kingmaker.Designers.Mechanics.Facts;

namespace DragonFixes.Fixes.MythicStuff;

public class Demon
{
    // Aspects
    [DragonConfigure]
    public static void AspectOfBrimorak()
    {
        if (ModCompat.IsModEnabled("WOTR_PATH_OF_RAGE") || ModCompat.tttbase) return;
        Main.log.Log("Patching to include all spells instead of all fire spells");
        var buff = BuffRefs.BrimorakAspectEffectBuff.Reference.Get();
        var arcana = buff.Components.OfType<DraconicBloodlineArcana>().FirstOrDefault();
        if (arcana == null)  return;
        BuffConfigurator.For(buff)
            .AddContextIncreaseSpellDamage(arcana.Value)
            .Configure();
        DragonHelpers.RemoveComponent<DraconicBloodlineArcana>(buff);
    }
}