using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Class.Kineticist;

namespace DragonFixes.Fixes.Classes;

public class Kineticist
{
        [DragonConfigure]
        public static void PatchSpindleInfusion()
        {
            Main.log.Log("Patching Spindle / Exploding Arrows infusions to use InfusionBurnCost instead of BlastBurnCost.");
            Blueprint<BlueprintReference<BlueprintAbility>>[] abilities = [AbilityRefs.SpindleAirBlastAbility, AbilityRefs.SpindleBlizzardBlastAbility,
                AbilityRefs.SpindleBloodBlastAbility, AbilityRefs.SpindleBlueFlameBlastAbility, AbilityRefs.SpindleChargedWaterBlastAbility,
                AbilityRefs.SpindleColdBlastAbility, AbilityRefs.SpindleEarthBlastAbility, AbilityRefs.SpindleElectricBlastAbility,
                AbilityRefs.SpindleFireBlastAbility, AbilityRefs.SpindleIceBlastAbility, AbilityRefs.SpindleMagmaBlastAbility,
                AbilityRefs.SpindleMetalBlastAbility, AbilityRefs.SpindleMudBlastAbility, AbilityRefs.SpindlePlasmaBlastAbility,
                AbilityRefs.SpindleSandstormBlastAbility, AbilityRefs.SpindleSteamBlastAbility, AbilityRefs.SpindleThunderstormBlastAbility,
                AbilityRefs.SpindleWaterBlastAbility, AbilityRefs.ExplodingArrowsAirBlastAbility, AbilityRefs.ExplodingArrowsBlizzardBlastAbility,
                AbilityRefs.ExplodingArrowsBlueFlameBlastAbility, AbilityRefs.ExplodingArrowsChargedWaterBlastAbility, AbilityRefs.ExplodingArrowsColdBlastAbility,
                AbilityRefs.ExplodingArrowsEarthBlastAbility, AbilityRefs.ExplodingArrowsElectricBlastAbility, AbilityRefs.ExplodingArrowsFireBlastAbility,
                AbilityRefs.ExplodingArrowsIceBlastAbility, AbilityRefs.ExplodingArrowsMagmaBlastAbility, AbilityRefs.ExplodingArrowsMetalBlastAbility,
                AbilityRefs.ExplodingArrowsMudBlastAbility, AbilityRefs.ExplodingArrowsPlasmaBlastAbility, AbilityRefs.ExplodingArrowsSandstormBlastAbility,
                AbilityRefs.ExplodingArrowsSteamBlastAbility, AbilityRefs.ExplodingArrowsThunderstormBlastAbility, AbilityRefs.ExplodingArrowsWaterBlastAbility];
            foreach(var ability in abilities)
            {
                AbilityConfigurator.For(ability)
                    .EditComponent<AbilityKineticist>(c => changeinfusions(c))
                    .Configure();
            }
        }
        public static void changeinfusions(AbilityKineticist component)
        {
            component.BlastBurnCost = 0;
            component.InfusionBurnCost = 2;
        }
}