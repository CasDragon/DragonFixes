using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.Items;

public class CursedItems
{
    [DragonConfigure]
    public static void PatchCursedArmor()
    {
        Main.log.Log("Patching cursed armor");
        BuffConfigurator.For(BuffRefs.CursedDelameresArmorBuff)
            .EditComponent<ContextSetAbilityParams>(c => c.CasterLevel.Value = 7)
            .Configure();
        BuffConfigurator.For(BuffRefs.CursedDelameresBowCurse)
            .EditComponent<ContextSetAbilityParams>(c => c.CasterLevel.Value = 7)
            .Configure();
        BuffConfigurator.For(BuffRefs.MaskOfNothingBuff)
            .EditComponent<ContextSetAbilityParams>(c => c.CasterLevel.Value = 10)
            .Configure();
        BuffConfigurator.For(BuffRefs.StorytellerAreshkaMaskBuff)
            .EditComponent<ContextSetAbilityParams>(c => c.CasterLevel.Value = 10)
            .Configure();
        BuffConfigurator.For(BuffRefs.TheTyranyOfMindCurseBuff)
            .EditComponent<ContextSetAbilityParams>(c => c.CasterLevel.Value = 4)
            .Configure();
        BuffConfigurator.For(BuffRefs.WickedKukriBuff)
            .EditComponent<ContextSetAbilityParams>(c => c.CasterLevel.Value = 9)
            .Configure();
    }
}