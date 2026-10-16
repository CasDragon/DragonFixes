using System.Linq;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;

namespace DragonFixes.Fixes.Spells;

public class BatteringBlast
{
    [DragonConfigure]
    public static void FixMaxProjectiles()
    {
        Main.log.Log("Fixing Battering Blast max projectiles");
        var projectiles =
            AbilityRefs.BatteringBlast.Reference.Get().Components.
                    First(c => c is AbilityDeliverProjectile) as
                AbilityDeliverProjectile;
        var projectile = ProjectileRefs.BatteringBlast00.Reference.Get().ToReference<BlueprintProjectileReference>();
        projectiles!.m_Projectiles = [.. projectiles.m_Projectiles, projectile, projectile, projectile, projectile,
            projectile, projectile, projectile, projectile, projectile, projectile, projectile, projectile];
    }
}