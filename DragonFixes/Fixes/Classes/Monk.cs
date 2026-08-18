using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonFixes.Fixes.Classes
{
    internal class Monk
    {
        [DragonConfigure]
        public static void PatchScaledFist()
        {
            Main.log.Log("Patching ScaledFistACBonusBuff/ScaledFistACBonusUnarmoredBuff to be class features.");
            BuffConfigurator.For(BuffRefs.ScaledFistACBonusBuff)
                .SetIsClassFeature(true)
                .Configure();
            BuffConfigurator.For(BuffRefs.ScaledFistACBonusUnarmoredBuff)
                .SetIsClassFeature(true)
                .Configure();
        }
        [DragonConfigure]
        public static void PatchExtraKi()
        {
            Main.log.Log("Patching ExtraKi to use IncreaseResourceAmount, fixing it not working multiple times");
            var x = FeatureRefs.ExtraKi.Reference.Get();
            DragonHelpers.RemoveComponent<IncreaseResourceAmountBySharedValue>(x);
            DragonHelpers.RemoveComponent<IncreaseResourceAmountBySharedValue>(x);
            DragonHelpers.RemoveComponent<IncreaseResourceAmountBySharedValue>(x);
            FeatureConfigurator.For(x)
                .AddIncreaseResourceAmount(
                    resource: AbilityResourceRefs.KiPowerResource.Reference.Get(),
                    value: 2)
                .AddIncreaseResourceAmount(
                    resource: AbilityResourceRefs.DrunkenKiPowerResource.Reference.Get(),
                    value: 2)
                .AddIncreaseResourceAmount(
                    resource: AbilityResourceRefs.ScaledFistPowerResource.Reference.Get(),
                    value: 2)
                .AddFeatureTagsComponent(FeatureTag.ClassSpecific)
                .Configure();
        }
    }
}
