using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Items
{
    internal class SmolItemTweaks
    {
        [DragonConfigure]
        public static void PatchSeasonedAssassin()
        {
            Main.log.Log("Patchin SeasonedAssassinFeature to not be hidden in the UI, so when you encounter an enemy with it, you can see it.");
            FeatureConfigurator.For(FeatureRefs.SeasonedAssassinsFeature)
                .SetHideInUI(false)
                .Configure();
        }
    }
}
