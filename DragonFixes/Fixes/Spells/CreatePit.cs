using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;

namespace DragonFixes.Fixes.Spells;

public class CreatePit
{
    [DragonConfigure]
    public static void PatchCreatePitArea()
    {
        Main.log.Log("Patching CreatePitArea to include more Wings features");
        var x = AbilityAreaEffectRefs.CreatePitArea.Reference.Get().GetComponent<AreaEffectPit>();
        x!.m_EffectsImmunityFacts = [
            .. x.m_EffectsImmunityFacts, 
            FeatureRefs.ShifterGriffonWingsFeature.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            FeatureRefs.ShifterFeyWingsFeature.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            FeatureRefs.ShifterAspectFiendWingsFeature.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.ShifterWildShapeGriffonBuff.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.ShifterWildShapeGriffonBuff9.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.ShifterWildShapeGriffonBuff14.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.ShifterWildShapeGriffonGodBuff.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.ShifterWildShapeGriffonGodBuff9.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.ShifterWildShapeGriffonGodBuff14.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.ShifterWildShapeGriffonDemonBuff.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.ShifterWildShapeGriffonDemonBuff9.Reference.Get().ToReference<BlueprintUnitFactReference>(),
            BuffRefs.ShifterWildShapeGriffonDemonBuff14.Reference.Get().ToReference<BlueprintUnitFactReference>()
        ];
    }
}