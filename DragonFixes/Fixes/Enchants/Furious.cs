using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Mechanics.Conditions;

namespace DragonFixes.Fixes.Enchants;

public class Furious
{
    [DragonConfigure]
    public static void PatchEnchant()
    {
        Main.log.Log("Patching Furious to include more rages");
        WeaponEnchantmentConfigurator.For(WeaponEnchantmentRefs.Furious)
            .EditComponent<WeaponConditionalEnhancementBonus>(EditWeaponConditionalEnhancementBonus)
            .Configure();
    }

    public static void EditWeaponConditionalEnhancementBonus(WeaponConditionalEnhancementBonus component)
    {
        var c1 = new ContextConditionHasBuff() { m_Buff = BuffRefs.StandartFocusedRageBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c2 = new ContextConditionHasBuff() { m_Buff = BuffRefs.InciteRageEffectBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c3 = new ContextConditionHasBuff() { m_Buff = BuffRefs.BloodragerStandartRageBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c4 = new ContextConditionHasBuff() { m_Buff = BuffRefs.ElementalRampagerRampageBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c5 = new ContextConditionHasBuff() { m_Buff = BuffRefs.RageshaperDevastatingFormBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c6 = new ContextConditionHasBuff() { m_Buff = BuffRefs.RageBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c7 = new ContextConditionHasBuff() { m_Buff = BuffRefs.Gorum_Buff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c8 = new ContextConditionHasBuff() { m_Buff = BuffRefs.DemonRageBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c9 = new ContextConditionHasBuff() { m_Buff = BuffRefs.InspiredRageEffectBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c10 = new ContextConditionHasBuff() { m_Buff = BuffRefs.InspiredRageEffectBuffMythic.Reference.Get().ToReference<BlueprintBuffReference>() };
        component.Conditions.Conditions = [.. component.Conditions.Conditions, c1, c2 , c3, c4, c5, c6, c7, c8, c9, c10];
    }
}