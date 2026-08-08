using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;

namespace DragonFixes.Fixes.Items;

public class NeophyteGloves
{
        [DragonConfigure]
        public static void PatchNeophyteGloves()
        {
            Main.log.Log("Patching the Gloves of the Neophyte to add the missing spells");
            BlueprintFeature bp = FeatureRefs.GlovesOfNeophyteFeature.Reference.Get();
            DragonHelpers.RemoveComponent(bp, bp.GetComponent<DiceDamageBonusOnSpell>());
            FeatureConfigurator.For(bp)
                .AddDiceDamageBonusOnSpell(spells: [
                    AbilityRefs.ShockingGraspEffect.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.IncendiaryRunes.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.AcidSplash.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.Jolt.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.RayOfFrost.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.BurningHands.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.CorrosiveTouch.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.CureLightWoundsDamage.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.EarPiercingScream.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.FirebellyAbility.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.MagicMissile.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.Snowball.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.DivineZap.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.Ignition.Reference.Get().ToReference<BlueprintAbilityReference>(),
                    AbilityRefs.InflictLightWoundsDamage.Reference.Get().ToReference<BlueprintAbilityReference>()
                    ], 
                    mergeBehavior: BlueprintCore.Blueprints.CustomConfigurators.ComponentMerge.Replace)
                .Configure();
        }
}