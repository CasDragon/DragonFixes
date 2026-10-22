using System.Linq;
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

    [DragonConfigure]
    public static void FixSpellbookProgression()
    {
        Main.log.Log("Fixing the Loremaster spellbook progression");
        var hunterloremaster = ProgressionRefs.LoremasterHunterProgression.Reference.Get();
        var hunterlevel = FeatureRefs.LoremasterHunterLevelUp.Reference.Get().ToReference<BlueprintFeatureBaseReference>();
        var rangerlevel = FeatureRefs.LoremasterRangerLevelUp.ToString();
        hunterloremaster.LevelEntries[4].m_Features =
        [
            .. hunterloremaster.LevelEntries[4].m_Features
                .Where(f => f.guid == rangerlevel),
            hunterlevel
        ];
        hunterloremaster.LevelEntries[9].m_Features =
        [
            .. hunterloremaster.LevelEntries[9].m_Features
                .Where(f => f.guid == rangerlevel),
            hunterlevel
        ];
        hunterloremaster.LevelEntries[10].m_Features =
        [
            .. hunterloremaster.LevelEntries[10].m_Features
                .Where(f => f.guid == rangerlevel),
            hunterlevel
        ];
        hunterloremaster.LevelEntries[11].m_Features =
        [
            .. hunterloremaster.LevelEntries[11].m_Features
                .Where(f => f.guid == rangerlevel),
            hunterlevel
        ];
        hunterloremaster.LevelEntries[12].m_Features =
        [
            .. hunterloremaster.LevelEntries[12].m_Features
                .Where(f => f.guid == rangerlevel),
            hunterlevel
        ];
    }
}