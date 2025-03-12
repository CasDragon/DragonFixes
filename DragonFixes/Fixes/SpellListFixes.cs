using BlueprintCore.Blueprints.CustomConfigurators.Classes.Spells;
using BlueprintCore.Blueprints.References;
using DragonFixes.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes
{
    internal class SpellListFixes
    {
        [DragonFix]
        public static void WizardSpellListPatch()
        {
            if (Settings.GetSetting<bool>("fingerofdeath"))
            {
                Main.log.Log("Patching Wizard spell list to remove dupe Finger of Death");
                SpellListConfigurator.For(SpellListRefs.WizardSpellList)
                .RemoveFromSpellsByLevel(new SpellLevelList(7) { m_Spells = [AbilityRefs.FingerOfDeathSithhud.Reference.Get().ToReference<BlueprintAbilityReference>()] })
                .Configure();
            }
        }
        [DragonFix]
        public static void ClericSpellListPatch()
        {
            if (Settings.GetSetting<bool>("breathofflies"))
            {
                Main.log.Log("Patching Cleric spell list to remove Breath of Flies");
                SpellListConfigurator.For(SpellListRefs.WizardSpellList)
                .RemoveFromSpellsByLevel(new SpellLevelList(7) { m_Spells = [AbilityRefs.BreathofFliesAbility.Reference.Get().ToReference<BlueprintAbilityReference>()] })
                .Configure();
            }
        }
    }
}
