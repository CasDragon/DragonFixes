using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;

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
}