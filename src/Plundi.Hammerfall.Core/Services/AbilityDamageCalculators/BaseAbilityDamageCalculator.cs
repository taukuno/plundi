using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDamageCalculators;

public abstract class BaseAbilityDamageCalculator : IAbilityDamageCalculator
{
    private readonly IEnumerable<IAbilityDetailsProvider> _abilityDetailsProviders;

    protected BaseAbilityDamageCalculator(IEnumerable<IAbilityDetailsProvider> abilityDetailsProviders)
    {
        _abilityDetailsProviders = abilityDetailsProviders;
    }

    /// <inheritdoc />
    public abstract bool CanHandleAbility(string ability);

    /// <inheritdoc />
    public virtual DamageRange CalculateTotalDamageRange(string ability, int characterLevel, AbilityRarity abilityRarity)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

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
    public virtual DamageRange CalculateBaseDamageRange(string ability, int characterLevel, AbilityRarity abilityRarity)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        var detailsProvider = GetAbilityDetailsProvider(ability);

        var totalCooldown = detailsProvider.GetCooldown(ability, characterLevel, abilityRarity) + detailsProvider.GetCastDuration(ability);
        var baseHits = CalculateBaseHits(ability, characterLevel, abilityRarity).ToList();

        return new()
        {
            Min = baseHits.FirstOrDefault()?.Damage ?? 0,
            Max = baseHits.Sum(x => x.Damage),
            MinDps = CalculateDps(baseHits.Take(1), totalCooldown),
            MaxDps = CalculateDps(baseHits, totalCooldown)
        };
    }

    /// <inheritdoc />
    public virtual DamageRange CalculateSpecialDamageRange(string ability, int characterLevel, AbilityRarity abilityRarity)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        var detailsProvider = GetAbilityDetailsProvider(ability);

        var totalCooldown = detailsProvider.GetCooldown(ability, characterLevel, abilityRarity) + detailsProvider.GetCastDuration(ability);
        var specialHits = CalculateSpecialHits(ability, characterLevel, abilityRarity).ToList();

        return new()
        {
            Min = 0,
            Max = specialHits.Sum(x => x.Damage),
            MinDps = 0,
            MaxDps = CalculateDps(specialHits, totalCooldown)
        };
    }

    /// <inheritdoc />
    public virtual DamageRange CalculateDotDamageRange(string ability, int characterLevel, AbilityRarity abilityRarity)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        var detailsProvider = GetAbilityDetailsProvider(ability);

        var totalCooldown = detailsProvider.GetCooldown(ability, characterLevel, abilityRarity) + detailsProvider.GetCastDuration(ability);
        var dotHits = CalculateDotHits(ability, characterLevel, abilityRarity);

        var dotDamage = dotHits.Sum(x => x.Damage);
        var dotDps = CalculateDps(dotHits, totalCooldown);

        return new()
        {
            Min = dotDamage,
            Max = dotDamage,
            MinDps = dotDps,
            MaxDps = dotDps
        };
    }

    /// <inheritdoc />
    public virtual List<DamageHit> CalculateBaseHits(string ability, int characterLevel, AbilityRarity abilityRarity)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        var detailsProvider = GetAbilityDetailsProvider(ability);

        var damageProfile = detailsProvider.GetDamageProfile(ability, characterLevel, abilityRarity);
        var attackPower = CharacterStatsProvider.GetAttackPower(characterLevel);
        return CalculateHits(damageProfile.BaseHits, attackPower);
    }

    /// <inheritdoc />
    public virtual List<DamageHit> CalculateSpecialHits(string ability, int characterLevel, AbilityRarity abilityRarity)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        var detailsProvider = GetAbilityDetailsProvider(ability);

        var damageProfile = detailsProvider.GetDamageProfile(ability, characterLevel, abilityRarity);
        var attackPower = CharacterStatsProvider.GetAttackPower(characterLevel);
        return CalculateHits(damageProfile.SpecialHits, attackPower);
    }

    /// <inheritdoc />
    public virtual List<DamageHit> CalculateDotHits(string ability, int characterLevel, AbilityRarity abilityRarity)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        var detailsProvider = GetAbilityDetailsProvider(ability);

        var damageProfile = detailsProvider.GetDamageProfile(ability, characterLevel, abilityRarity);
        var attackPower = CharacterStatsProvider.GetAttackPower(characterLevel);
        return CalculateHits(damageProfile.DotHits, attackPower);
    }

    /// <inheritdoc />
    public virtual decimal? CalculateTimeToKillBasedOnSimulation(string ability, int characterLevel, AbilityRarity abilityRarity, int targetLevel)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        var detailsProvider = GetAbilityDetailsProvider(ability);

        var baseHits = CalculateBaseHits(ability, characterLevel, abilityRarity);
        var specialHits = CalculateSpecialHits(ability, characterLevel, abilityRarity);
        var dotHits = CalculateDotHits(ability, characterLevel, abilityRarity);
        var hits = baseHits.Concat(specialHits).Concat(dotHits).OrderBy(x => x.Timing).ToList();

        if (baseHits.Sum(x => x.Damage) + specialHits.Sum(x => x.Damage) + dotHits.Sum(x => x.Damage) <= 0)
        {
            return null;
        }

        var targetHitPoints = Convert.ToDecimal(CharacterStatsProvider.GetHitPoints(targetLevel));
        var totalTime = 0m;
        var cooldown = detailsProvider.GetCooldown(ability, characterLevel, abilityRarity);

        while (targetHitPoints >= 0)
        {
            totalTime += detailsProvider.GetCastDuration(ability);

            foreach (var hit in hits.Where(x => x.Timing <= cooldown + detailsProvider.GetCastDuration(ability)))
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
    public virtual decimal? CalculateTimeToKillBasedOnDps(string ability, int characterLevel, AbilityRarity abilityRarity, int targetLevel)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        var targetHitPoints = Convert.ToDecimal(CharacterStatsProvider.GetHitPoints(targetLevel));
        var totalDamageRange = CalculateTotalDamageRange(ability, characterLevel, abilityRarity);

        if (targetHitPoints <= 0 || totalDamageRange.MaxDps <= 0)
        {
            return null;
        }

        return targetHitPoints / totalDamageRange.MaxDps;
    }

    private IAbilityDetailsProvider GetAbilityDetailsProvider(string ability)
    {
        return _abilityDetailsProviders.FirstOrDefault(x => x.CanHandleAbility(ability)) ?? throw new InvalidOperationException($"No details provider registered for the ability '{ability}'.");
    }

    private static List<DamageHit> CalculateHits(IEnumerable<DamageHit> hits, int attackPower)
    {
        return hits.Select(x =>
        {
            var damage = x.IsRelative ? x.Damage * attackPower : x.Damage;
            return new DamageHit { Damage = damage, IsRelative = false, Timing = x.Timing };
        }).ToList();
    }

    private static decimal CalculateDps(IEnumerable<DamageHit> hits, decimal totalCooldown)
    {
        return hits.Where(x => x.Timing <= totalCooldown).Sum(x => x.Damage) / totalCooldown;
    }
}
