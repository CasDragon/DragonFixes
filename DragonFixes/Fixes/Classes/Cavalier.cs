using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;

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

        [DragonConfigure]
        public static void PatchAbsoluteOrder()
        {
            Main.log.Log("Patching AbsoluteOrder to allow more targets.");
            BlueprintAbility approach = AbilityRefs.AbsoluteOrderApproach.Reference.Get();
            DragonHelpers.RemoveComponent<AbilityTargetHasFact>(approach);
            BlueprintAbility fall = AbilityRefs.AbsoluteOrderFall.Reference.Get();
            DragonHelpers.RemoveComponent<AbilityTargetHasFact>(fall);
            BlueprintAbility flee = AbilityRefs.AbsoluteOrderFlee.Reference.Get();
            DragonHelpers.RemoveComponent<AbilityTargetHasFact>(flee);
            BlueprintAbility halt = AbilityRefs.AbsoluteOrderHalt.Reference.Get();
            DragonHelpers.RemoveComponent<AbilityTargetHasFact>(halt);
        }
    }
}
