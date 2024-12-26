using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonFixes.Util;
using Kingmaker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes
{
    internal class Various
    {
        public static void PatchAbundantArcanePool()
        {
            if (Settings.GetSetting<bool>("abundantarcanepool"))
            {
                Main.log.Log("Patching Abundant Arcane Pool for Spell Dancer");
                FeatureConfigurator.For(FeatureRefs.AbundantArcanePool)
                    .AddPrerequisiteFeature(FeatureRefs.SpellDanceFeature.Reference.Get())
                    .Configure();
            }
        }

        public static void PatchMartialProf()
        {
            Main.log.Log("Patching MartialProf to add Spiked Shields, owlcat plz");
            FeatureConfigurator.For(FeatureRefs.MartialWeaponProficiency)
                .AddProficiencies(weaponProficiencies: [WeaponCategory.WeaponLightShield, WeaponCategory.SpikedHeavyShield,
                                                        WeaponCategory.WeaponHeavyShield, WeaponCategory.SpikedLightShield])
                .Configure();
        }
    }
}
