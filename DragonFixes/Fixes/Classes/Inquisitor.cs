using System.Linq;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.Classes;

public class Inquisitor
{
    // Living Grimoire 
    [DragonConfigure]
    public static void AddLivingGrimToForbidSpellbook()
    {
        Main.log.Log("Adding LivingGrimoire to ForbidSpellbook component on deities");
        var deities = FeatureSelectionRefs.DeitySelection.Reference.Get().m_AllFeatures;
        var livinggrim = SpellbookRefs.LivingGrimoireSpellbook.Reference.Get()
            .ToReference<BlueprintSpellbookReference>();
        foreach (var deity in deities)
        {
            if (deity.GetBlueprint() is not BlueprintFeature d) continue;
            var forbid = d.Components.OfType<ForbidSpellbookOnAlignmentDeviation>().FirstOrDefault();
            forbid?.m_Spellbooks = [..forbid.m_Spellbooks, livinggrim];
        }
    }

    [DragonConfigure]
    public static void FixBlessedScriptSpellBuff()
    {
        Main.log.Log("Patching BlessedScriptSpellBuff to fix it increasing spell DC and CL");
        var buff = BuffRefs.BlessedScriptSpellBuff.Reference.Get();
        var crc = buff.Components.OfType<ContextRankConfig>().First();
        crc?.m_Progression = ContextRankProgression.Custom;
    }
}