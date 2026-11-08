using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;

namespace DragonFixes.Fixes.Spells;

public class SilverDarts
{
    [DragonConfigure]
    public static void AllowSpecialization()
    {
        Main.log.Log("SilverDarts now can be specialized and is in thass lists");
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.SilverDarts);
        SpellHelpers.AddToThassilonian(AbilityRefs.SilverDarts, 3);
    }
}