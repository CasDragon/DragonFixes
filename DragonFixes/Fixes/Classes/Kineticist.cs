using System.Linq;
using BlueprintCore.Blueprints.Configurators.UnitLogic.Properties;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using DragonFixes.Util;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace DragonFixes.Fixes.Classes;

public class Kineticist
{

    [DragonConfigure]
    public static void PatchGrapplingInfusionBuff()
    {
        if (ModCompat.pp) return;
        // Apparently DC currently is 10 + CHA? 
        Main.log.Log("Patching Grappling Infusion Buff to have actual DC");
        BuffConfigurator.For(BuffRefs.GrapplingInfusionBuff)
            .EditComponent<AddKineticistInfusionDamageTrigger>(GrapplingChange)
            .Configure();
    }

    public static void GrapplingChange(AddKineticistInfusionDamageTrigger component)
    {
        var condition = component.Actions.Actions.First(c => c is Conditional) as 
            Conditional;
        var saving = condition!.IfTrue.Actions.First(c => c is ContextActionSavingThrow) as 
            ContextActionSavingThrow;
        saving!.UseDCFromContextSavingThrow = false;
        saving!.HasCustomDC = true;

        var property = UnitPropertyConfigurator.New("grapplinginfusionproperty", Guids.grapplinginfusionproperty)
            .AddClassLevelGetter(clazz: CharacterClassRefs.KineticistClass.ToString(),
                settings: new PropertySettings()
                {
                    m_Progression = PropertySettings.Progression.AsIs,
                    m_StartLevel = 0,
                    m_StepLevel = 0,
                    m_Negate = false
                })
            .AddKineticistMainStatBonusPropertyGetter()
            .SetBaseValue(10)
            .SetOperationOnComponents(BlueprintUnitProperty.MathOperation.Sum)
            .Configure();

        var x = new ContextValue()
        {
            ValueType =  ContextValueType.CasterCustomProperty,
            Value = 0,
            ValueRank = AbilityRankType.Default,
            ValueShared = AbilitySharedValue.Damage,
            Property = UnitProperty.None,
            m_CustomProperty = property.ToReference<BlueprintUnitPropertyReference>(),
            m_AbilityParameter = AbilityParameterType.Level
        };

        saving!.CustomDC = x;
    }
    
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
                .EditComponent<AbilityKineticist>(changeinfusions)
                .Configure();
        }
    }
    public static void changeinfusions(AbilityKineticist component)
    {
        component.BlastBurnCost = 0;
        component.InfusionBurnCost = 2;
    }

    /// <summary>
    /// A composite blast should deal double a simple blast's dice with full Constitution -
    /// i.e. step2/AsIs (only Blue Flame is an energy composite, which halves Con).
    ///
    /// Metal's two Deadly Earth areas stack step1/Div2 on top of
    /// Half:true, halving twice.
    ///
    /// Ice Blast, blade and Fragmentation sit at step1/Div2, dealing simple-blast damage
    /// at composite burn cost. Ice's Spindle and Wall are already correct; leave them.
    /// </summary>
    [DragonConfigure]
    public static void FixCompositeBlastDamageScaling()
    {
        Main.log.Log("Fixing Ice Blast and Metal Deadly Earth to deal composite blast damage instead of simple blast damage.");
        Blueprint<BlueprintReference<BlueprintAbility>>[] iceBlastAbilities = [
            AbilityRefs.IceBlastAbility,
            AbilityRefs.IceBlastBladeDamage,
            AbilityRefs.FragmentationIceBlastAbility
        ];
        foreach (var iceBlastAbility in iceBlastAbilities)
        {
            AbilityConfigurator.For(iceBlastAbility)
                .EditComponents<ContextRankConfig>(RestoreCompositeScaling, c => true)
                .Configure();
        }
        Blueprint<BlueprintReference<BlueprintAbilityAreaEffect>>[] deadlyEarthAreas = [
            AbilityAreaEffectRefs.DeadlyEarthMetalBlastArea,
            AbilityAreaEffectRefs.DeadlyEarthMetalBlastAreaRare
        ];
        foreach (var deadlyEarthArea in deadlyEarthAreas)
        {
            AbilityAreaEffectConfigurator.For(deadlyEarthArea)
                .EditComponents<ContextRankConfig>(RestoreCompositeScaling, c => true)
                .Configure();
        }
    }

    private static void RestoreCompositeScaling(ContextRankConfig config)
    {
        if (config.m_Type == AbilityRankType.DamageDice)
        {
            config.m_Progression = ContextRankProgression.MultiplyByModifier;
            config.m_StepLevel = 2;
        }
        else if (config.m_Type == AbilityRankType.DamageBonus)
        {
            config.m_Progression = ContextRankProgression.AsIs;
        }
    }
}