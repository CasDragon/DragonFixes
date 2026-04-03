using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;

namespace DragonFixes.Fixes.Classes
{
    internal class GoldDragon
    {
        [DragonConfigure]
        public static void PatchDragonFormAbillity()
        {
            Main.log.Log("Patching DragonFormAbillity to not be castable if you are in corrupt gd form.");
            BlueprintAbility ability = AbilityRefs.DragonFormAbillity.Reference.Get();
            AbilityTargetHasFact component = ability.GetComponent<AbilityTargetHasFact>();
            component.m_CheckedFacts = [.. component.m_CheckedFacts, BuffRefs.CorruptedDragonFormBuff.Reference.Get().ToReference<BlueprintUnitFactReference>()];
        }
        [DragonConfigure]
        public static void PatchThousandBitesBuff()
        {
            Main.log.Log("Patching ThousandBitesBuff to correctly buff CorruptedDragonForm");
            BuffConfigurator.For(BuffRefs.ThousandBitesBuff)
                .AddBuffExtraEffects(checkedBuff: BuffRefs.CorruptedDragonFormBuff.Reference.Get(),
                        extraEffectBuff: BuffRefs.ThousandBitesBuffEffect.Reference.Get())
                .Configure();
        }
    }
}
