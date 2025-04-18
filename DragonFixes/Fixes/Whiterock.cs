using BlueprintCore.Blueprints.Configurators.DialogSystem;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using DragonFixes.Util;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes
{
    internal class Whiterock
    {
        [DragonFix]
        public static void PatchStudyTarget()
        {
            Main.log.Log("Patching SlayerStudyTargetBuff to correctly use AND logic");
            BuffConfigurator.For(BuffRefs.SlayerStudyTargetBuff)
                .EditComponent<AddFactContextActions>(c => c.Activated.Actions
                            .OfType<Conditional>()
                            .Where(x => x.ConditionsChecker.Conditions
                                    .OfType<ContextConditionCasterHasFact>()
                                    .First()
                                    .m_Fact.deserializedGuid == FeatureRefs.ExecutionerFocusedKiller.Reference.deserializedGuid)
                            .First()
                            .IfTrue.Actions
                            .OfType<Conditional>()
                            .First()
                            .ConditionsChecker.Operation = Operation.And
                            )
                .Configure();
        }
        [DragonFix]
        public static void PatchBaneLivingEnchant()
        {
            Main.log.Log("Patching BaneLiving to correctly use AND logic");
            WeaponEnchantmentConfigurator.For(WeaponEnchantmentRefs.BaneLiving)
                .EditComponent<WeaponConditionalEnhancementBonus>(c => c.Conditions.Operation = Operation.And) 
                .EditComponent<WeaponConditionalDamageDice>(c => c.Conditions.Operation = Operation.And)
                .Configure();
        }
        [DragonFix]
        public static void PatchCue()
        {
            Main.log.Log("Patching Cue_0022 to have Jernaugh correctly recognise PC race");
            CueConfigurator.For("e1c7bbcf26b658244939a8e4ca807556")
                .ModifyConditions(c => c.Operation = Operation.Or)
                .Configure();
        }
    }
}
