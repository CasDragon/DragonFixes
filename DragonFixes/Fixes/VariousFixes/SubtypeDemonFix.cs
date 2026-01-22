using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Various
{
    internal class SubtypeDemonFix
    {
        [DragonConfigure]
        public static void PatchForElectricImmunity()
        {
            Main.log.Log("Patching SubtypeDemon to conditionally add ElectricImmunity, should help with Blackwater.");
            var feat = FeatureRefs.SubtypeDemon.Reference.Get();
            DragonHelpers.RemoveComponent<AddFacts>(feat);
            FeatureConfigurator.For(feat)
                .AddFacts([FeatureRefs.AcidResistance10.Reference.Get(), FeatureRefs.ImmunityToPoison.Reference.Get(),
                        FeatureRefs.ColdResistance10.Reference.Get(), FeatureRefs.FireResistance10.Reference.Get(),
                        FeatureRefs.SubtypeEvil.Reference.Get(), FeatureRefs.SubtypeChaotic.Reference.Get()])
                .AddFeatureIfHasFact(FeatureRefs.DisableElectricityImmunity.Reference.Get(), FeatureRefs.ElectricityImmunity.Reference.Get(), true)
                .Configure();
        }
    }
}
