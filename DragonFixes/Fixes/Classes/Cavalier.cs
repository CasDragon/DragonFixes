using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Classes
{
    internal class Cavalier
    {
        [DragonConfigure]
        public static void PatchDeadlyCharge()
        {
            Main.log.Log("Patching DIscipleOfThePikeDeadlyChargeBuff to include double damage.");
            BuffRefs.DIscipleOfThePikeDeadlyChargeBuff.Reference.Get().Components = BuffRefs.CavalierSupremeChargeBuff.Reference.Get().Components;
        }
    }
}
