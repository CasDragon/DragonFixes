using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Whiterock
{
    internal class EnchantKeyFixes
    {
        [DragonConfigure]
        public static void PatchUnholyEnchant()
        {
            Main.log.Log("Patching Unholy enchant to add prefix.");
            WeaponEnchantmentConfigurator.For(WeaponEnchantmentRefs.Unholy)
                .SetPrefix("32d9aeca-c1c8-425d-975a-5347a8f57038")
                .Configure();
        }
        [DragonConfigure]
        public static void PatchVorpalEnchant()
        {
            Main.log.Log("Patching Vorpal enchant to add prefix.");
            WeaponEnchantmentConfigurator.For(WeaponEnchantmentRefs.Vorpal)
                .SetPrefix("599d9e98-9c91-464d-b206-a39c024040eb")
                .Configure();
        }
    }
}
