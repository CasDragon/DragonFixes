using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Designers.Mechanics.Facts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.NewComponents;

namespace DragonFixes.Fixes.Whiterock
{
    internal class Shadowblood
    {
        [DragonConfigure]
        public static void blah()
        {
            Main.log.Log("Patching Shadowbloodbuff because Whiterock told me to");
            BuffConfigurator.For(BuffRefs.ShadowbloodBuff)
                .EditComponent<SavingThrowBonusAgainstDescriptor>(c => c.OnlyPositiveValue = false)
                .Configure();
        }
    }
}
