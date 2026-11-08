using System;
using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.ModRefs;
using DragonLibrary.Utils;

namespace DragonFixes.Fixes.Modded;

public class PetsCantTakeThis
{
    [DragonConfigure]
    public static void DisallowPets()
    {
        var pet = CharacterClassRefs.AnimalCompanionClass.Reference.Get();
        if (ModCompat.IsModEnabled("Swashbuckler"))
        {
            try
            {
                CharacterClassConfigurator.For(SwashbucklerRefs.Swashbuckler)
                    .AddPrerequisiteNoClassLevel(pet)
                    .AddPrerequisiteIsPet(not: true)
                    .Configure();
            }
            catch (Exception e)
            {
                Main.log.Log("Error patching Swashbuckler class");
                Main.log.Log(e.ToString());
            }
        }
        if (ModCompat.IsModEnabled("PsychicWarrior"))
        {
            try
            {
                CharacterClassConfigurator.For(PsychicWarriorRefs.SoulKnifeClass)
                    .AddPrerequisiteNoClassLevel(pet)
                    .AddPrerequisiteIsPet(not: true)
                    .Configure();
            }
            catch (Exception e)
            {
                Main.log.Log("Error patching Soulknife class");
                Main.log.Log(e.ToString());
            }
            try
            {
                CharacterClassConfigurator.For(PsychicWarriorRefs.PsychicWarriorClass)
                    .AddPrerequisiteNoClassLevel(pet)
                    .AddPrerequisiteIsPet(not: true)
                    .Configure();
            }
            catch (Exception e)
            {
                Main.log.Log("Error patching Psychic Warrior class");
                Main.log.Log(e.ToString());
            }
        }
    }
}