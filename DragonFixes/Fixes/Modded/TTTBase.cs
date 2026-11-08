using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonLibrary.ModRefs;
using DragonLibrary.Utils;

namespace DragonFixes.Fixes.Modded;

public class TTTBase
{
    [DragonConfigure]
    public static void WeaponTraining()
    {
        var featSelection = FeatureSelectionRefs.BasicFeatSelection.Reference.Get();
        var bonusSelection = FeatureSelectionRefs.FighterFeatSelection.Reference.Get();
        var awt = TTTBaseRefs.AdvancedWeaponTrainingSelection;
        var aat = TTTBaseRefs.AdvancedArmorTrainingSelection;
        var featConfigurator = FeatureSelectionConfigurator.For(featSelection);
        var bonusConfigurator = FeatureSelectionConfigurator.For(bonusSelection);
        if (featSelection.AllFeatures.m_Array.All(f => f.guid != awt))
        {
            featConfigurator.AddToAllFeatures(awt);
        }

        if (bonusSelection.AllFeatures.m_Array.All(f => f.guid != awt))
        {
            bonusConfigurator.AddToAllFeatures(awt);
        }
        if (featSelection.AllFeatures.m_Array.All(f => f.guid != aat))
        {
            featConfigurator.AddToAllFeatures(aat);
        }

        if (bonusSelection.AllFeatures.m_Array.All(f => f.guid != aat))
        {
            bonusConfigurator.AddToAllFeatures(aat);
        }
        featConfigurator.Configure();
        bonusConfigurator.Configure();
    }
}