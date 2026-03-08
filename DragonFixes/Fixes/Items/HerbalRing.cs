using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Items
{
    internal class HerbalRing
    {
        [DragonConfigure]
        public static void RestoreEnchant()
        {
            Main.log.Log("Patching HerbalRingItem to have the correct enchant.");
            ItemEquipmentRingConfigurator.For(ItemEquipmentRingRefs.HerbalRingItem)
                .SetEnchantments(EquipmentEnchantmentRefs.HerbalRingEnchantment.Reference.Get())
                .Configure();
        }
    }
}
