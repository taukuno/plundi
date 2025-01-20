using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDamageCalculators;

public class DefaultAbilityDamageCalculator : IAbilityDamageCalculator
{
    protected readonly AbilityServiceProvider AbilityServiceProvider;
    private readonly HashSet<string> _handledAbilities =
    [
        "Aura of Zealotry",
        "Celestial Barrage",
        "Earthbreaker",
        "Fire Whirl",
        "Mana Sphere",
        "Rime Arrow",
        "Searing Axe",
        "Slicing Winds",
        "Star Bomb",
        "Storm Archon",
        "Call Galefeather",
        "Explosive Caltrops",
        "Fade to Shadow",
        "Faeform",
        "Hunter's Chains",
        "Lightning Bulwark",
        "Quaking Leap",
        "Repel",
        "Snowdrift",
        "Steel Traps",
        "Windstorm",
        "Void Tear"
    ];

    public DefaultAbilityDamageCalculator(AbilityServiceProvider abilityServiceProvider)
    {
        AbilityServiceProvider = abilityServiceProvider;
    }

    /// <inheritdoc />
    public virtual bool CanHandleAbility(string abilityName)
    {
        return _handledAbilities.Contains(abilityName);
    }

    /// <inheritdoc />
    public virtual DamageRange CalculateTotalDamageRange(string abilityName, AbilityRarity abilityRarity, int characterLevel)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var baseDamageRange = CalculateBaseDamageRange(abilityName, abilityRarity, characterLevel);
        var specialDamageRange = CalculateSpecialDamageRange(abilityName, abilityRarity, characterLevel);
        var dotDamageRange = CalculateDotDamageRange(abilityName, abilityRarity, characterLevel);

