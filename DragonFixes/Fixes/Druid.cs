using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonFixes.Util;

namespace DragonFixes.Fixes
{
    internal class Druid
    {
        [DragonFix]
        public static void PatchChainDischarge()
        {
            Main.log.Log("Patching 017ed1a0d47a422eab183a88084966b1 to correctly use the ability that it says it will.");
            FeatureConfigurator.For("017ed1a0d47a422eab183a88084966b1")
                .AddInitiatorAttackWithWeaponTrigger(group: Kingmaker.Blueprints.Items.Weapons.WeaponFighterGroup.Natural, checkWeaponGroup: true,
                        onlyHit: true, action: 
                        ActionsBuilder.New().CastSpell(AbilityRefs.RampageChainDischarge.Reference.Get()))
                .Configure();
        }
    }
}
