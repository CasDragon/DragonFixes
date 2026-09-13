using BlueprintCore.Blueprints.CustomConfigurators;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace DragonFixes.Util;

public static class FixHelpers
{
    public static void SetCustomDC(ContextActionSavingThrow savingThrow, int dc)
    {
        savingThrow.HasCustomDC = true;
        savingThrow.CustomDC.Value = dc;
    }

}