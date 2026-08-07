using System;
using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.References;
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
                CharacterClassConfigurator.For("338ABF27-23C1-4C1A-B0F1-7CD7E3020444")
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
                CharacterClassConfigurator.For("7c4a2f91-e3b8-4d65-a017-58c91b2e3f40")
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
                CharacterClassConfigurator.For("4c8f9c4e-7f22-4d9b-a9b9-2f7a9f7e1d01")
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