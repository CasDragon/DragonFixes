using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.Items;

public class FreeRein
{
    [DragonConfigure]
    public static void PatchFreeRein()
    {
        if (ModCompat.tttbase && TTTSettingChecker.CheckSpellsFixes("FreedomOfMovement"))
        {
            Main.log.Log("TTT installed and stagger setting is enabled, disabling Free Rein fix");
            return;
        }
        Main.log.Log("Patching Free Rein and Freest Rein");
        BlueprintBuff buff = BuffConfigurator.For(BuffRefs.BootsOfFreereinBuff)
            .AddBuffDescriptorImmunity(false, SpellDescriptor.Staggered)
            .Configure();
        BlueprintFeature bp = FeatureRefs.BootsOfFreestReinFeature.Reference.Get();
        DragonHelpers.RemoveComponent(bp, bp.GetComponent<AddFactContextActions>());
        FeatureConfigurator.For(bp)
            .AddFactContextActions(activated: ActionsBuilder.New()
                    .ApplyBuffPermanent(buff, true),
                deactivated: ActionsBuilder.New()
                    .RemoveBuff(buff))
            .Configure();
    }
}