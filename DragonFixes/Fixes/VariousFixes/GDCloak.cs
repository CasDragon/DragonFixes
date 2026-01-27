using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.VariousFixes
{
    internal class GDCloak
    {
        [DragonConfigure]
        public static void PatchGDCloak()
        {
            Main.log.Log("Stop GD from stealing your cloak!");
            DragonHelpers.RemoveComponent<AddFactContextActions>(FeatureRefs.DragonLevel1Immunities.Reference.Get());
        }
    }
}
