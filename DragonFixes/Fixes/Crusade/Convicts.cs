using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Armies.TacticalCombat.Components;

namespace DragonFixes.Fixes.Crusade;

public class Convicts
{
    [DragonConfigure]
    public static void Fix1()
    {
        Main.log.Log("Fix morale loss on Convict crusade unit");
        FeatureConfigurator.For(FeatureRefs.ArmyLoseMoraleOnHit)
            .EditComponent<ArmyFullAttackEndTrigger>(c => c.ShouldBeInitiator = true)
            .Configure();
    }
}