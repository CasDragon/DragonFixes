using System.Linq;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace DragonFixes.Fixes.MythicStuff;

public class Lich
{
    [DragonConfigure]
    public static void PatchSkeltalRage()
    {
        Main.log.Log("Patching LichSkeletalRageParametrized to include dlc rage features");
        ParametrizedFeatureConfigurator.For(ParametrizedFeatureRefs.LichSkeletalRageParametrized)
            .AddToBlueprintParameterVariants(FeatureRefs.ClearMindFeature.ToString(),
                FeatureRefs.ComeAndGetMeFeature.ToString())
            .Configure();
    }

    [DragonConfigure]
    public static void PowerFromDeath()
    {
        Main.log.Log("Fixing duration of haste buff from Power From Death and changing it's duration localization");
        var ability = AbilityConfigurator.For(AbilityRefs.PowerFromDeath)
            .SetLocalizedDuration(Duration.MinutePerLevel)
            .Configure();
        var action = ability.m_AllElements.First(e =>
            e is ContextActionApplyBuff x &&
            x.m_Buff.deserializedGuid == BuffRefs.HasteBuff.Reference.deserializedGuid) as ContextActionApplyBuff;
        action?.DurationValue.Rate = DurationRate.Minutes;
    }
}