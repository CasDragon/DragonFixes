using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Items
{
    internal class AmuletOfJousting
    {
        [DragonConfigure]
        public static void PatchAmuletOfJoustingFeature()
        {
            Main.log.Log("Patching AmuletOfJoustingFeature to set buff on target");
            FeatureRefs.AmuletOfJoustingFeature.Reference.Get()
                .GetComponent<AddAbilityUseTrigger>()
                .ActionsOnTarget = true;
        }
    }
}
