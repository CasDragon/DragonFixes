using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonFixes.Util;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonFixes.Fixes.Classes
{
    internal class Arcanist
    {
        [DragonConfigure]
        public static void PatchMDInhibitHeroics()
        {
            Main.log.Log("Patching FakeInspireHeroicsEffectBuff to correctly apply penalty");
            AddContextStatBonus x = BuffRefs.FakeInspireHeroicsEffectBuff.Reference.Get()
                .GetComponents<AddContextStatBonus>()
                    .FirstOrDefault(c => c.Multiplier is 1);
            x.Multiplier = -1;
        }
        [DragonConfigure]
        public static void PatchMDInhibitCourage()
        {
            Main.log.Log("Patching FakeInspireCourageEffectBuff to correctly apply penalty");
            BlueprintBuff x = BuffRefs.FakeInspireCourageEffectBuff.Reference.Get();
            DragonHelpers.RemoveComponent<ContextRankConfig>(x);
            BuffConfigurator.For(x)
                .AddContextRankConfig(ContextRankConfigs.MaxClassLevelWithArchetype(["52dbfd8505e22f84fad8d702611f60b7"], ["5c77110cd0414e7eb4c2e485659c9a46"],
                    max: 20, min: 0).WithDivStepProgression(-5))
                .Configure();
        }
    }
}
