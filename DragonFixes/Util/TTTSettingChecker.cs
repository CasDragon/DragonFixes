using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Util
{
    internal class TTTSettingChecker
    {
        public static bool CheckSpellsFixes(string key)
        {
            return TabletopTweaks.Base.Main.TTTContext.Fixes.Spells.IsEnabled(key);
        }
    }
}
