using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Classes
{
    internal class Slayer
    {
        [DragonConfigure]
        public static void PatchBiteofthevampireffect()
        {
            Main.log.Log("Patching Biteofthevampireffect to work on Sneak Attacks");
            FeatureConfigurator.For(FeatureRefs.Biteofthevampireffect)
                .EditComponent<AddInitiatorAttackWithWeaponTrigger>(c => c.NotSneakAttack = false)
                .Configure();
        }
    }
}
