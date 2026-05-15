using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Prerequisites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.VariousFixes
{
    internal class WeaponTraining
    {
        [DragonConfigure]
        public static void PatchAdvancedWeaponTrainingSelection()
        {
            Main.log.Log("Patching AdvancedWeaponTraining1 to include other Weapon Training Selections as valid prereq");
            var x = FeatureSelectionRefs.AdvancedWeaponTraining1.Reference.Get();
            DragonHelpers.RemoveComponent<PrerequisiteFeature>(x);
            FeatureSelectionConfigurator.For(x)
                .AddPrerequisiteFeaturesFromList([FeatureSelectionRefs.WeaponTrainingSelection.Reference.Get(),
                    FeatureSelectionRefs.SoheiWeaponTrainingSelection.Reference.Get(),
                    FeatureSelectionRefs.LichSkeletalWeaponTrainingSelection.Reference.Get(),
                    FeatureSelectionRefs.DIscipleOfThePikeWeaponTrainingSelection.Reference.Get()],
                    1,
                    group: Prerequisite.GroupType.All)
                .Configure();
        }
    }
}
