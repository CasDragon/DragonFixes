using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.MythicStuff;

public class Angel
{
    [DragonConfigure]
    public static void PatchCleansingFlames()
    {
        Main.log.Log("Patching AngelCleansingFlames to have metamagic available");
        AbilityConfigurator.For(AbilityRefs.AngelCleansingFlames)
            .SetAvailableMetamagic(Metamagic.Bolstered | Metamagic.CompletelyNormal | Metamagic.Empower
                | Metamagic.Maximize | Metamagic.Heighten | Metamagic.Persistent | Metamagic.Quicken
                | Metamagic.Reach | Metamagic.Selective)
            .Configure();
    }

    [DragonConfigure]
    public static void PatchPiercingRays()
    {
        Main.log.Log("Patching AngelPiercingRays to do correct 1 + MR/2 value");
        var buff = BuffRefs.PiercingRaysBuff.Reference.Get();
        var crc = buff.Components.OfType<ContextRankConfig>().First();
        crc.m_Progression = ContextRankProgression.OnePlusDivStep;
        crc.m_StartLevel = 1;
        crc.m_StepLevel = 2;
    }
}