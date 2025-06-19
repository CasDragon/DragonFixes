using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonFixes.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes
{
    internal class OtherNaturalAttackNonsense
    {
        [DragonFix]
        public static void PatchThisNonsense()
        {
            Main.log.Log("Patching 'OtherNaturalAttack' nonsense for Clutch of Corruption");
            FeatureConfigurator.For(FeatureRefs.ClutchOfCorruptionFeature)
                .EditComponent<AddInitiatorAttackWithWeaponTrigger>(c => stuff(c))
                .Configure();
            Main.log.Log("Patching 'OtherNaturalAttack' nonsense for Cobra Pads");
            FeatureConfigurator.For(FeatureRefs.CobraPadsFeature)
                .EditComponent<AddInitiatorAttackWithWeaponTrigger>(c => stuff(c))
                .Configure();
            Main.log.Log("Patching 'OtherNaturalAttack' nonsense for AncientWoodFeature");
            BlueprintFeature bp = FeatureConfigurator.For(FeatureRefs.AncientWoodFeature)
                //.RemoveComponents(c => c is ACBonusAgainstWeaponCategory)
                .AddACBonusAgainstWeaponGroup(armorClassBonus: 3, descriptor: ModifierDescriptor.Competence, fighterGroup: WeaponFighterGroup.Natural)
                .Configure();
            LibraryStuff.RemoveComponent(bp, bp.GetComponent<ACBonusAgainstWeaponCategory>());
        }
        public static void stuff(AddInitiatorAttackWithWeaponTrigger component)
        {
            component.CheckWeaponCategory = false;
            component.CheckWeaponGroup = true;
            component.Group = WeaponFighterGroup.Natural;
        }
    }
}
