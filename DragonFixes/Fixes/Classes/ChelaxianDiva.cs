using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Classes
{
    internal class ChelaxianDiva
    {
        [DragonConfigure]
        public static void PatchTragedyOfFalseHopeArea()
        {
            Main.log.Log("Patching TragedyOfFalseHopeArea to include Mind Affecting descriptor");
            AbilityAreaEffectConfigurator.For(AbilityAreaEffectRefs.TragedyOfFalseHopeArea)
                .AddSpellDescriptorComponent(descriptor: SpellDescriptor.MindAffecting)
                .Configure();
        }
    }
}
