using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;

namespace DragonFixes.Fixes.Classes;

public class Hunter
{
    [DragonConfigure]
    public static void PatchScapeGoat()
    {
        Main.log.Log("Patching Scapegoat to work on allies?");
        DragonHelpers.RemoveComponent<AbilityTargetHasFact>(AbilityRefs.ScapegoatAbilityAlly.Reference.Get());
        DragonHelpers.RemoveComponent<AbilityTargetHasFact>(AbilityRefs.ScapegoatAbilityAllyPet.Reference.Get());
    }
}