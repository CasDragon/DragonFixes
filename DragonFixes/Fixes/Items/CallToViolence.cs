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
        public static void PatchRages()
        {
            Main.log.Log("Patching  to include more rage effects.");
            var buff = BuffRefs.CallToViolenceEffectBuff.ToString();
            BuffConfigurator.For(BuffRefs.CallToViolenceBuff)
                .AddBuffExtraEffects(
                    checkedBuff: BuffRefs.InspiredRageEffectBuffMythic.ToString(),
                    extraEffectBuff: buff
                )
                .AddBuffExtraEffects(
                    checkedBuff: BuffRefs.RageBuff.ToString(),
                    extraEffectBuff: buff
                )
                .AddBuffExtraEffects(
                    checkedBuff: BuffRefs.InciteRageEffectBuff.ToString(),
                    extraEffectBuff: buff
                )
                .AddBuffExtraEffects(
                    checkedBuff: BuffRefs.ElementalRampagerRampageBuff.ToString(),
                    extraEffectBuff: buff
                )
                .AddBuffExtraEffects(
                    checkedBuff: BuffRefs.RageSpellBuff.ToString(),
                    extraEffectBuff: buff
                )
                .AddBuffExtraEffects(
                    checkedBuff: BuffRefs.Gorum_Buff.ToString(),
                    extraEffectBuff: buff
                )
                .Configure();
        }
    }
}
