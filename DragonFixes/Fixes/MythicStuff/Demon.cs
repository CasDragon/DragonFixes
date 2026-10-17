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
        Main.log.Log("Patching to include all spells instead of all fire spells");
        var buff = BuffRefs.BrimorakAspectEffectBuff.Reference.Get();
        var arcana = buff.Components.OfType<DraconicBloodlineArcana>().First();
        BuffConfigurator.For(buff)
            .AddContextIncreaseSpellDamage(arcana.Value)
            .Configure();
        DragonHelpers.RemoveComponent<DraconicBloodlineArcana>(buff);
    }
}