using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;

namespace DragonFixes.Fixes.Spells;

public static class SpellSpecialization
{
    [DragonConfigure]
    public static void IncludeDLCSpells()
    {
        // List stolen from Yogurt
        if (ModCompat.yogurt) return;
        Main.log.Log("Including DLC Spells in Spell Specialization");
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.AspectOfTheStag);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.AnimalAspectGreaterBase);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.IncendiaryRunes);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.HauntingMists);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.GhoulTouchCast);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.KeenEdge);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ExplosiveRunes);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ExcruciatingDeformation);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.CurseOfMagicNegation);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.Repulsion);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ArchonsTrumpet);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.SymbolOfWeakness);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.SymbolOfVulnerability);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.MeteorSwarm);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.RevivingFinale);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.PurgingFinale);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.SongOfDiscordGreater);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.IllOmen);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.FungalInfestationAbility);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.OracleBurden);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.Implosion);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ArcanaTheft);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.DispelMagicTarget);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.DispelMagicGreaterTarget);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.PeerThroughAvoidance);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.PeerThroughEndurance);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.PeerThroughMind);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.PeerThroughReflexes);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.PeerThroughResistance);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.PeerThroughSpellResistance);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.DragonSmite);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.SummonDragonI);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.SummonDragonII);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.SummonDragonIII);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.DragonMight);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.DragonDemand);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.DragonPride);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.DragonWrath);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ThousandBites);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.DragonUltimateApsu);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.DragonUltimateDahak);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.BoneSpearConjure);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.DeathRealm);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.LivingPlagueAbility);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.FormOfTheDragonIRed);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ProtectionFromAcid);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.SummonElementalElderEarth);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ProtectionFromCold);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ElementalBodyIIIWater);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ProtectionFromGood);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.BurningHandsAcid);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ScorchingRayAcid);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.BurningHandsCold);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ScorchingRayCold);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.BurningHandsElecricity);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ScorchingRayElecricity);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.SummonNaturesAllyVIIISingle);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.SummonNaturesAllyIXSingle);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ResistFire);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ProtectionFromFire);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ElementalBodyIEarth);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ElementalBodyIWater);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ElementalBodyIAir);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.ElementalBodyIFire);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.SummonElementalSmallFire);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.SummonElementalMediumFire);
        SpellHelpers.AddToSpellSpecialization(AbilityRefs.SummonElementalLargeFire);
    }
}