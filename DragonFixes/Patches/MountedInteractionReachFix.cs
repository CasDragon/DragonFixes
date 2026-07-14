using System;
using HarmonyLib;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Commands;

namespace DragonFixes.Patches
{
    /// <summary>
    /// Mounted characters struggle to disarm traps and loot objects. Interaction reach is a small
    /// fixed radius tied to the object and, unlike attacks, never accounts for body size.
    /// Therefore, a large mount can't bring the rider close enough. We pad the radius by the
    /// mount's corpulence when mounted, letting it stand adjacent and still reach
    /// </summary>
    [HarmonyPatch]
    internal class MountedInteractionReachFix
    {
        [HarmonyPatch(typeof(UnitInteractWithObject), nameof(UnitInteractWithObject.Init)), HarmonyPostfix]
        private static void Init_Postfix(UnitInteractWithObject __instance, UnitEntityData executor)
        {
            try
            {
                UnitEntityData mount = executor?.RiderPart?.SaddledUnit;
                if (mount != null)
                    __instance.ApproachRadius += mount.Corpulence;
            }
            catch (Exception e)
            {
                Main.log.Log("MountedInteractionReachFix error: " + e);
            }
        }
    }
}
