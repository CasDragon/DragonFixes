using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Patches
{
    [HarmonyPatch]
    internal class EnduringBladeMountFix
    {
        // Known blueprint GUIDs
        private const string EnduringBladeBuffGuid = "3c2fe8e0374d28d4185355121f4c4544";

        // Tracks ClearEnchantPool calls originating from ContextActionShieldWeaponEnchantPool.
        [ThreadStatic]
        private static bool InShieldWeaponEnchantPool;

        [HarmonyPatch(typeof(ContextActionShieldWeaponEnchantPool), nameof(ContextActionShieldWeaponEnchantPool.RunAction))]
        [HarmonyPrefix]
        public static void ShieldWeaponEnchantPool_Prefix()
        {
            InShieldWeaponEnchantPool = true;
        }

        [HarmonyPatch(typeof(ContextActionShieldWeaponEnchantPool), nameof(ContextActionShieldWeaponEnchantPool.RunAction))]
        [HarmonyFinalizer]
        public static void ShieldWeaponEnchantPool_Finalizer(Exception __exception)
        {
            InShieldWeaponEnchantPool = false;
        }

        /// <summary>
        /// Prevent ContextActionShieldWeaponEnchantPool from clearing the rider's weapon enchantments
        /// when enchanting the mount's weapon. This is the core fix for the Enduring Blade + Mounted bug.
        /// </summary>
        [HarmonyPatch(typeof(UnitPartEnchantPoolData), nameof(UnitPartEnchantPoolData.ClearEnchantPool))]
        [HarmonyPrefix]
        public static bool ClearEnchantPool_Prefix(UnitPartEnchantPoolData __instance, EnchantPoolType type)
        {
            try
            {
                if (type != EnchantPoolType.ArcanePool || !InShieldWeaponEnchantPool)
                {
                    return true; // Allow clearing for other pool types or callers
                }

                var unit = __instance.Owner;
                var riderPart = unit?.Get<UnitPartRider>();
                var enduringBladeBuff = BlueprintTool.Get<BlueprintBuff>(EnduringBladeBuffGuid);

                // If mounted with Enduring Blade active, prevent clearing the rider's pool
                if (riderPart != null && riderPart.SaddledUnit != null &&
                    enduringBladeBuff != null && unit.Descriptor.Buffs.HasFact(enduringBladeBuff))
                {
                    return false; // Skip the clear to preserve rider weapon enchantments
                }

                return true; // Allow normal clearing
            }
            catch (Exception e)
            {
                Main.log.Log("Error in  EduringBladesMountFix patch");
                Main.log.Log(e.ToString());
                return true; // On error, allow normal behavior
            }
        }
    }
}
