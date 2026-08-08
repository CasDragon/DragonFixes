using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonFixes.Fixes.Spells;

public class ElementalBodySpells
{
    
    [DragonConfigure]
    public static void PatchBodyBuffs()
    {
        Main.log.Log("Patching FieryBodyBuff, IceBodyBuff, IronBodyBuff to include ImprovedUnarmedStrike");
        BuffConfigurator.For(BuffRefs.FieryBodyBuff)
            .AddMechanicsFeature(AddMechanicsFeature.MechanicsFeatureType.ImprovedUnarmedStrike)
            .Configure();
        BuffConfigurator.For(BuffRefs.IceBodyBuff)
            .AddMechanicsFeature(AddMechanicsFeature.MechanicsFeatureType.ImprovedUnarmedStrike)
            .Configure();
        BuffConfigurator.For(BuffRefs.IronBodyBuff)
            .AddMechanicsFeature(AddMechanicsFeature.MechanicsFeatureType.ImprovedUnarmedStrike)
            .Configure();
    }
}