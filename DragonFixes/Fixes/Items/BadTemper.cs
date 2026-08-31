using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;

namespace DragonFixes.Fixes.Items;

public class BadTemper
{
    [DragonConfigure]
    public static void PatchFeature()
    {
        Main.log.Log("Patching BadTemperFeature to include more rages");
        var buff = BuffRefs.BadTemperBuff.ToString();
        FeatureConfigurator.For(FeatureRefs.BadTemperFeature)
            .AddBuffExtraEffects(
                checkedBuff: BuffRefs.InciteRageEffectBuff.ToString(),
                extraEffectBuff: buff
            )
            .AddBuffExtraEffects(
                checkedBuff: BuffRefs.ElementalRampagerRampageBuff.ToString(),
                extraEffectBuff: buff
            )
            .AddBuffExtraEffects(
                checkedBuff: BuffRefs.RageshaperDevastatingFormBuff.ToString(),
                extraEffectBuff: buff
            )
            .AddBuffExtraEffects(
                checkedBuff: BuffRefs.RageSpellBuff.ToString(),
                extraEffectBuff: buff
            )
            .AddBuffExtraEffects(
                checkedBuff: BuffRefs.Gorum_Buff.ToString(),
                extraEffectBuff: buff
            )
            .AddBuffExtraEffects(
                checkedBuff: BuffRefs.InspiredRageEffectBuffBeforeMasterSkald.ToString(),
                extraEffectBuff: buff
            )
            .Configure();
    }
}