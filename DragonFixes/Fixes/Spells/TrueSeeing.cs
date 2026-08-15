using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;

namespace DragonFixes.Fixes.Spells;

public class TrueSeeing
{
    [DragonConfigure]
    public static void PatchTrueSeeingCast()
    {
        Main.log.Log("Patching TrueSeeingCast to allow for Extend metamagic.");
        AbilityConfigurator.For(AbilityRefs.TrueSeeingCast)
            .AddToAvailableMetamagic(Kingmaker.UnitLogic.Abilities.Metamagic.Extend)
            .Configure();
    }
}