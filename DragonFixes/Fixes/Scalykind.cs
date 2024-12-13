using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonFixes.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes
{
    internal class Scalykind
    {
        public static void PatchScalykind()
        {
            if (Settings.GetSetting<bool>("scalykind"))
            {
                Main.log.Log("Patching second domain selection.");
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.SecondDomainsSelection)
                    .AddToAllFeatures(ProgressionRefs.ScalykindDomainProgressionSecondary.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.SecondDomainsSeparatistSelection)
                    .AddToAllFeatures(ProgressionRefs.ScalykindDomainProgressionSeparatist.Reference.Get())
                    .Configure();
            }
            else
            {
                Main.log.Log("Scalykind patch disabled, skipping.");
            }
        }
    }
}
