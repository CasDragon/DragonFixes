using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;

namespace DragonFixes.Fixes.Spells;

public class BreathOfFlies
{
    [DragonConfigure]
    public static void AllowSpecialization()
    {
        Main.log.Log("BreathofFliesAbility now can be specialized and is in thass lists");
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.BreathofFliesAbility);
        SpellHelpers.AddToThassilonian(AbilityRefs.BreathofFliesAbility, 7);
    }
    
}