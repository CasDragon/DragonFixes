using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Classes
{
    internal class WinterWitch
    {
        [DragonConfigure]
        public static void PatchHexProgression()
        {
            Main.log.Log("Patching the Winter Witch hex selections to actually show up");
            ProgressionConfigurator.For(ProgressionRefs.WinterWitchShamanHexProgression)
                .SetHideInUI(false)
                .SetHideNotAvailibleInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .Configure();
            ProgressionConfigurator.For(ProgressionRefs.WinterWitchWitchHexProgression)
                .SetHideInUI(false)
                .SetHideNotAvailibleInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .Configure();
        }
    }
}
