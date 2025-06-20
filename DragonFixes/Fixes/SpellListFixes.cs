using BlueprintCore.Blueprints.CustomConfigurators.Classes.Spells;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
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
                BlueprintSpellList bp = SpellListRefs.WizardSpellList.Reference.Get();
                LibraryStuff.RemoveSpellFromSpellList(bp, AbilityRefs.FingerOfDeathSithhud.Reference.Get(), 7);
            }
        }
        [DragonFix]
        public static void ClericSpellListPatch()
        {
            if (Settings.GetSetting<bool>("breathofflies"))
            {
                Main.log.Log("Patching Cleric spell list to remove Breath of Flies");
                BlueprintSpellList bp = SpellListRefs.ClericSpellList.Reference.Get();
                LibraryStuff.RemoveSpellFromSpellList(bp, AbilityRefs.BreathofFliesAbility.Reference.Get(), 7);
            }
        }
    }
}
