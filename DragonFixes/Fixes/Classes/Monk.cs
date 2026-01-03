using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Classes
{
    internal class Monk
    {
        [DragonConfigure]
        public static void PatchScaledFist()
        {
            Main.log.Log("Patching ScaledFistACBonusBuff/ScaledFistACBonusUnarmoredBuff to be class features.");
            BuffConfigurator.For(BuffRefs.ScaledFistACBonusBuff)
                .SetIsClassFeature(true)
                .Configure();
            BuffConfigurator.For(BuffRefs.ScaledFistACBonusUnarmoredBuff)
                .SetIsClassFeature(true)
                .Configure();
        }
    }
}
