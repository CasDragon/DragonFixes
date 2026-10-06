using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Items.Weapons;

namespace DragonFixes.Fixes.Classes
{
    internal class Druid
    {
        // Rampager
        [DragonConfigure]
        public static void PatchChainDischarge()
        {
            Main.log.Log("Patching RampageExtraEffect11 to correctly use the ability that it says it will.");
            FeatureConfigurator.For("017ed1a0d47a422eab183a88084966b1")
                .AddInitiatorAttackWithWeaponTrigger(group: WeaponFighterGroup.Natural, checkWeaponGroup: true,
                        onlyHit: true, action: 
                        ActionsBuilder.New().CastSpell(AbilityRefs.RampageChainDischarge.Reference.Get()))
                .Configure();
        }
        
        // Blight Druid

        [DragonConfigure]
        public static void PatchBlightBond()
        {
            Main.log.Log("Patching Blight Bond to include Ice domain");
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.BlightDruidBond)
                .AddToAllFeatures(ProgressionRefs.IceSubdomainProgressionDruid.ToString())
                .Configure();
        }
    }
}
