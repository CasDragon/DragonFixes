using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonFixes.Util;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes
{
    internal class ShamanSpirits
    {
        [DragonConfigure]
        public static void PatchFireSpirit()
        {
            Main.log.Log("Patching Shaman Flame spirit capstone");
            BuffConfigurator.For(BuffRefs.ShamanFlameSpiritManifestationBolsterBuff)
                .EditComponent<AutoMetamagic>(c => c.Descriptor = SpellDescriptor.Fire)
                .Configure();
            BuffConfigurator.For(BuffRefs.ShamanFlameSpiritManifestationReachBuff)
                .EditComponent<AutoMetamagic>(c => c.Descriptor = SpellDescriptor.Fire)
                .Configure();
            BuffConfigurator.For(BuffRefs.ShamanFlameSpiritManifestationExtendBuff)
                .EditComponent<AutoMetamagic>(c => c.Descriptor = SpellDescriptor.Fire)
                .Configure();
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.ShamanFlameSpiritManifestationExtend)
                .SetBuff(BuffRefs.ShamanFlameSpiritManifestationExtendBuff.Reference.Get())
                .Configure();
        }
        [DragonConfigure]
        public static void PatchWindSpirit()
        {
            Main.log.Log("Patching Shaman Wind spirit capstone");
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.ShamanWindSpiritManifestationExtend)
                .SetBuff(BuffRefs.ShamanWindSpiritManifestationExtendBuff.Reference.Get())
                .Configure();
        }
        [DragonConfigure]
        public static void PatchFrostSpirit()
        {
            Main.log.Log("Patching Shaman Frost spirit capstone");
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.ShamanFrostSpiritManifestationExtend)
                .SetBuff(BuffRefs.ShamanFrostSpiritManifestationExtendBuff.Reference.Get())
                .Configure();
        }
        [DragonConfigure]
        public static void PatchWavesSpirit()
        {
            Main.log.Log("Patching Shaman Waves spirit capstone");
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.ShamanWavesSpiritManifestationExtend)
                .SetBuff(BuffRefs.ShamanWavesSpiritManifestationExtendBuff.Reference.Get())
                .Configure();
        }
        [DragonConfigure]
        public static void PatchStonesSpirit()
        {
            Main.log.Log("Patching Shaman Stones spirit capstone");
            BuffConfigurator.For(BuffRefs.ShamanStonesSpiritManifestationBolsterBuff)
                .EditComponent<AutoMetamagic>(c => c.Descriptor = SpellDescriptor.Acid)
                .Configure();
            BuffConfigurator.For(BuffRefs.ShamanStonesSpiritManifestationReachBuff)
                .EditComponent<AutoMetamagic>(c => c.Descriptor = SpellDescriptor.Acid)
                .Configure();
            BuffConfigurator.For(BuffRefs.ShamanStonesSpiritManifestationExtendBuff)
                .EditComponent<AutoMetamagic>(c => c.Descriptor = SpellDescriptor.Acid)
                .Configure();
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.ShamanStonesSpiritManifestationExtend)
                .SetBuff(BuffRefs.ShamanStonesSpiritManifestationExtendBuff.Reference.Get())
                .Configure();
        }
    }
}