        return new()
        {
            Min = baseDamageRange.Min + specialDamageRange.Min + dotDamageRange.Min,
            Max = baseDamageRange.Max + specialDamageRange.Max + dotDamageRange.Max,
            MinDps = baseDamageRange.MinDps + specialDamageRange.MinDps + dotDamageRange.MinDps,
            MaxDps = baseDamageRange.MaxDps + specialDamageRange.MaxDps + dotDamageRange.MaxDps
        };
    }

    /// <inheritdoc />
    public virtual DamageRange CalculateBaseDamageRange(string abilityName, AbilityRarity abilityRarity, int characterLevel)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var detailsProvider = AbilityServiceProvider.GetAbilityDetailsProvider(abilityName);

        var totalCooldown = detailsProvider.GetCooldown(abilityName, abilityRarity, characterLevel) + detailsProvider.GetCastDuration(abilityName);
        var baseHits = CalculateBaseHits(abilityName, abilityRarity, characterLevel).ToList();

        return new()
        {
            Min = baseHits.FirstOrDefault()?.Damage ?? 0,
            Max = baseHits.Sum(x => x.Damage),
            MinDps = CalculateDps(baseHits.Take(1), totalCooldown),
            MaxDps = CalculateDps(baseHits, totalCooldown)
        };
    }

    /// <inheritdoc />
    public virtual DamageRange CalculateSpecialDamageRange(string abilityName, AbilityRarity abilityRarity, int characterLevel)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var detailsProvider = AbilityServiceProvider.GetAbilityDetailsProvider(abilityName);

        var totalCooldown = detailsProvider.GetCooldown(abilityName, abilityRarity, characterLevel) + detailsProvider.GetCastDuration(abilityName);
        var specialHits = CalculateSpecialHits(abilityName, abilityRarity, characterLevel).ToList();

        return new()
        {
            Min = 0,
            Max = specialHits.Sum(x => x.Damage),
            MinDps = 0,
            MaxDps = CalculateDps(specialHits, totalCooldown)
        };
    }

    /// <inheritdoc />
    public virtual DamageRange CalculateDotDamageRange(string abilityName, AbilityRarity abilityRarity, int characterLevel)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var detailsProvider = AbilityServiceProvider.GetAbilityDetailsProvider(abilityName);

        var totalCooldown = detailsProvider.GetCooldown(abilityName, abilityRarity, characterLevel) + detailsProvider.GetCastDuration(abilityName);
        var dotHits = CalculateDotHits(abilityName, abilityRarity, characterLevel);

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
    public virtual List<DamageHit> CalculateBaseHits(string abilityName, AbilityRarity abilityRarity, int characterLevel)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var detailsProvider = AbilityServiceProvider.GetAbilityDetailsProvider(abilityName);

        var damageProfile = detailsProvider.GetDamageProfile(abilityName, abilityRarity, characterLevel);
        var attackPower = CharacterStatsProvider.GetAttackPower(characterLevel);
        return CalculateHits(damageProfile.BaseHits, attackPower);
    }

    /// <inheritdoc />
    public virtual List<DamageHit> CalculateSpecialHits(string abilityName, AbilityRarity abilityRarity, int characterLevel)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var detailsProvider = AbilityServiceProvider.GetAbilityDetailsProvider(abilityName);

        var damageProfile = detailsProvider.GetDamageProfile(abilityName, abilityRarity, characterLevel);
        var attackPower = CharacterStatsProvider.GetAttackPower(characterLevel);
        return CalculateHits(damageProfile.SpecialHits, attackPower);
    }

    /// <inheritdoc />
    public virtual List<DamageHit> CalculateDotHits(string abilityName, AbilityRarity abilityRarity, int characterLevel)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var detailsProvider = AbilityServiceProvider.GetAbilityDetailsProvider(abilityName);

        var damageProfile = detailsProvider.GetDamageProfile(abilityName, abilityRarity, characterLevel);
        var attackPower = CharacterStatsProvider.GetAttackPower(characterLevel);
        return CalculateHits(damageProfile.DotHits, attackPower);
    }

    /// <inheritdoc />
    public virtual decimal? CalculateTimeToKillBasedOnSimulation(string abilityName, AbilityRarity abilityRarity, int characterLevel, int targetLevel)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var detailsProvider = AbilityServiceProvider.GetAbilityDetailsProvider(abilityName);

        var baseHits = CalculateBaseHits(abilityName, abilityRarity, characterLevel);
        var specialHits = CalculateSpecialHits(abilityName, abilityRarity, characterLevel);
        var dotHits = CalculateDotHits(abilityName, abilityRarity, characterLevel);
        var hits = baseHits.Concat(specialHits).Concat(dotHits).OrderBy(x => x.Timing).ToList();

        if (baseHits.Sum(x => x.Damage) + specialHits.Sum(x => x.Damage) + dotHits.Sum(x => x.Damage) <= 0)
        {
            return null;
        }

        var targetHitPoints = Convert.ToDecimal(CharacterStatsProvider.GetHitPoints(targetLevel));
        var totalTime = 0m;
        var cooldown = detailsProvider.GetCooldown(abilityName, abilityRarity, characterLevel);

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
    public virtual decimal? CalculateTimeToKillBasedOnDps(string abilityName, AbilityRarity abilityRarity, int characterLevel, int targetLevel)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var targetHitPoints = Convert.ToDecimal(CharacterStatsProvider.GetHitPoints(targetLevel));
        var totalDamageRange = CalculateTotalDamageRange(abilityName, abilityRarity, characterLevel);

        if (targetHitPoints <= 0 || totalDamageRange.MaxDps <= 0)
        {
            return null;
        }

        return targetHitPoints / totalDamageRange.MaxDps;
    }

    protected virtual List<DamageHit> CalculateHits(IEnumerable<DamageHit> hits, int attackPower)
    {
        return hits.Select(x =>
        {
            var damage = x.IsRelative ? x.Damage * attackPower : x.Damage;
            return new DamageHit { Damage = damage, IsRelative = false, Timing = x.Timing };
        }).ToList();
    }

    protected virtual  decimal CalculateDps(IEnumerable<DamageHit> hits, decimal totalCooldown)
    {
        return hits.Where(x => x.Timing <= totalCooldown).Sum(x => x.Damage) / totalCooldown;
    }
}
