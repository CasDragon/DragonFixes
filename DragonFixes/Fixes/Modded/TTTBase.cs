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
        if (!ModCompat.tttbase) return;
        var featSelection = FeatureSelectionRefs.BasicFeatSelection.Reference.Get();
        var bonusSelection = FeatureSelectionRefs.FighterFeatSelection.Reference.Get();
        var awt = TTTBaseRefs.AdvancedWeaponTrainingSelection;
        var aat = TTTBaseRefs.AdvancedArmorTrainingSelection;
        var featConfigurator = FeatureSelectionConfigurator.For(featSelection);
        var bonusConfigurator = FeatureSelectionConfigurator.For(bonusSelection);
        var awtEnabled = TTTSettingChecker.CheckFighterAdvancedWeaponTrainingEnabled();
        var aatEnabled = TTTSettingChecker.CheckFighterAdvancedArmorTrainingEnabled();
        if (featSelection.AllFeatures.m_Array.All(f => f.guid != awt) && awtEnabled)
        {
            featConfigurator.AddToAllFeatures(awt);
        }

        if (bonusSelection.AllFeatures.m_Array.All(f => f.guid != awt) && awtEnabled)
        {
            bonusConfigurator.AddToAllFeatures(awt);
        }
        if (featSelection.AllFeatures.m_Array.All(f => f.guid != aat) && aatEnabled)
        {
            featConfigurator.AddToAllFeatures(aat);
        }

        if (bonusSelection.AllFeatures.m_Array.All(f => f.guid != aat) && aatEnabled)
        {
            bonusConfigurator.AddToAllFeatures(aat);
        }
        featConfigurator.Configure();
        bonusConfigurator.Configure();
    }
}