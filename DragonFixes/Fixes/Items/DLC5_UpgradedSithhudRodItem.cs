using System.Linq;
using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.Items;

public class DLC5_UpgradedSithhudRodItem
{
    [DragonConfigure]
    public static void SlowNotProne()
    {
        Main.log.Log("DLC5_UpgradedSithhudRodItem now Slows instead of Prone/CantAct");
        var slowBuffRef = BuffRefs.SlowBuff.Reference.Get().ToReference<BlueprintBuffReference>();
        var proneGuid = BuffRefs.Prone.ToString();
        var cantAct = BuffRefs.CantAct.ToString();
        
        var areaEffect = AbilityAreaEffectRefs.DLC5_UpgradeSithhudRodAreaEffect.Reference.Get();
        var areaPrones = areaEffect.m_AllElements.Where(e => e is ContextActionApplyBuff c
                                                             && c.Buff.AssetGuidThreadSafe == proneGuid);
        foreach (var element in areaPrones)
        {
            var areaProne = (ContextActionApplyBuff)element;
            areaProne.m_Buff = slowBuffRef;
        }

        var buff = BuffRefs.DLC5_UpgradedSithhudRodEnemyBuff.Reference.Get();
        var buffPrones = buff.m_AllElements.Where(e => e is ContextActionApplyBuff c
                                                             && c.Buff.AssetGuidThreadSafe == proneGuid);
        foreach (var element in buffPrones)
        {
            var buffProne = (ContextActionApplyBuff)element;
            buffProne.m_Buff = slowBuffRef;
        }
        
        var buffAddFact = buff.Components.FirstOrDefault(c => c is AddFactContextActions) as AddFactContextActions;
        var y = buffAddFact?.Activated.Actions.FirstOrDefault(e =>
            e is ContextActionApplyBuff c && c.Buff.AssetGuidThreadSafe == cantAct);
        var x = buffAddFact?.Activated.Actions?.Where(a => a != y);
        if (x != null)
            buffAddFact?.Activated.Actions = [.. x.ToList()];
        var z = buffAddFact?.Deactivated.Actions.FirstOrDefault(e =>
            e is ContextActionApplyBuff c && c.Buff.AssetGuidThreadSafe == cantAct);
        var v = buffAddFact?.Deactivated.Actions?.Where(a => a != z);
        if (v != null)
            buffAddFact?.Activated.Actions = [.. v.ToList()];
    }

    [DragonConfigure]
    public static void FixItem()
    {
        Main.log.Log("Fix DLC5_UpgradedSithhudRodItem to consume charges");
        ItemEquipmentUsableConfigurator.For(ItemEquipmentUsableRefs.DLC5_UpgradedSithhudRodItem)
            .SetRestoreChargesOnRest(true)
            .Configure();
        BuffConfigurator.For(BuffRefs.DLC5_UpgradedSithhudRodBuff)
            .EditComponent<MetamagicRodMechanics>(c => 
                c.m_RodAbility = ActivatableAbilityRefs.DLC5_UpgradedSithhudRodActivatableAbility.Reference.Get()
                    .ToReference<BlueprintActivatableAbilityReference>())
            .Configure();
    }
}