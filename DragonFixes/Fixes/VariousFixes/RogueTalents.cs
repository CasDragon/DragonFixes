using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.VariousFixes
{
    internal class RogueTalents
    {
        [DragonConfigure]
        public static void PatchWeakeningWoundBuff()
        {
            // Whiterock
            Main.log.Log("Patching WeakeningWoundBuff to include more archetypes.");
            BuffConfigurator.For(BuffRefs.WeakeningWoundBuff)
                .EditComponent<ContextRankConfig>(c => dothingy1(c))
                .Configure();
        }
        public static void dothingy1(ContextRankConfig config)
        {
            config.m_Class = [.. config.m_Class, CharacterClassRefs.SkaldClass.Reference.Get().ToReference<BlueprintCharacterClassReference>(), 
                CharacterClassRefs.ShifterClass.Reference.Get().ToReference<BlueprintCharacterClassReference>(),
                CharacterClassRefs.InquisitorClass.Reference.Get().ToReference<BlueprintCharacterClassReference>()];
            config.m_AdditionalArchetypes = [.. config.m_AdditionalArchetypes,
                ArchetypeRefs.ProvocateurArchetype.Reference.Get().ToReference<BlueprintArchetypeReference>(),
                ArchetypeRefs.FeyformShifterShifterArchetype.Reference.Get().ToReference<BlueprintArchetypeReference>(),
                ArchetypeRefs.SanctifiedSlayerArchetype.Reference.Get().ToReference<BlueprintArchetypeReference>()];
        }
    }
}
