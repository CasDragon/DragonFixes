using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace DragonFixes.Fixes.Spells;

public class JoyfulRapture
{
    [DragonConfigure]
    public static void PatchJoyfulRapture()
    {
        Main.log.Log("Patching Joyful Rapture to correctly dispel Negative Emotion instead of petrified");
        AbilityConfigurator.For(AbilityRefs.JoyfulRapture)
            .EditComponent<AbilityEffectRunAction>(c => c.Actions.Actions
                .OfType<ContextActionDispelMagic>()
                .First()
                .Descriptor = SpellDescriptor.NegativeEmotion)
            .Configure();
    }
}