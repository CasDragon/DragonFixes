using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;

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
