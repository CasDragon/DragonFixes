using System;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonLibrary.BPCoreExtensions;
using DragonLibrary.Utils;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace DragonFixes.Fixes.MythicStuff;

public class Azata
{
    [DragonConfigure]
    public static void PatchIncredibleMight()
    {
        Main.log.Log("Patching IncredibleMightMainBuff to not double up on bonuses");
        var buff = BuffRefs.IncredibleMightMainBuff.Reference.Get();
        DragonHelpers.RemoveComponent<AttackBonusConditional>(buff);
        DragonHelpers.RemoveComponent<DamageBonusConditional>(buff);

        BuffConfigurator.For(buff)
            .AddContextIncreaseSpellDamage(ContextValues.Property(UnitProperty.MythicLevel))
            .Configure();
    }
}