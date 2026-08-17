using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonFixes.Util;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.Spells;

public class ChainLightning
{
    /// <summary>
    /// Chain Lightning's Intensified flag sits on a ContextRankConfig of type DamageDice, but its
    /// damage reads the Default rank, which is missing the flag. The flag lands in a slot nothing
    /// reads, so Intensified never raises the 20d6 cap.
    ///
    /// You still need > CL 20 to actually benefit from it though.
    /// </summary>
    [DragonConfigure]
    public static void FixIntensifiedDamageCap()
    {
        Main.log.Log("Fixing Chain Lightning so Intensified Spell raises its damage dice cap.");
        var bp = AbilityRefs.ChainLightning.Reference.Get();
        var dice = bp.FlattenAllActions()
          .OfType<ContextActionDealDamage>()
          .FirstOrDefault()
          ?.Value
          .DiceCountValue;
        if (dice?.ValueType != ContextValueType.Rank)
        {
            Main.log.Log("Chain Lightning damage is no longer rank driven, skipping Intensified fix.");
            return;
        }
        AbilityConfigurator.For(bp)
            .EditComponents<ContextRankConfig>(
                c => c.m_AffectedByIntensifiedMetamagic = true,
                c => c.m_Type == dice.ValueRank)
            .Configure();
    }
}
