using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.DialogSystem;
using BlueprintCore.Utils.Types;
using DragonLibrary.Utils;

namespace DragonFixes.Fixes.Units;

public class Aivu
{
    [DragonConfigure]
    public static void GiveAivuSwarmPoints()
    {
        Main.log.Log("Giving Aivu a value for swarm size");
        AnswerConfigurator.For("f85e4e6aee1ae964da765e705bbfbe95")
            .ModifyOnSelect(s => s.Actions = [.. s.Actions, ActionsBuilder.New().IncreaseSwarmThatWalksStrength(ContextValues.Constant(20)).Build().Actions[0]])
            .Configure();
    }
}