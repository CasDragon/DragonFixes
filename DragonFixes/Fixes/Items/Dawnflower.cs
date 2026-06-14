using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Items
{
    internal class Dawnflower
    {
        [DragonConfigure]
        public static void PatchGoodEnhant()
        {
            Main.log.Log("Patching DawnflowersKiss_Good_ScimitarEnchantment to have the buff go for 2 rounds instead of 1");
            WeaponEnchantmentConfigurator.For(WeaponEnchantmentRefs.DawnflowersKiss_Good_ScimitarEnchantment)
                .EditComponent<AddInitiatorAttackWithWeaponTrigger>(c => DoThing(c))
                .Configure();
            WeaponEnchantmentConfigurator.For(WeaponEnchantmentRefs.DawnflowersKiss_Good02_ScimitarEnchantment)
                .EditComponent<AddInitiatorAttackWithWeaponTrigger>(c => DoThing(c))
                .Configure();
        }

        public static void DoThing(AddInitiatorAttackWithWeaponTrigger component)
        {
            ContextActionApplyBuff x = (ContextActionApplyBuff) component.Action.Actions.First();
            x.DurationValue.BonusValue.Value = 2;
        }
    }
}
