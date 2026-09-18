using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonFixes.Fixes.Classes;

public class Inquisitor
{
    // Living Grimoire 
    [DragonConfigure]
    public static void AddLivingGrimToForbidSpellbook()
    {
        Main.log.Log("Adding LivingGrimoire to ForbidSpellbook component on deities");
        var deities = FeatureSelectionRefs.DeitySelection.Reference.Get().m_AllFeatures;
        foreach (var deity in deities)
        {
            if (deity.GetBlueprint() is not BlueprintFeature d) continue;
            var forbid = d.GetComponent<ForbidSpellbookOnAlignmentDeviation>();
            forbid?.m_Spellbooks = [..forbid.m_Spellbooks, SpellbookRefs.LivingGrimoireSpellbook.Reference.Get().ToReference<BlueprintSpellbookReference>()];
        }
    }
}