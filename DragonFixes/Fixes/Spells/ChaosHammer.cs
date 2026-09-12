using System.Linq;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace DragonFixes.Fixes.Spells;

public class ChaosHammer
{
    [DragonConfigure]
    public static void FixPreRoll()
    {
        Main.log.Log("Fixing Chaos Hammer from prerolling so damage riders work");
        var bp = AbilityRefs.ChaosHammer.Reference.Get();
        foreach (var element1 in bp.m_AllElements.Where(e => e is ContextActionDealDamage))
        {
            var element = (ContextActionDealDamage)element1;
            element.ReadPreRolledFromSharedValue = false;
            element.IsAoE = true;
        }
        var bp2 = AbilityRefs.ChaosHammerFlesheater.Reference.Get();
        foreach (var element1 in bp2.m_AllElements.Where(e => e is ContextActionDealDamage))
        {
            var element = (ContextActionDealDamage)element1;
            element.ReadPreRolledFromSharedValue = false;
            element.IsAoE = true;
        }
    }
}