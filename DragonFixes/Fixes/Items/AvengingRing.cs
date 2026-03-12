using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Items
{
    internal class AvengingRing
    {
        [DragonConfigure]
        public static void PatchRing()
        {
            Main.log.Log("Patching AvengingRingFeature to work on any AoO instead of AoO hits.");
            FeatureConfigurator.For(FeatureRefs.AvengingRingFeature)
                .EditComponent<AddTargetAttackWithWeaponTrigger>(c => c.OnlyHit = false)
                .Configure();
        }
    }
}
