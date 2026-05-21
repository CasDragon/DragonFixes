using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Backgrounds
{
    internal class RahadoumFaithless
    {
        [DragonConfigure]
        public static void AddBackgroundRahadoumFaithless()
        {
            Main.log.Log("Adding the RahadoumFaithless blueprint to background selection.");
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.BackgroundsRegionalSelection)
                .AddToAllFeatures([FeatureRefs.BackgroundRahadoumFaithless.ToString()])
                .Configure();
        }

    }
}
