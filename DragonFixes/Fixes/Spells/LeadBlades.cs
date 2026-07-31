using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;

namespace DragonFixes.Fixes.Spells;

public class LeadBlades
{
    [DragonConfigure]
    public static void PatchLeadBladesBuff()
    {
        Main.log.Log("Patching LeafBladeBuff to include Sawtooth Sabre.");
        BuffConfigurator.For(BuffRefs.LeafBladesBuff)
            .EditComponent<IncreaseDiceSizeOnAttack>(c => c.Categories = [.. c.Categories, WeaponCategory.SawtoothSabre])
            .Configure();
    }
}