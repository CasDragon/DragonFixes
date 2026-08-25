using System;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;

namespace DragonFixes.Fixes.MythicStuff;

public class Azata
{
    [DragonConfigure]
    public static void PatchIncredibleMight()
    {
        Main.log.Log("Patching IncredibleMightMainBuff to not double up on bonuses");
        var buff = BuffRefs.IncredibleMightMainBuff.Reference.Get();
        try
        {
            DragonHelpers.RemoveComponent<AttackBonusConditional>(buff);
        }
        catch (Exception e)
        {
            Main.log.Log("Buff doesn't have that component");
        }
        try
        {
            DragonHelpers.RemoveComponent<DamageBonusConditional>(buff);
        }
        catch (Exception e)
        {
            Main.log.Log("Buff doesn't have that component");
        }
    }
}