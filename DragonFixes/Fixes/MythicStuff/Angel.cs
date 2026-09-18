using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components;

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
}