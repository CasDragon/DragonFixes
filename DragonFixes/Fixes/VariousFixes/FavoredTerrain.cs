using System.Collections.Generic;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonLibrary.NewComponents;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonFixes.Fixes.VariousFixes;

public class FavoredTerrainFix
{
    [DragonConfigure]
    public static void ReplaceComponent()
    {
        Main.log.Log("Replacing FavoredTerrain component on Favored Terrain features with fixed version.");
        List<Blueprint<BlueprintReference<BlueprintFeature>>> x =
        [
            FeatureRefs.FavoriteTerrainAbyss, FeatureRefs.FavoriteTerrainDesert, FeatureRefs.FavoriteTerrainFirstWorld,
            FeatureRefs.FavoriteTerrainForest, FeatureRefs.FavoriteTerrainHighlands, FeatureRefs.FavoriteTerrainPlains,
            FeatureRefs.FavoriteTerrainUnderground, FeatureRefs.FavoriteTerrainUrban
        ];
        foreach (var feature in x)
        {
            var y = feature.Reference.Get();
            var component = y.GetComponent<FavoredTerrain>();
            DragonHelpers.RemoveComponent(y,  component);
            FeatureConfigurator.For(y)
                .AddComponent(new FixedFavoredTerrain() { Setting = component.Setting })
                .Configure();
        }
    }
}