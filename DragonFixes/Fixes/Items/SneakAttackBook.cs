using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Items
{
    internal class SneakAttackBook
    {
        [DragonConfigure]
        public static void PatchBook()
        {
            Main.log.Log("Patching BaphometEscapedFeature to allow for sneak attack to be added regardles, because Owlcat hates fun.");
            var x = FeatureConfigurator.For(FeatureRefs.BaphometEscapedFeature)
                .EditComponent<AddFacts>(c => c.m_Flags = 0)
                .Configure();
            DragonHelpers.RemoveComponent<AddFeatureIfHasFact>(x);
        }
    }
}
