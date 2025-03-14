﻿using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonFixes.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Mechanics.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes
{
    internal class Various
    {
        [DragonFix]
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
        [DragonFix]
        public static void PatchMartialProf()
        {
            Main.log.Log("Patching MartialProf to add Spiked Shields, owlcat plz");
            FeatureConfigurator.For(FeatureRefs.MartialWeaponProficiency)
                .AddProficiencies(weaponProficiencies: [WeaponCategory.WeaponLightShield, WeaponCategory.SpikedHeavyShield,
                                                        WeaponCategory.WeaponHeavyShield, WeaponCategory.SpikedLightShield])
                .Configure();
            FeatureConfigurator.For(FeatureRefs.ShieldBashFeature)
                .RemoveComponents(c => c is PrerequisiteNotProficient)
                .Configure();
        }
        [DragonFix]
        public static void PatchWyrmShifterRedBreath()
        {
            Main.log.Log("Patching Wyrm Shifter's level 20 breath to correctly be fire damage instead of cold");
            AbilityConfigurator.For(AbilityRefs.FinalWyrmshifterRedBreathWeaponAbility)
                .EditComponent<AbilityEffectRunAction>(c => c.Actions.Actions
                        .OfType<ContextActionDealDamage>()
                        .First()
                        .DamageType
                        .Energy = Kingmaker.Enums.Damage.DamageEnergyType.Fire)
                .Configure();
        }
        [DragonFix]
        public static void PatchBestialRags()
        {
            Main.log.Log("Patching bestial rags");
            BuffConfigurator.For(BuffRefs.BestialRagsBuff)
                .RemoveComponents(c => c is SpellDescriptorComponent)
                .Configure();
        }
        [DragonFix]
        public static void PatchInspiringCommand()
        {
            Main.log.Log("Patching Inspiring Command");
            AbilityConfigurator.For(AbilityRefs.NobilityDomainBaseAbility)
                .SetType(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityType.Supernatural)
                .Configure();
            AbilityConfigurator.For(AbilityRefs.NobilityDomainBaseAbilitySeparatist)
                .SetType(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityType.Supernatural)
                .Configure();
        }
        [DragonFix]
        public static void PatchNeophyteGloves()
        {
            Main.log.Log("Patching the Gloves of the Neophyte to add the missing spells");
            FeatureConfigurator.For(FeatureRefs.GlovesOfNeophyteFeature)
                .EditComponent<DiceDamageBonusOnSpell>(c => dothisthing(c))
                .Configure();
        }

        public static void dothisthing(DiceDamageBonusOnSpell spell)
        {
            spell.m_Spells.Append(AbilityRefs.ShockingGraspEffect.Reference.Get().ToReference<BlueprintAbilityReference>());
            spell.m_Spells.Append(AbilityRefs.IncendiaryRunes.Reference.Get().ToReference<BlueprintAbilityReference>());
        }
    }
}
