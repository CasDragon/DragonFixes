using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace DragonFixes.Fixes
{
    internal class BloodlineFixes
    {
        [DragonConfigure]
        public static void PatchEarthElementalBurrowBuff()
        {
            Main.log.Log("Patching BloodlineElementalEarthElementalMovementBurrowBuff to have a multiplier of 1 instead of 0.");
            BuffConfigurator.For(BuffRefs.BloodlineElementalEarthElementalMovementBurrowBuff)
                .EditComponent<AddContextStatBonus>(c => c.Multiplier = 1)
                .Configure();
        }

        [DragonConfigure]
        public static void PatchDescriptorOnElementalBloodlines()
        {
            Main.log.Log("Patching elemental bloodlines to remove elemental descriptor, no idea if this breaks other things");
            Blueprint<BlueprintReference<BlueprintBuff>>[] x = 
            [
                BuffRefs.BloodlineElementalAirArcanaBuff,
                BuffRefs.BloodlineElementalFireArcanaBuff
            ];
            foreach (var i in x)
            {
                try
                {
                    DragonHelpers.RemoveComponent<SpellDescriptorComponent>(i.Reference.Get());
                }
                catch
                {
                    Main.log.Log($"No SpellDescriptorComponent found on {i.Reference.NameSafe()}");
                }
            }
        }
    }
}
