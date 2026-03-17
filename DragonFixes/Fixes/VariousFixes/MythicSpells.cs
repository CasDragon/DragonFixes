using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.VariousFixes
{
    internal class MythicSpells
    {
        [DragonConfigure]
        public static void PatchEffectOn()
        {
            Main.log.Log("Patching mythic aoes to mark as harmfulonally");
            AbilityConfigurator.For(AbilityRefs.TricksterRainOfHalberds)
                .SetEffectOnAlly(AbilityEffectOnUnit.Harmful)
                .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                .Configure();
            AbilityConfigurator.For(AbilityRefs.TricksterHallucinogenicCloud)
                .SetEffectOnAlly(AbilityEffectOnUnit.Harmful)
                .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                .Configure();
            AbilityConfigurator.For(AbilityRefs.AbyssalStorm)
                .SetEffectOnAlly(AbilityEffectOnUnit.Harmful)
                .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                .Configure();
            AbilityConfigurator.For(AbilityRefs.Supernova)
                .SetEffectOnAlly(AbilityEffectOnUnit.Harmful)
                .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                .Configure();
        }
    }
}
