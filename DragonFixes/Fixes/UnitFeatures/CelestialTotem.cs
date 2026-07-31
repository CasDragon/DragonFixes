using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonFixes.Fixes.UnitFeatures;

public class CelestialTotem
{
    private const string settingname = "lessercelestialtotem";
    private const string settingdescription = "Fix Lesser Celestial Totem from procing on 0 hp heals";
    [DragonConfigure]
    [DragonSetting(SettingCategories.None, settingname, settingdescription)]
    public static void PatchCelestialTotemLesser()
    {
        if (SettingsAction.GetSetting<bool>(settingname))
        {
            Main.log.Log("Patching Celestial Totem Lesser to not heal on 0 hp heals.");
            BuffConfigurator.For(BuffRefs.CelestialTotemLesserBuff)
                .EditComponent<AddHealTrigger>(c => c.AllowZeroHealDamage = false)
                .Configure();
        }
    }
}