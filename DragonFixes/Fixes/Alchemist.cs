using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonFixes.Util;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Mechanics;
using static Kingmaker.UnitLogic.Mechanics.Components.AdditionalDiceOnDamage;

namespace DragonFixes.Fixes
{
    internal class Alchemist
    {
        [DragonFix]
        public static void PatchRingOfBigBoomFeature()
        {
            Main.log.Log("Patching RingofBigBoom to actually work on booms");
            FeatureConfigurator.For(FeatureRefs.RingOfBigBoomFeature)
                .AdditionalDiceOnDamage(isOneAtack: true, checkWeaponType: false,
                    checkAbilityType: false, checkSpellDescriptor: true,
                    checkSpellParent: true, spellDescriptorsList: SpellDescriptor.Bomb,
                    ignoreDamageFromThisFact: true, damageEntriesUse: DamageEntriesUse.Simple,
                    useWeaponDice: true, mainDamageTypeUse: true, 
                    diceValue: new ContextDiceValue
                    {
                        DiceType = Kingmaker.RuleSystem.DiceType.D6,
                        DiceCountValue = ContextValues.Constant(0),
                        BonusValue = ContextValues.Constant(12),
                    },
                    damageTypeDescription: new DamageTypeDescription
                        {
                            Type = DamageType.Energy,
                            Common = new DamageTypeDescription.CommomData
                            {
                                Reality = 0,
                                Alignment = 0,
                                Precision = false
                            },
                            Physical = new DamageTypeDescription.PhysicalData
                            {
                                Material = 0,
                                Form = PhysicalDamageForm.Slashing,
                                Enhancement = 0, 
                                EnhancementTotal = 0
                            },
                            Energy = DamageEnergyType.Fire
                        })
                .Configure();
        }
    }
}
