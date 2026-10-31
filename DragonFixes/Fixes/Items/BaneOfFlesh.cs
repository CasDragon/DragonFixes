using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics;

namespace DragonFixes.Fixes.Items;

public class BaneOfFlesh
{
    [DragonLocalizedString(buffname, "Bane Of Flesh")]
    public const string buffname = "baneofflesh.name";
    [DragonLocalizedString(buffname, "Whenever you or your party members {g|Encyclopedia:Attack}attack{/g} using the Rapid Shot {g|Encyclopedia:Feat}feat{/g}, they don't suffer a –2 {g|Encyclopedia:Penalty}penalty{/g} to attack but gain a +2 {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Damage}damage{/g} instead.", true)]
    public const string buffdescription = "baneofflesh.description";
    [DragonConfigure]
    public void FixBuff()
    {
        Main.log.Log("Fix Bane of Flesh's buff to actually provide bonus with Rapid Shot.");
        var x =BuffConfigurator.For(BuffRefs.BrokenPhylacteryBodyScytheRapidShot2Buff)
            .SetDisplayName(buffname)
            .SetDescription(buffdescription)
            .SetFlags([])
            .Configure();
        var attack = x.Components.FirstOrDefault(c => c is AttackBonusAgainstFactOwner) as AttackBonusAgainstFactOwner;
        attack?.AttackBonus = 2;
        attack?.Bonus = new ContextValue();
        attack?.Descriptor = ModifierDescriptor.UntypedStackable;
        var damage = x.Components.FirstOrDefault(c => c is DamageBonusAgainstFactOwner) as DamageBonusAgainstFactOwner;
        damage?.DamageBonus = 2;
        damage?.Bonus = new ContextValue();
        damage?.Descriptor = ModifierDescriptor.UntypedStackable;
    }
}