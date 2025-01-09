using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDamageCalculators;

public abstract class BaseAbilityDamageCalculator<T> : IAbilityDamageCalculator<T> where T : IAbility
{
    /// <inheritdoc />
    public virtual DamageRange CalculateTotalDamageRange(T ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var baseDamageRange = CalculateBaseDamageRange(ability, characterLevel, abilityRarity);
        var specialDamageRange = CalculateSpecialDamageRange(ability, characterLevel, abilityRarity);
        var dotDamageRange = CalculateDotDamageRange(ability, characterLevel, abilityRarity);

        return new()
        {
            Min = baseDamageRange.Min + specialDamageRange.Min + dotDamageRange.Min,
            Max = baseDamageRange.Max + specialDamageRange.Max + dotDamageRange.Max,
            MinDps = baseDamageRange.MinDps + specialDamageRange.MinDps + dotDamageRange.MinDps,
            MaxDps = baseDamageRange.MaxDps + specialDamageRange.MaxDps + dotDamageRange.MaxDps
        };
    }

    /// <inheritdoc />
    public virtual DamageRange CalculateBaseDamageRange(T ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var realCooldown = ability.GetCooldown(characterLevel, abilityRarity) + ability.CastDuration;
        var baseHits = CalculateBaseHits(ability, characterLevel, abilityRarity).ToList();

        return new()
        {
            Min = baseHits.FirstOrDefault()?.Damage ?? 0,
            Max = baseHits.Sum(x => x.Damage),
            MinDps = CalculateDps(baseHits.Take(1), realCooldown),
            MaxDps = CalculateDps(baseHits, realCooldown)
        };
    }

    /// <inheritdoc />
    public virtual DamageRange CalculateSpecialDamageRange(T ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var realCooldown = ability.GetCooldown(characterLevel, abilityRarity) + ability.CastDuration;
        var specialHits = CalculateSpecialHits(ability, characterLevel, abilityRarity).ToList();

        return new()
        {
            Min = 0,
            Max = specialHits.Sum(x => x.Damage),
            MinDps = 0,
            MaxDps = CalculateDps(specialHits, realCooldown)
        };
    }

    /// <inheritdoc />
    public virtual DamageRange CalculateDotDamageRange(T ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var realCooldown = ability.GetCooldown(characterLevel, abilityRarity) + ability.CastDuration;
        var dotHits = CalculateDotHits(ability, characterLevel, abilityRarity);

        var dotDamage = dotHits.Sum(x => x.Damage);
        var dotDps = CalculateDps(dotHits, realCooldown);

        return new()
        {
            Min = dotDamage,
            Max = dotDamage,
            MinDps = dotDps,
            MaxDps = dotDps
        };
    }

    /// <inheritdoc />
    public virtual List<DamageHit> CalculateBaseHits(T ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var damageProfile = ability.GetDamageProfile(characterLevel, abilityRarity);
        var attackPower = CharacterStatsProvider.GetAttackPower(characterLevel);
        return CalculateHits(damageProfile.BaseHits, attackPower);
    }

    /// <inheritdoc />
    public virtual List<DamageHit> CalculateSpecialHits(T ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var damageProfile = ability.GetDamageProfile(characterLevel, abilityRarity);
        var attackPower = CharacterStatsProvider.GetAttackPower(characterLevel);
        return CalculateHits(damageProfile.SpecialHits, attackPower);
    }

    /// <inheritdoc />
    public virtual List<DamageHit> CalculateDotHits(T ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var damageProfile = ability.GetDamageProfile(characterLevel, abilityRarity);
        var attackPower = CharacterStatsProvider.GetAttackPower(characterLevel);
        return CalculateHits(damageProfile.DotHits, attackPower);
    }

    /// <inheritdoc />
    public virtual double? CalculateTimeToKillBasedOnSimulation(T ability, int characterLevel, AbilityRarity abilityRarity, int targetLevel)
    {
        var baseHits = CalculateBaseHits(ability, characterLevel, abilityRarity);
        var specialHits = CalculateSpecialHits(ability, characterLevel, abilityRarity);
        var dotHits = CalculateDotHits(ability, characterLevel, abilityRarity);
        var hits = baseHits.Concat(specialHits).Concat(dotHits).OrderBy(x => x.Timing).ToList();

        if (baseHits.Sum(x => x.Damage) + specialHits.Sum(x => x.Damage) + dotHits.Sum(x => x.Damage) <= 0)
        {
            return null;
        }

        var targetHitPoints = Convert.ToDouble(CharacterStatsProvider.GetHitPoints(targetLevel));
        var totalTime = 0d;
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
    public virtual double? CalculateTimeToKillBasedOnDps(T ability, int characterLevel, AbilityRarity abilityRarity, int targetLevel)
    {
        var targetHitPoints = Convert.ToDouble(CharacterStatsProvider.GetHitPoints(targetLevel));
        var totalDamageRange = CalculateTotalDamageRange(ability, characterLevel, abilityRarity);

        if (targetHitPoints <= 0 || totalDamageRange.MaxDps <= 0)
        {
            return null;
        }

        return targetHitPoints / totalDamageRange.MaxDps;
    }

    private static List<DamageHit> CalculateHits(IEnumerable<DamageHit> hits, int attackPower)
    {
        return hits.Select(x =>
        {
            var damage = x.IsRelative ? x.Damage * attackPower : x.Damage;
            return new DamageHit { Damage = damage, IsRelative = false, Timing = x.Timing };
        }).ToList();
    }

    private static double CalculateDps(IEnumerable<DamageHit> hits, double realCooldown)
    {
        return hits.Where(x => x.Timing <= realCooldown).Sum(x => x.Damage) / realCooldown;
    }
}
