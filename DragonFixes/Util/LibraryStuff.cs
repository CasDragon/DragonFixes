using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;

namespace DragonFixes.Util
{
    internal class LibraryStuff
    {
        public static void RemoveComponent(BlueprintScriptableObject blueprint, BlueprintComponent component)
        {
            blueprint.Components = blueprint.Components.Except([component]).ToArray();
        }
        public static void RemoveComponent<T>(BlueprintScriptableObject blueprint)
            where T : BlueprintComponent
        {
            blueprint.Components = blueprint.Components.Except([blueprint.GetComponent<T>()]).ToArray();
        }
        public static void RemoveSpellFromSpellList(BlueprintSpellList spellList, BlueprintAbility spell, int spellLevel)
        {
            BlueprintAbilityReference spellReference = spell.ToReference<BlueprintAbilityReference>();
            spellList.SpellsByLevel[spellLevel].m_Spells = spellList.SpellsByLevel[spellLevel].m_Spells.Except([spellReference]).ToList();
        }
    }
}
