using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;

namespace DragonFixes.Fixes.Spells;

public class InvisibleMass
{
    internal const string description = "This {g|Encyclopedia:Spell}spell{/g} functions like Invisibility, except it applies to all party members.";
    [DragonLocalizedString(spelldescription, description)]
    internal const string spelldescription = $"InvisibilityMass.description";
    [DragonConfigure]
    public static void ReplaceEnglishDescription()
    {
        Main.log.Log("Replacing English description for Invisibility Mass");
        AbilityConfigurator.For(AbilityRefs.InvisibilityMass)
            .SetDescription(spelldescription)
            .Configure();
    }
}