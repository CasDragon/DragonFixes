using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonFixes.Util;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace DragonFixes.Fixes
{
    internal class MetamagicRods
    {
        [DragonConfigure]
        public static void PatchPiercingRods()
        {
            Main.log.Log("Patching Piercing metamagic rods to actually be piercing instead of intensified.");
            BlueprintBuff normal = BuffRefs.MetamagicRodNormalPiercingBuff.Reference.Get();
            DragonHelpers.RemoveComponent<MetamagicRodMechanics>(normal);
            BuffConfigurator.For(normal)
                .AddMetamagicRodMechanics(metamagic: Kingmaker.UnitLogic.Abilities.Metamagic.Piercing,
                    maxSpellLevel: 6, rodAbility: ActivatableAbilityRefs.MetamagicRodNormalPiercingToggleAbility.Reference.Get())
                .Configure();
            BlueprintBuff lesser = BuffRefs.MetamagicRodLesserPiercingBuff.Reference.Get();
            DragonHelpers.RemoveComponent<MetamagicRodMechanics>(lesser);
            BuffConfigurator.For(lesser)
                .AddMetamagicRodMechanics(metamagic: Kingmaker.UnitLogic.Abilities.Metamagic.Piercing,
                    maxSpellLevel: 3, rodAbility: ActivatableAbilityRefs.MetamagicRodLesserPiercingToggleAbility.Reference.Get())
                .Configure();
            BlueprintBuff greater = BuffRefs.MetamagicRodGreaterPiercingBuff.Reference.Get();
            DragonHelpers.RemoveComponent<MetamagicRodMechanics>(greater);
            BuffConfigurator.For(greater)
                .AddMetamagicRodMechanics(metamagic: Kingmaker.UnitLogic.Abilities.Metamagic.Piercing,
                    maxSpellLevel: 9, rodAbility: ActivatableAbilityRefs.MetamagicRodGreaterPiercingToggleAbility.Reference.Get())
                .Configure();
        }
    }
}
