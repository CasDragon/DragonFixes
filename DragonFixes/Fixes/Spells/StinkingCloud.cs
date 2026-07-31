using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Mechanics.Conditions;

namespace DragonFixes.Fixes.Spells;

public class StinkingCloud
{
    private const string settingname = "stinkycloud";
    private const string settingdescription = "Fix Stinking Cloud to only call for 1 save with TTT installed";
    [DragonConfigure]
    public static void PatchStinkingCloud()
    {
        if (SettingsAction.GetSetting<bool>(settingname))
        {
            Main.log.Log("Patching StinkingCloudArea to actually trigger");
            AbilityAreaEffectConfigurator.For(AbilityAreaEffectRefs.StinkingCloudArea)
                .EditComponent<AbilityAreaEffectRunAction>(c =>
                    c.UnitEnter.Actions.OfType<Conditional>().FirstOrDefault()
                        .ConditionsChecker.Conditions.OfType<ContextConditionHasBuff>().FirstOrDefault()
                        .Not = true)
                .Configure();
        }
    }
}