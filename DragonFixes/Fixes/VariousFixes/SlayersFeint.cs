using System.Linq;
using BlueprintCore.Utils;
using DragonFixes.Util;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Actions;
using TabletopTweaks.Core.Utilities;

namespace DragonFixes.Fixes.VariousFixes;

public class SlayersFeintFix
{
    [DragonConfigure]
    public static void FixFeintCheckForCaster()
    {
        if (!Settings.GetSetting<bool>("feintcheckforcaster"))
        {
            Main.log.Log("Feint CheckForCaster patch disabled, skipping.");
            return;
        }
        Main.log.Log("Fixing Slayer's Feint skill checks to roll for the caster instead of the target.");
        // CheckForCaster = true makes the caster of the ability roll the check, not the target.
        // Notably, this only happened For enemies that were more perceptive than BAB+Wis,
        // but that's most of them...
        var ability = BlueprintTool.Get<BlueprintAbility>(Guids.FeintAbility);
        foreach (var check in ability.FlattenAllActions().OfType<ContextActionSkillCheck>())
        {
            check.CheckForCaster = true;
        }
    }
}
