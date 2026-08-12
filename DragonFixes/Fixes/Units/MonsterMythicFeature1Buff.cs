using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonFixes.Fixes.Units;

public class MonsterMythicFeature1Buff
{
    [DragonConfigure]
    public static void Configure()
    {
        Main.log.Log("Patching MonsterMythicFeature1Buff to have a Will save instead of 2 reflex.");
        var bp = BuffRefs.MonsterMythicFeature1Buff.Reference.Get();
        var components = bp.Components
            .Where(c => c is AddContextStatBonus { Stat: StatType.SaveReflex }).ToList();
        if (components.Count() > 1)
        {
            var component = components[0] as AddContextStatBonus;
            component!.Stat = StatType.SaveWill;
        }
    }
}