using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDamageCalculators;

public class ToxicSmackerelDamageCalculator : DefaultAbilityDamageCalculator
{
    // It's extremely unlikely to hit a pre-poisoned target; therefore, the special DPS has to be recalculated
    // We reduce the DPS by 33.33% to simulate a scenario where we use Toxic Smackerel 3x on a not yet poisoned target

    /// <inheritdoc />
    public ToxicSmackerelDamageCalculator(AbilityServiceProvider abilityServiceProvider) : base(abilityServiceProvider) {}

    /// <inheritdoc />
    public override bool CanHandleAbility(string abilityName)
    {
        return abilityName == "Toxic Smackerel";
    }

    /// <inheritdoc />
    public override DamageRange CalculateSpecialDamageRange(string abilityName, AbilityRarity abilityRarity, int playerLevel)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var damageRange = base.CalculateSpecialDamageRange(abilityName, abilityRarity, playerLevel);
        return damageRange with
        {
            MaxDps = damageRange.MaxDps * 2 / 3
        };
    }

    /// <inheritdoc />
    public override decimal? CalculateTimeToKillBasedOnSimulation(string abilityName, AbilityRarity abilityRarity, int playerLevel, int targetLevel)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var baseHits = CalculateBaseHits(abilityName, abilityRarity, playerLevel);
        var specialHits = CalculateSpecialHits(abilityName, abilityRarity, playerLevel);
        var dotHits = CalculateDotHits(abilityName, abilityRarity, playerLevel);
        var hits = baseHits.Concat(specialHits).Concat(dotHits).OrderBy(x => x.Timing).ToList();

        if (baseHits.Sum(x => x.Damage) + specialHits.Sum(x => x.Damage) + dotHits.Sum(x => x.Damage) <= 0)
        {
            return null;
        }

        // We add the bonus damage to the target's health to simulate a not-yet poisoned target
        var detailsProvider = AbilityServiceProvider.GetAbilityDetailsProvider(abilityName);
        var targetHitPoints = Convert.ToDecimal(PlayerStatsProvider.GetHitPoints(targetLevel)) + specialHits.FirstOrDefault()?.Damage ?? 0;
        var totalTime = 0m;
        var cooldown = detailsProvider.GetCooldown(abilityName, abilityRarity, playerLevel);

        while (targetHitPoints >= 0)
        {
            totalTime += detailsProvider.GetCastDuration(abilityName);

            foreach (var hit in hits.Where(x => x.Timing <= cooldown + detailsProvider.GetCastDuration(abilityName)))
            {
                targetHitPoints -= hit.Damage;

                if (targetHitPoints <= 0)
                {
                    totalTime += hit.Timing;
                }
            }

            totalTime += cooldown;
        }

        return totalTime;
    }

    /// <inheritdoc />
    public override decimal? CalculateTimeToKillBasedOnDps(string abilityName, AbilityRarity abilityRarity, int playerLevel, int targetLevel)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var baseDamageRange = base.CalculateBaseDamageRange(abilityName, abilityRarity, playerLevel);
        var specialDamageRange = base.CalculateSpecialDamageRange(abilityName, abilityRarity, playerLevel);
        var dotDamageRange = base.CalculateDotDamageRange(abilityName, abilityRarity, playerLevel);
        var targetHitPoints = Convert.ToDecimal(PlayerStatsProvider.GetHitPoints(targetLevel));

        var dpsWithoutBonus = baseDamageRange.MaxDps + dotDamageRange.MaxDps;
        var dpsWithBonus = dpsWithoutBonus + specialDamageRange.MaxDps;

        if (targetHitPoints <= 0 || dpsWithBonus <= 0)
        {
            return null;
        }

        var detailsProvider = AbilityServiceProvider.GetAbilityDetailsProvider(abilityName);
        var totalCooldown =  detailsProvider.GetCastDuration(abilityName) + detailsProvider.GetCooldown(abilityName, abilityRarity, playerLevel);
        var damageDealtDuringFirstCooldown = dpsWithoutBonus * totalCooldown;

        if (targetHitPoints <= damageDealtDuringFirstCooldown)
        {
            return targetHitPoints / dpsWithoutBonus;
        }

        return (targetHitPoints - damageDealtDuringFirstCooldown) / dpsWithBonus + totalCooldown;
    }
}
