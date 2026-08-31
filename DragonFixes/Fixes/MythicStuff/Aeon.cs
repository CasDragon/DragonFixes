using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.MythicStuff
{
    internal class Aeon
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
            var dispel = (ContextActionDispelMagic)bp.GetComponent<AbilityEffectRunAction>().Actions.Actions[0];
            dispel.ContextBonus.ValueRank = AbilityRankType.DamageDice;
            dispel.ContextBonus.Value = 1;
        }

        [DragonConfigure]
        public static void PatchCrystalMind()
        {
            Main.log.Log("Patching CrystalMind to include more rages");
            AbilityConfigurator.For(AbilityRefs.CrystalMind)
                .EditComponent<AbilityEffectRunAction>(EditAbilityEffectRunAction)
                .Configure();
        }

        public static void EditAbilityEffectRunAction(AbilityEffectRunAction component)
        {
            var c1 = new ContextActionRemoveBuff()
                { m_Buff = BuffRefs.StandartFocusedRageBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
            var c2 = new ContextActionRemoveBuff()
                { m_Buff = BuffRefs.InciteRageEffectBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
            var c3 = new ContextActionRemoveBuff()
                { m_Buff = BuffRefs.InspiredRageEffectBuffMythic.Reference.Get().ToReference<BlueprintBuffReference>() };
            var c4 = new ContextActionRemoveBuff()
            {
                m_Buff = BuffRefs.ElementalRampagerRampageBuff.Reference.Get().ToReference<BlueprintBuffReference>()
            };
            var c5 = new ContextActionRemoveBuff()
            {
                m_Buff = BuffRefs.RageshaperDevastatingFormBuff.Reference.Get().ToReference<BlueprintBuffReference>()
            };
            var c6 = new ContextActionRemoveBuff()
                { m_Buff = BuffRefs.RageBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
            var c7 = new ContextActionRemoveBuff()
                { m_Buff = BuffRefs.InspiredRageEffectBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
            var c8 = new ContextActionRemoveBuff()
                { m_Buff = BuffRefs.DemonRageBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
            var c9 = new ContextActionRemoveBuff()
                { m_Buff = BuffRefs.RageSpellBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
            component.Actions.Actions = [.. component.Actions.Actions, c1, c2, c3, c4, c5, c6, c7, c8, c9];
        }
    }
}
