using Kingmaker.UnitLogic.Mechanics.Actions;

namespace DragonFixes.Util;

public class FixHelpers
{
    public static void SetCustomDC(ContextActionSavingThrow savingThrow, int dc)
    {
        savingThrow.HasCustomDC = true;
        savingThrow.CustomDC.Value = dc;
    }
}