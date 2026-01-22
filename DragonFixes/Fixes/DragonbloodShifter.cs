using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes
{
    internal class DragonbloodShifter
    {
        [DragonConfigure]
        public static void PatchBreathWeapons()
        {
            Main.log.Log("Patching Dragonblood Shifter's breath weapons to actually scale off CON.");
            Blueprint<BlueprintReference<BlueprintFeature>>[] x = [FeatureRefs.ShifterDragonFormFeatureFinal, 
                FeatureRefs.ShifterDragonFormFeatureImproved, FeatureRefs.ShifterDragonFormFeature];
            Blueprint<BlueprintReference<BlueprintAbility>>[] y = [AbilityRefs.WyrmshifterBlackBreathWeaponAbility,
                AbilityRefs.WyrmshifterBlueBreathWeaponAbility, AbilityRefs.WyrmshifterBrassBreathWeaponAbility,
                AbilityRefs.WyrmshifterGoldBreathWeaponAbility, AbilityRefs.WyrmshifterGreenBreathWeaponAbility, AbilityRefs.WyrmshifterSilverBreathWeaponAbility,
                AbilityRefs.FinalWyrmshifterBlackBreathWeaponAbility, AbilityRefs.FinalWyrmshifterBrassBreathWeaponAbility, AbilityRefs.FinalWyrmshifterBlueBreathWeaponAbility,
                AbilityRefs.FinalWyrmshifterBronzeBreathWeaponAbility, AbilityRefs.FinalWyrmshifterCopperBreathWeaponAbility, AbilityRefs.FinalWyrmshifterGoldBreathWeaponAbility,
                AbilityRefs.FinalWyrmshifterGreenBreathWeaponAbility, AbilityRefs.FinalWyrmshifterRedBreathWeaponAbility, AbilityRefs.FinalWyrmshifterSilverBreathWeaponAbility,
                AbilityRefs.FinalWyrmshifterWhiteBreathWeaponAbility, AbilityRefs.GreaterWyrmshifterBlackBreathWeaponAbility, AbilityRefs.GreaterWyrmshifterBlueBreathWeaponAbility,
                AbilityRefs.GreaterWyrmshifterBrassBreathWeaponAbility, AbilityRefs.GreaterWyrmshifterGoldBreathWeaponAbility, AbilityRefs.GreaterWyrmshifterGreenBreathWeaponAbility,
                AbilityRefs.GreaterWyrmshifterSilverBreathWeaponAbility];
            BlueprintAbilityReference[] abilities = [];
            foreach (var z in y)
            {
                abilities.Append(z.Cast<BlueprintAbilityReference>().Reference);
            }
            ReplaceAbilitiesStat component = new ReplaceAbilitiesStat() 
            {
                Stat = StatType.Constitution,
                m_Ability = abilities
            };
            foreach (var feature in x)
            {
                FeatureConfigurator.For(feature)
                    .AddComponent(component)
                    .Configure();
            }
        }
    }
}
