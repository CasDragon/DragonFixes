using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.Mechanics.Facts;

namespace DragonFixes.Fixes.UnitFeatures;

public class UncannyDodge
{
    [DragonConfigure]
    public static void PatchUncannyDodgeTalent()
    {
        Main.log.Log("Removing circular logic from UncannyDodgeTalent.");
        BlueprintFeature x = FeatureRefs.UncannyDodgeTalent.Reference.Get();
        DragonHelpers.RemoveComponent<RecalculateOnFactsChange>(x);
        DragonHelpers.RemoveComponent<AddFeatureIfHasFact>(x);
        DragonHelpers.RemoveComponent<AddFeatureIfHasFact>(x);
        FeatureConfigurator.For(x)
            .AddFacts([FeatureRefs.UncannyDodgeChecker.Reference.Get()])
            .Configure();
    }

    [DragonConfigure]
    public static void AddArchetypes()
    {
        Main.log.Log("Adding Sylvan Trickster archetype to UncannyDodge.");
        FeatureConfigurator.For(FeatureRefs.UncannyDodgeTalent)
            .AddPrerequisiteArchetypeLevel(group: Prerequisite.GroupType.Any,
                characterClass: CharacterClassRefs.RogueClass.ToString(),
                archetype: ArchetypeRefs.SylvanTricksterArchetype.ToString(),
                level: 4)
            .Configure();
    }
}