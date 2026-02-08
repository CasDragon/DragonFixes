using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Items
{
    internal class CallToViolence
    {
        [DragonConfigure]
        public static void Patch()
        {
            Main.log.Log("Patching  to include Skald rage.");
            BuffConfigurator.For(BuffRefs.CallToViolenceBuff)
                .AddBuffExtraEffects(
                    checkedBuff: BuffRefs.InspiredRageEffectBuffBeforeMasterSkald.Reference.Get(),
                    extraEffectBuff: BuffRefs.CallToViolenceEffectBuff.Reference.Get()
                    )
                .Configure();
        }
    }
}
