using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.Items;

public class AspectofAsp
{
    [DragonConfigure]
    public static void PatchAspectofAsp()
    {
        Main.log.Log("Patching Aspect of Asep enchant to work");
        FeatureConfigurator.For(FeatureRefs.AspectOfTheAspFeature)
            .EditComponent<AdditionalDiceOnAttack>(c =>
                c.AttackType = AdditionalDiceOnAttack.WeaponOptions.AllAttacks)
            .Configure();
    }
}