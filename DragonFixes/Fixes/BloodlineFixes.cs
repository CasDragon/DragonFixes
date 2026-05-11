using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes
{
    internal class BloodlineFixes
    {
        [DragonConfigure]
        public static void PatchEarthElementalBurrowBuff()
        {
            Main.log.Log("Patching BloodlineElementalEarthElementalMovementBurrowBuff to have a multiplier of 1 instead of 0.");
            BuffConfigurator.For(BuffRefs.BloodlineElementalEarthElementalMovementBurrowBuff)
                .EditComponent<AddContextStatBonus>(c => c.Multiplier = 1)
                .Configure();
        }
    }
}
