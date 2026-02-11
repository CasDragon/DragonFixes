using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Prerequisites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Classes
{
    internal class DemonSlayer
    {
        [DragonConfigure]
        public static void PatchFavoredEnemySpellcasting()
        {
            Main.log.Log("Patching FavoredEnemySpellcasting to include OrderOfTheNail.");
            FeatureConfigurator.For(FeatureRefs.FavoredEnemySpellcasting)
                .EditComponent<PrerequisiteFeaturesFromList>(c => 
                    c.m_Features = [.. c.m_Features, 
                        FeatureSelectionRefs.OrderOfTheNailFavoriteEnemySelection.Reference.Get().ToReference<BlueprintFeatureReference>()])
                .Configure();
        }
    }
}
