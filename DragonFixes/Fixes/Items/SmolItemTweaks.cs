using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using DragonLibrary.BPCoreExtensions;
using Kingmaker.Blueprints.Items.Equipment;

namespace DragonFixes.Fixes.Items
{
    internal class SmolItemTweaks
    {
        [DragonConfigure]
        public static void PatchSeasonedAssassin()
        {
            Main.log.Log("Patchin SeasonedAssassinFeature to not be hidden in the UI, so when you encounter an enemy with it, you can see it.");
            FeatureConfigurator.For(FeatureRefs.SeasonedAssassinsFeature)
                .SetHideInUI(false)
                .Configure();
        }


        [DragonConfigure]
        public static void PatchBaphometFireGloves_BurningCunningBuff()
        {
            Main.log.Log("Allow ray spells to properly work with BaphometFireGloves.");
            BlueprintBuff x = BuffRefs.BaphometFireGloves_BurningCunningBuff.Reference.Get();
            BlueprintFeature y = FeatureRefs.BaphometFireGloves_BurningCunningFeature.Reference.Get();
            BlueprintItemEquipmentGloves item = ItemEquipmentGlovesRefs.BaphometFireGloves_BurningCunningItem.Reference.Get();
            AddInitiatorAttackWithWeaponTrigger z = y.GetComponent<AddInitiatorAttackWithWeaponTrigger>();
            DragonHelpers.RemoveComponent(y, z);
            FeatureConfigurator.For(y)
                .AddSneakAttackRollTrigger(z.Action)
                .Configure();
            BuffConfigurator.For(x)
                .RemoveFromFlags(BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.RemoveOnRest)
                .SetIcon(BuffRefs.ShamanHexFlameCurseBuff.Reference.Get().Icon)
                .SetDisplayName(item.m_DisplayNameText)
                .SetDescription(item.m_DescriptionText)
                .Configure();
        }
    }
}
