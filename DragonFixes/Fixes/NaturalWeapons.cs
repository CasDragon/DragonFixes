using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes;

internal class NaturalWeapons
{
    [DragonConfigure]
    public static void DoubleThem()
    {
        Main.log.Log("Setting a bunch of natural weapons to count as double;");
         Blueprint<BlueprintReference<BlueprintItemWeapon>>[] doublenaturalList = 
         [
             ItemWeaponRefs.BeastTotemClaw, ItemWeaponRefs.BeastTotemClawGreater, ItemWeaponRefs.BloodragerAbyssalClaw1d6, 
             ItemWeaponRefs.BloodragerAbyssalClaw1d6Magic, ItemWeaponRefs.BloodragerAbyssalClaw1d8Magic, 
             ItemWeaponRefs.BloodragerAbyssalClaw1d8MagicFlaming, ItemWeaponRefs.BloodragerDraconicClaw1d6, 
             ItemWeaponRefs.BloodragerDraconicClaw1d8Magic, ItemWeaponRefs.BloodragerDraconicClaw1d8MagicAcid, 
             ItemWeaponRefs.BloodragerDraconicClaw1d8MagicCold, ItemWeaponRefs.BloodragerDraconicClaw1d8MagicElectricity, 
             ItemWeaponRefs.BloodragerDraconicClaw1d8MagicFire, ItemWeaponRefs.Claw1d8, ItemWeaponRefs.Claw1d10, 
             ItemWeaponRefs.Claw2d6, ItemWeaponRefs.Claw2d8, ItemWeaponRefs.ShifterClaw1d10x3, ItemWeaponRefs.Nails1d3, 
             ItemWeaponRefs.Spike1d4, ItemWeaponRefs.Spike1d6, ItemWeaponRefs.Spike1d8, ItemWeaponRefs.Spike1d10, 
             ItemWeaponRefs.Spike1d10x3, ItemWeaponRefs.Slam1d4, ItemWeaponRefs.Slam1d6, ItemWeaponRefs.Slam1d8, 
             ItemWeaponRefs.TerribleSlam1d10, ItemWeaponRefs.TerribleSlam1d10x3, ItemWeaponRefs.BloodlineAbyssalClaw1d4, 
             ItemWeaponRefs.BloodlineAbyssalClaw1d6, ItemWeaponRefs.BloodlineAbyssalClaw1d6Fire, ItemWeaponRefs.BloodlineDraconicClaw1d4, 
             ItemWeaponRefs.BloodlineDraconicClaw1d6, ItemWeaponRefs.BloodlineDraconicClaw1d6Corrosive, 
             ItemWeaponRefs.BloodlineDraconicClaw1d6Fire, ItemWeaponRefs.BloodlineDraconicClaw1d6Frost, 
             ItemWeaponRefs.BloodlineDraconicClaw1d6Shock
         ];
        foreach (var doublenatural in doublenaturalList)
        {
            ItemWeaponConfigurator.For(doublenatural)
                .SetCountAsDouble(true)
                .Configure();
        }
    }
    [DragonConfigure]
    public static void PatchThisNonsense()
    {
        Main.log.Log("Patching 'OtherNaturalAttack' nonsense for Clutch of Corruption");
        FeatureConfigurator.For(FeatureRefs.ClutchOfCorruptionFeature)
            .EditComponent<AddInitiatorAttackWithWeaponTrigger>(stuff)
            .Configure();
        Main.log.Log("Patching 'OtherNaturalAttack' nonsense for Cobra Pads");
        FeatureConfigurator.For(FeatureRefs.CobraPadsFeature)
            .EditComponent<AddInitiatorAttackWithWeaponTrigger>(stuff)
            .Configure();
        Main.log.Log("Patching 'OtherNaturalAttack' nonsense for AncientWoodFeature");
        BlueprintFeature bp = FeatureConfigurator.For(FeatureRefs.AncientWoodFeature)
            .AddACBonusAgainstWeaponGroup(armorClassBonus: 3, descriptor: ModifierDescriptor.Competence, fighterGroup: WeaponFighterGroup.Natural)
            .Configure();
        DragonHelpers.RemoveComponent(bp, bp.GetComponent<ACBonusAgainstWeaponCategory>());
    }
    public static void stuff(AddInitiatorAttackWithWeaponTrigger component)
    {
        component.CheckWeaponCategory = false;
        component.CheckWeaponGroup = true;
        component.Group = WeaponFighterGroup.Natural;
    }
}
