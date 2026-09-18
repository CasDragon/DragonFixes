using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Prerequisites;

namespace DragonFixes.Fixes.Classes;

public static class Loremaster
{
    [DragonConfigure]
    public static void AddDLCMetamagicToPrereq()
    {
        Main.log.Log("Adding Intensify metamagic to Loremaster prereqs");
        CharacterClassConfigurator.For(CharacterClassRefs.LoremasterClass)
            .EditComponent<PrerequisiteFeaturesFromList>(c => c.m_Features = 
                [..c.m_Features, FeatureRefs.IntensifiedSpell.Reference.Get().ToReference<BlueprintFeatureReference>()])
            .Configure();
    }
}