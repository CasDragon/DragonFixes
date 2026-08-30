using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.Items;

public class Lawbringer
{
    [DragonConfigure]
    public static void PatchEnchant()
    {
        Main.log.Log("Patching LawbringerEnchantment to include more rages");
        WeaponEnchantmentConfigurator.For(WeaponEnchantmentRefs.LawbringerEnchantment)
            .EditComponent<AddInitiatorAttackWithWeaponTrigger>(EditAddInitiatorAttackWithWeaponTrigger)
            .Configure();
    }

    public static void EditAddInitiatorAttackWithWeaponTrigger(AddInitiatorAttackWithWeaponTrigger component)
    {
        var c1 = new ContextActionRemoveBuff() { m_Buff = BuffRefs.StandartFocusedRageBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c2 = new ContextActionRemoveBuff() { m_Buff = BuffRefs.InciteRageEffectBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c3 = new ContextActionRemoveBuff() { m_Buff = BuffRefs.BloodragerStandartRageBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c4 = new ContextActionRemoveBuff() { m_Buff = BuffRefs.ElementalRampagerRampageBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c5 = new ContextActionRemoveBuff() { m_Buff = BuffRefs.RageshaperDevastatingFormBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c6 = new ContextActionRemoveBuff() { m_Buff = BuffRefs.RageBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c7 = new ContextActionRemoveBuff() { m_Buff = BuffRefs.Gorum_Buff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c8 = new ContextActionRemoveBuff() { m_Buff = BuffRefs.DemonRageBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c9 = new ContextActionRemoveBuff() { m_Buff = BuffRefs.InspiredRageEffectBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c10 = new ContextActionRemoveBuff() { m_Buff = BuffRefs.InspiredRageEffectBuffMythic.Reference.Get().ToReference<BlueprintBuffReference>() };
        component.Action.Actions = [.. component.Action.Actions, c1, c2 , c3, c4, c5, c6, c7, c8, c9, c10];
    }
}