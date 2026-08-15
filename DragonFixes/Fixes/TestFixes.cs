using BlueprintCore.Blueprints.Configurators.DialogSystem;

namespace DragonFixes.Fixes
{
    internal class TestFixes
    {
       // [DragonConfigure]
       // Test to make sure it worked, but I would need to get localization for the normal key for every other language to not make it show English
        public static void LocalizationNonsense()
        {
            CueConfigurator.For("0df3b5e250906534eac207b3dc5a5d07")
                .SetText(Main.breetypokey)
                .Configure();
        }
    }
}
