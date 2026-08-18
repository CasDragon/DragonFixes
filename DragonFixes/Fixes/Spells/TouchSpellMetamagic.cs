using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;

namespace DragonFixes.Fixes.Spells;

public class TouchSpellMetamagic
{
    /// <summary>
    /// A sticky touch spell is two blueprints: the cast half sits in the spellbook, the delivery
    /// half carries the damage. Metamagic availability is gated on the cast half only
    /// (RuleCollectMetamagic), so a cast half with a stale config hides metamagic options the
    /// delivery half is fully set up for. Vampiric Touch loses Intensified, Piercing and Extend,
    /// Corrosive Touch loses Intensified and Piercing.
    ///
    /// Shocking Grasp and Force Punch are the same architecture with identical configs on both
    /// halves, which is the intended shape, so this copies whatever the delivery half allows.
    /// </summary>
    [DragonConfigure]
    public static void SyncCastMetamagicWithDelivery()
    {
        SyncWithDelivery(AbilityRefs.VampiricTouchCast.Reference.Get());
        SyncWithDelivery(AbilityRefs.CorrosiveTouchCast.Reference.Get());
    }

    private static void SyncWithDelivery(BlueprintAbility cast)
    {
        var delivery = cast.GetComponent<AbilityEffectStickyTouch>()?.TouchDeliveryAbility;
        if (delivery is null)
        {
            Main.log.Log($"{cast.name} has no touch delivery ability, skipping metamagic sync.");
            return;
        }
        var missing = delivery.AvailableMetamagic & ~cast.AvailableMetamagic;
        if (missing == 0)
        {
            return;
        }
        Main.log.Log($"Giving {cast.name} the metamagic its delivery ability already allows: {missing}.");
        AbilityConfigurator.For(cast)
            .AddToAvailableMetamagic(missing)
            .Configure();
    }
}
