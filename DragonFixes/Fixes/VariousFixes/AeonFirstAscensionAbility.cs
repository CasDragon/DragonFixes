using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.VariousFixes
{
    internal class AeonFirstAscensionAbility
    {
        [DragonConfigure]
        public static void PatchAeonFirstAscensionAbility()
        {
            Main.log.Log("Patching AeonFirstAscensionAbility to have correct DC for dispelling.");
            var bp = AbilityRefs.AeonFirstAscensionAbility.Reference.Get();
            var comp = bp.GetComponents<ContextRankConfig>();
            var crc1 = comp.Where(c => c.m_BaseValueType == ContextRankBaseValueType.MythicLevel).FirstOrDefault();
            var crc2 = comp.Where(c => c.m_BaseValueType == ContextRankBaseValueType.CasterLevel).FirstOrDefault();
            crc1.m_Type = AbilityRankType.DamageDice;
            crc2.m_Progression = ContextRankProgression.AsIs;
            crc2.m_BaseValueType = ContextRankBaseValueType.CharacterLevel;
            var dispel = (ContextActionDispelMagic) bp.GetComponent<AbilityEffectRunAction>().Actions.Actions[0];
            dispel.ContextBonus.ValueRank = AbilityRankType.DamageDice;
            dispel.ContextBonus.Value = 1;
        }
    }
}
