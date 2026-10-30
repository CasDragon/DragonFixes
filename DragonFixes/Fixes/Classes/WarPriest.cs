using System.Linq;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.Classes;

public class WarPriest
{
    [DragonConfigure]
    public static void RemoveBaseSacredWeapon()
    {
        Main.log.Log("Removing SacredWeaponFavoriteDamageOverride from WarpriestProgression");
        var prog = ProgressionRefs.WarpriestProgression.Reference.Get();
        DragonHelpers.RemoveComponent<SacredWeaponFavoriteDamageOverride>(prog);
        
        Main.log.Log("Setting AchaekekSacredWeaponFeature to Sawtooth despite it already being that in the blueprint. ");
        var ach = FeatureRefs.AchaekekSacredWeaponFeature.Reference.Get();
        var comp =
            ach.Components.FirstOrDefault(c => c is SacredWeaponFavoriteDamageOverride) as
                SacredWeaponFavoriteDamageOverride;
        comp?.Category = WeaponCategory.SawtoothSabre;
    }
}