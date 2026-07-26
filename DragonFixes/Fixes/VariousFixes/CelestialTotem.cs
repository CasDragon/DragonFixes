using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonFixes.Fixes.VariousFixes;

public class CelestialTotem
{
    [DragonConfigure]
    public static void PatchCelestialTotemLesser()
    {
        Main.log.Log("Patching Celestial Totem Lesser to not heal on 0 hp heals.");
        BuffConfigurator.For(BuffRefs.CelestialTotemLesserBuff)
            .EditComponent<AddHealTrigger>(c => c.AllowZeroHealDamage = false)
            .Configure();
    }
}