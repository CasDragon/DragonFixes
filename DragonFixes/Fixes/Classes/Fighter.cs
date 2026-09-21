using System.Linq;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace DragonFixes.Fixes.Classes;

public class Fighter
{
    // Two Handed Fighter
    [DragonConfigure]
    public static void PatchTwoHandedFighterPiledriverFeature()
    {
        Main.log.Log("Patching TwoHandedFighterPiledriverFeature to correctly remove the bullrush buff");
        if (FeatureRefs.TwoHandedFighterPiledriverFeature.Reference.Get().Components
                .FirstOrDefault(c => 
                    c is AddAbilityUseTrigger x
                    && x.m_Ability.Equals(AbilityRefs.TwoHandedFighterPiledriverBullRushAbility.Reference.Get()
                        .ToReference<BlueprintAbilityReference>())
                    && x.Action.Actions[0] is ContextActionRemoveBuff) is not AddAbilityUseTrigger component)
        {
            Main.log.Log("Component is null");
            return;
        }
        var removeBuff = component.Action.Actions[0] as ContextActionRemoveBuff;
        removeBuff?.m_Buff = BuffRefs.TwoHandedFighterPiledriverBullRushBuff.Reference.Get().ToReference<BlueprintBuffReference>();
    }
}