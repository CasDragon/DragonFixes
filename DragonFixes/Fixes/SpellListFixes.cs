using BlueprintCore.Blueprints.CustomConfigurators.Classes.Spells;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonFixes.Util;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes
{
    internal class SpellListFixes
    {
        [DragonConfigure]
        public static void WizardSpellListPatch()
        {
            if (Settings.GetSetting<bool>("fingerofdeath"))
            {
                Main.log.Log("Patching Wizard spell list to remove dupe Finger of Death");
                BlueprintSpellList bp = SpellListRefs.WizardSpellList.Reference.Get();
                BlueprintAbility spell = AbilityRefs.FingerOfDeathSithhud.Reference.Get();
                DragonHelpers.RemoveSpellFromSpellList(bp, spell, 7);
                foreach (var x in spell.GetComponents<SpellListComponent>())
                {
                    DragonHelpers.RemoveComponent<SpellListComponent>(spell);
                }
                DragonHelpers.RemoveComponent<CraftInfoComponent>(spell);
                if (ModCompat.tttbase)
                {
                    Main.log.Log("Dupe Finger of Death remains due to TTT-Base, changing name so people know");
                    AbilityConfigurator.For(spell)
                        .SetDisplayName(Main.tttbrokenname)
                        .SetDescription(Main.tttbrokendescription)
                        .SetHidden(true)
                        .Configure();
                }
            }
        }
        [DragonConfigure]
        public static void ClericSpellListPatch()
        {
            if (Settings.GetSetting<bool>("breathofflies"))
            {
                Main.log.Log("Patching Cleric spell list to remove Breath of Flies");
                BlueprintSpellList bp = SpellListRefs.ClericSpellList.Reference.Get();
                DragonHelpers.RemoveSpellFromSpellList(bp, AbilityRefs.BreathofFliesAbility.Reference.Get(), 7);
                BlueprintAbility spell = AbilityRefs.BreathofFliesAbilitySithhud.Reference.Get();
                foreach (var x in spell.GetComponents<SpellListComponent>())
                {
                    DragonHelpers.RemoveComponent<SpellListComponent>(spell);
                }
                DragonHelpers.RemoveComponent<CraftInfoComponent>(spell);
                if (ModCompat.tttbase)
                {
                    Main.log.Log("Breath of Flies remains in wrong spell list due to TTT-Base, changing name so people know");
                    AbilityConfigurator.For(spell)
                        .SetDisplayName(Main.tttbrokenname)
                        .SetDescription(Main.tttbrokendescription)
                        .Configure();
                }
            }
        }
    }
}
