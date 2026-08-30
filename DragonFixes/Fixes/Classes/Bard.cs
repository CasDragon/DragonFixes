using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.Classes;

public class Bard
{
    [DragonConfigure]
    public static void PatchInspireTranquilityEffectBuff()
    {
        Main.log.Log("Patching InspireTranquilityEffectBuff to include more rages");
        BuffConfigurator.For(BuffRefs.InspireTranquilityEffectBuff)
            .EditComponent<SuppressBuffs>(EditSuppressBuffs)
            .Configure();
        BuffConfigurator.For(BuffRefs.InspireTranquilityEffectBuffMythic)
            .EditComponent<SuppressBuffs>(EditSuppressBuffs)
            .Configure();
    }

    public static void EditSuppressBuffs(SuppressBuffs component)
    {
        component.m_Buffs =
        [
            .. component.m_Buffs,
            BuffRefs.StandartFocusedRageBuff.Reference.Get().ToReference<BlueprintBuffReference>(),
            BuffRefs.InciteRageEffectBuff.Reference.Get().ToReference<BlueprintBuffReference>(),
            BuffRefs.ElementalRampagerRampageBuff.Reference.Get().ToReference<BlueprintBuffReference>(),
            BuffRefs.RageshaperDevastatingFormBuff.Reference.Get().ToReference<BlueprintBuffReference>(),
            BuffRefs.RageSpellBuff.Reference.Get().ToReference<BlueprintBuffReference>(),
            BuffRefs.InspiredRageEffectBuff.Reference.Get().ToReference<BlueprintBuffReference>(),
            BuffRefs.InspiredRageEffectBuffMythic.Reference.Get().ToReference<BlueprintBuffReference>()
        ];
    }
}