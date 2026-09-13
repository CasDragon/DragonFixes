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
        Main.log.Log(
            "Patching Spindle / Exploding Arrows infusions to use InfusionBurnCost instead of BlastBurnCost, and restoring the missing composite blast surcharge.");
        const int simpleBlastBurnCost = 0;
        const int compositeBlastBurnCost = 2;
        (Blueprint<BlueprintReference<BlueprintAbility>> Ability, int BlastBurnCost)[]
            infusionAbilities =
            [
                (AbilityRefs.SpindleAirBlastAbility, simpleBlastBurnCost),
                (AbilityRefs.SpindleColdBlastAbility, simpleBlastBurnCost),
                (AbilityRefs.SpindleEarthBlastAbility, simpleBlastBurnCost),
                (AbilityRefs.SpindleElectricBlastAbility, simpleBlastBurnCost),
                (AbilityRefs.SpindleFireBlastAbility, simpleBlastBurnCost),
                (AbilityRefs.SpindleWaterBlastAbility, simpleBlastBurnCost),
                (AbilityRefs.ExplodingArrowsAirBlastAbility, simpleBlastBurnCost),
                (AbilityRefs.ExplodingArrowsColdBlastAbility, simpleBlastBurnCost),
                (AbilityRefs.ExplodingArrowsEarthBlastAbility, simpleBlastBurnCost),
                (AbilityRefs.ExplodingArrowsElectricBlastAbility, simpleBlastBurnCost),
                (AbilityRefs.ExplodingArrowsFireBlastAbility, simpleBlastBurnCost),
                (AbilityRefs.ExplodingArrowsWaterBlastAbility, simpleBlastBurnCost),
                (AbilityRefs.SpindleBlizzardBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.SpindleBloodBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.SpindleBlueFlameBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.SpindleChargedWaterBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.SpindleIceBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.SpindleMagmaBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.SpindleMetalBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.SpindleMudBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.SpindlePlasmaBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.SpindleSandstormBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.SpindleSteamBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.SpindleThunderstormBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.ExplodingArrowsBlizzardBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.ExplodingArrowsBlueFlameBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.ExplodingArrowsChargedWaterBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.ExplodingArrowsIceBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.ExplodingArrowsMagmaBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.ExplodingArrowsMetalBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.ExplodingArrowsMudBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.ExplodingArrowsPlasmaBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.ExplodingArrowsSandstormBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.ExplodingArrowsSteamBlastAbility, compositeBlastBurnCost),
                (AbilityRefs.ExplodingArrowsThunderstormBlastAbility, compositeBlastBurnCost)
            ];

        foreach (var (ability, blastBurnCost) in infusionAbilities)
        {
            AbilityConfigurator.For(ability)
                .EditComponent<AbilityKineticist>(component =>
                    ChangeInfusionBurnCost(component, blastBurnCost))
                .Configure();
        }
    }

    private static void ChangeInfusionBurnCost(AbilityKineticist component, int blastBurnCost)
    {
        component.BlastBurnCost = blastBurnCost;
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