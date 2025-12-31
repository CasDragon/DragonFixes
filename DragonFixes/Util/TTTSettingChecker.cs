using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Util
{
    internal class TTTSettingChecker
    {
        public static bool CheckStaggerRemoval()
        {
            return TabletopTweaks.Base.Main.TTTContext.Fixes.BaseFixes.IsEnabled("StaggeredDescriptors");
        }
    }
}
