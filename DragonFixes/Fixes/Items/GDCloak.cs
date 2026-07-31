using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.Items
{
    internal class GDCloak
    {
        [DragonConfigure]
        public static void PatchGDCloak()
        {
            Main.log.Log("Stop GD from stealing your cloak!");
            DragonHelpers.RemoveComponent<AddFactContextActions>(FeatureRefs.DragonLevel1Immunities.Reference.Get());
        }
    }
}
