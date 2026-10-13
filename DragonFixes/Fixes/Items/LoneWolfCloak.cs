using System.Linq;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Buffs.Components;

namespace DragonFixes.Fixes.Items;

public class LoneWolfCloak
{
    [DragonConfigure]
    public static void PartialFix()
    {
        Main.log.Log("Partially fixing Lone Wolf's Cloak to actually apply it's damage bonus (doesn't fix the other bugs)");
        var x = BuffRefs.CloakOfLoneWolfEffectBuff.Reference.Get().Components
            .First(c => c is WeaponAttackTypeDamageBonus) as WeaponAttackTypeDamageBonus;
        x!.Value = ContextValues.Constant(1);
    }
}