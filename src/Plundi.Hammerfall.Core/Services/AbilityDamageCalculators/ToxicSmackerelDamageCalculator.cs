using Plundi.Hammerfall.Core.Models;
using Plundi.Hammerfall.Core.Models.Abilities;

namespace Plundi.Hammerfall.Core.Services.AbilityDamageCalculators;

public class ToxicSmackerelDamageCalculator : BaseAbilityDamageCalculator<ToxicSmackerel>
{
    // It's extremely unlikely to hit a pre-poisoned target; therefore, the special DPS has to be recalculated
    // We reduce the DPS by 33.33% to simulate a scenario where we use Toxic Smackerel 3x on a not yet poisoned target

    /// <inheritdoc />
    public override DamageRange CalculateSpecialDamageRange(ToxicSmackerel ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var damageRange = base.CalculateSpecialDamageRange(ability, characterLevel, abilityRarity);
        return damageRange with
        {
            MaxDps = damageRange.MaxDps * 2 / 3
        };
    }

    /// <inheritdoc />
    public override decimal? CalculateTimeToKillBasedOnSimulation(ToxicSmackerel ability, int characterLevel, AbilityRarity abilityRarity, int targetLevel)
    {
        var baseHits = CalculateBaseHits(ability, characterLevel, abilityRarity);
        var specialHits = CalculateSpecialHits(ability, characterLevel, abilityRarity);
        var dotHits = CalculateDotHits(ability, characterLevel, abilityRarity);
        var hits = baseHits.Concat(specialHits).Concat(dotHits).OrderBy(x => x.Timing).ToList();

        if (baseHits.Sum(x => x.Damage) + specialHits.Sum(x => x.Damage) + dotHits.Sum(x => x.Damage) <= 0)
        {
            return null;
        }

        // We add the bonus damage to the target's health to simulate a not-yet poisoned target
        var targetHitPoints = Convert.ToDecimal(CharacterStatsProvider.GetHitPoints(targetLevel)) + specialHits.FirstOrDefault()?.Damage ?? 0;
        var totalTime = 0m;
        var cooldown = ability.GetCooldown(characterLevel, abilityRarity);

        while (targetHitPoints >= 0)
        {
            totalTime += ability.CastDuration;

            foreach (var hit in hits.Where(x => x.Timing <= cooldown + ability.CastDuration))
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
    public override decimal? CalculateTimeToKillBasedOnDps(ToxicSmackerel ability, int characterLevel, AbilityRarity abilityRarity, int targetLevel)
    {
        var baseDamageRange = base.CalculateBaseDamageRange(ability, characterLevel, abilityRarity);
        var specialDamageRange = base.CalculateSpecialDamageRange(ability, characterLevel, abilityRarity);
        var dotDamageRange = base.CalculateDotDamageRange(ability, characterLevel, abilityRarity);
        var targetHitPoints = Convert.ToDecimal(CharacterStatsProvider.GetHitPoints(targetLevel));

        var dpsWithoutBonus = baseDamageRange.MaxDps + dotDamageRange.MaxDps;
        var dpsWithBonus = dpsWithoutBonus + specialDamageRange.MaxDps;

        if (targetHitPoints <= 0 || dpsWithBonus <= 0)
        {
            return null;
        }

        var realCooldown = ability.CastDuration + ability.GetCooldown(characterLevel, abilityRarity);
        var damageDealtDuringFirstCooldown = dpsWithoutBonus * realCooldown;

        if (targetHitPoints <= damageDealtDuringFirstCooldown)
        {
            return targetHitPoints / dpsWithoutBonus;
        }

        return (targetHitPoints - damageDealtDuringFirstCooldown) / dpsWithBonus + realCooldown;
    }
}
