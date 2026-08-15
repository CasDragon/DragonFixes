using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Designers.Mechanics.Facts;

namespace DragonFixes.Fixes.Races;

public class Tiefling
{
    [DragonConfigure]
    public static void PatchTieflingHeritageDemodand()
    {
        Main.log.Log("Patching TieflingHeritageDemodand to remove AND condition.");
        FeatureConfigurator.For(FeatureRefs.TieflingHeritageDemodand)
            .EditComponent<AttackBonusConditional>(c => c.Conditions.Operation = Kingmaker.ElementsSystem.Operation.Or)
            .Configure();
    }
}