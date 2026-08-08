using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Spells;

namespace DragonFixes.Fixes.Classes;

public class MantisZealot
{
    [DragonConfigure]
    public static void PatchDeadlyFascination()
    {
        Main.log.Log("Patching MantisZealotDeadlyFascinationAbility to include MindEffecting descriptor.");
        AbilityConfigurator.For(AbilityRefs.MantisZealotDeadlyFascinationAbility)
            .SetSpellDescriptor(SpellDescriptor.MindAffecting | SpellDescriptor.Charm | SpellDescriptor.Daze)
            .Configure();
    }
}