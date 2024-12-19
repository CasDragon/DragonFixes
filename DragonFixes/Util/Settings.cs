using DragonFixes;
using Kingmaker.Localization;
using ModMenu.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityModManagerNet;

namespace DragonFixes.Util
{
    internal class Settings
    {
        private static readonly string RootKey = "dragonfixes";

        public static void InitializeSettings()
        {
            ModMenu.ModMenu.AddSettings(
                SettingsBuilder.New(RootKey, CreateString(GetKey("es-title"), "Eldritch Scion"))
                    .SetMod(Main.entry)
                    .AddToggle(
                        Toggle.New(GetKey("esarcaneaccuracy"), defaultValue: true, CreateString("esarcaneaccuracy-toggle", "Fixes Eldritch Scions version of Arcane Accuracy to correctly scale off CHA.")))
                    .AddToggle(
                        Toggle.New(GetKey("esprescientduration"), defaultValue: true, CreateString("esprescientduration-toggle", "Change Edlritch Scions version of Prescient Strike to correctly last for 2 rounds instead of 1")))
                    .AddToggle(
                        Toggle.New(GetKey("esextraarcanaselection"), defaultValue: true, CreateString("esextraarcanaselection-toggle", "The Feat Extra Arcana will now allow Eldritch Scrion to add an extra version of their Arcana and not regular versions")))
                    .AddAnotherSettingsGroup(GetKey("various"), CreateString(GetKey("various-title"), "Various Fixes"))
                    .AddToggle(
                        Toggle.New(GetKey("oracleapsurestrictionremoval"), defaultValue: true, CreateString("oracleapsurestrictionremoval-toggle", "Allow Oracle to select Apsu as a deity")))
                    .AddToggle(
                        Toggle.New(GetKey("curespellstargetfix"), defaultValue: true, CreateString("curespellstargetfix-toggle", "Fixes several healing spells (Like Heal and Cure Light Wounds) to target enemies")))
                    .AddToggle(
                        Toggle.New(GetKey("abundantarcanepool"), defaultValue: true, CreateString("abundantarcanepool-toggle", "Allow Spell Dancer archetype to select Abundant Arcane Pool.")))
                    .AddToggle(
                        Toggle.New(GetKey("scalykind"), defaultValue: true, CreateString("scalykind-toggle", "Include Scalykind domain in the second domain selection"))));
        }
        public static T GetSetting<T>(string key)
        {
            try
            {
                return ModMenu.ModMenu.GetSettingValue<T>(GetKey(key));
            }
            catch (Exception ex)
            {
                Main.log.Error(ex.ToString());
                return default(T);
            }
        }
        private static LocalizedString CreateString(string partialkey, string text)
        {
            return Helpers.CreateString(GetKey(partialkey), text);
        }
        private static string GetKey(string partialKey)
        {
            return $"{RootKey}.{partialKey}";
        }

    }
    public static class Helpers
    {
        private static Dictionary<string, LocalizedString> textToLocalizedString = new Dictionary<string, LocalizedString>();
        public static LocalizedString CreateString(string key, string value)
        {
            // See if we used the text previously.
            // (It's common for many features to use the same localized text.
            // In that case, we reuse the old entry instead of making a new one.)
            if (textToLocalizedString.TryGetValue(value, out LocalizedString localized))
            {
                return localized;
            }
            var strings = LocalizationManager.CurrentPack?.m_Strings;
            if (strings!.TryGetValue(key, out var oldValue) && value != oldValue.Text)
            {
                Main.log.Log($"Info: duplicate localized string `{key}`, different text.");
            }
            var sE = new Kingmaker.Localization.LocalizationPack.StringEntry();
            sE.Text = value;
            strings[key] = sE;
            localized = new LocalizedString
            {
                m_Key = key
            };
            textToLocalizedString[value] = localized;
            return localized;
        }
    }
}

