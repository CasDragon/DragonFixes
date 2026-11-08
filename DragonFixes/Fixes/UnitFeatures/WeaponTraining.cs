using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Prerequisites;

namespace DragonFixes.Fixes.UnitFeatures
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
                .AddPrerequisiteFeaturesFromList([FeatureSelectionRefs.WeaponTrainingSelection.ToString(),
                    FeatureSelectionRefs.SoheiWeaponTrainingSelection.ToString(),
                    FeatureSelectionRefs.LichSkeletalWeaponTrainingSelection.ToString(),
                    FeatureSelectionRefs.DIscipleOfThePikeWeaponTrainingSelection.ToString()],
                    1,
                    group: Prerequisite.GroupType.All)
                .Configure();
        }
    }
}
