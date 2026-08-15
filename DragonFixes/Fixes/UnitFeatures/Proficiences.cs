using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Enums;

namespace DragonFixes.Fixes.UnitFeatures;

public class Proficiences
{
    [DragonConfigure]
    public static void PatchMartialProf()
    {
        Main.log.Log("Patching MartialProf to add Spiked Shields, owlcat plz");
        FeatureConfigurator.For(FeatureRefs.MartialWeaponProficiency)
            .AddProficiencies(weaponProficiencies: [WeaponCategory.WeaponLightShield, WeaponCategory.SpikedHeavyShield,
                WeaponCategory.WeaponHeavyShield, WeaponCategory.SpikedLightShield])
            .Configure();
        BlueprintFeature bp = FeatureRefs.ShieldBashFeature.Reference.Get();
        DragonHelpers.RemoveComponent(bp, bp.GetComponent<PrerequisiteNotProficient>());
    }
}