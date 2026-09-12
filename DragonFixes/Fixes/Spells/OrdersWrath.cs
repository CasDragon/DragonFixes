using System.Linq;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace DragonFixes.Fixes.Spells;

public class OrdersWrath
{
    [DragonConfigure]
    public static void FixPreRoll()
    {
        Main.log.Log("Fixing Order's Wrath from prerolling so damage riders work");
        var bp = AbilityRefs.OrdersWrath.Reference.Get();
        foreach (var element1 in bp.m_AllElements.Where(e => e is ContextActionDealDamage))
        {
            var element = (ContextActionDealDamage)element1;
            element.ReadPreRolledFromSharedValue = false;
            element.IsAoE = true;
        }
        var bp2 = AbilityRefs.OrdersWrathFleshEater.Reference.Get();
        foreach (var element1 in bp2.m_AllElements.Where(e => e is ContextActionDealDamage))
        {
            var element = (ContextActionDealDamage)element1;
            element.ReadPreRolledFromSharedValue = false;
            element.IsAoE = true;
        }
    }
}