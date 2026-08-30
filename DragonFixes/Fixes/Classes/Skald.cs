using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.Classes;

public class Skald
{
    [DragonConfigure]
    public static void PatchInspiredRageEffectBuff()
    {
        Main.log.Log("Patching InspiredRageEffectBuff to include more rages");
        BuffConfigurator.For(BuffRefs.InspiredRageEffectBuff)
            .EditComponent<AddFactContextActions>(EditAddFactContextActions)
            .Configure();
        BuffConfigurator.For(BuffRefs.InspiredRageEffectBuffMythic)
            .EditComponent<AddFactContextActions>(EditAddFactContextActions)
            .Configure();
    }

    public static void EditAddFactContextActions(AddFactContextActions component)
    {
        var c1 = new ContextActionRemoveBuff() { m_Buff = BuffRefs.RageSpellBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c2 = new ContextActionRemoveBuff() { m_Buff = BuffRefs.InciteRageEffectBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c3 = new ContextActionRemoveBuff() { m_Buff = BuffRefs.RageBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c4 = new ContextActionRemoveBuff() { m_Buff = BuffRefs.ElementalRampagerRampageBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        var c5 = new ContextActionRemoveBuff() { m_Buff = BuffRefs.RageshaperDevastatingFormBuff.Reference.Get().ToReference<BlueprintBuffReference>() };
        component.Activated.Actions = [.. component.Activated.Actions, c1, c2 , c3, c4, c5];
    }
}