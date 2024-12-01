using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityModManagerNet;

namespace DragonFixes.Util
{
    public class Settings : UnityModManager.ModSettings
    {
        public static bool ESArcaneAccuracy = true;
        public static bool ESExtraArcanaSelection = true;
        public static bool ESPrescientDuration = true;
        public static bool OracleApsuRestrictionRemoval = true;
        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Save(this, modEntry);
        }
    }
}
