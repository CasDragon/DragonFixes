using System.Linq;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace DragonFixes.Fixes.Spells;

public class UnholyBlight
{
    [DragonConfigure]
    public static void FixPreRoll()
    {
        Main.log.Log("Fixing Unholy Blight from prerolling so damage riders work");
        var bp = AbilityRefs.UnholyBlight.Reference.Get();
        foreach (var element1 in bp.m_AllElements.Where(e => e is ContextActionDealDamage))
        {
            var element = (ContextActionDealDamage)element1;
            element.ReadPreRolledFromSharedValue = false;
            element.IsAoE = true;
        }
        var bp2 = AbilityRefs.UnholyBlightFleshEater.Reference.Get();
        foreach (var element1 in bp2.m_AllElements.Where(e => e is ContextActionDealDamage))
        {
            var element = (ContextActionDealDamage)element1;
            element.ReadPreRolledFromSharedValue = false;
            element.IsAoE = true;
        }
    }
}