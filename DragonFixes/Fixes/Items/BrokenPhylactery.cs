using System;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.Items;

public class BrokenPhylactery
{
    [DragonConfigure]
    public static void RemoveComponents()
    {
        Main.log.Log("Removing unnecessary component on BrokenPhylacteryBodyRingBuff.");
        var x = BuffRefs.BrokenPhylacteryBodyRingBuff.Reference.Get();
        try
        {
            DragonHelpers.RemoveComponent<ChangeSpellElementalDamage>(x);
            DragonHelpers.RemoveComponent<AddStatBonus>(x);
        }
        catch (Exception e)
        {
            Main.log.Log(e.ToString());
        }
    }
}