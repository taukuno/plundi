using Plundi.Core.Models;

namespace Plundi.Core.Services.AbilityDamageCalculators;

public abstract class BaseAbilityDamageCalculator<T> : IAbilityDamageCalculator<T> where T : IAbility
{
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

    public virtual List<DamageHit> CalculateBaseHits(T ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var damageProfile = ability.GetDamageProfile(characterLevel, abilityRarity);
        var attackPower = CharacterStatsProvider.GetAttackPower(characterLevel);
        return CalculateHits(damageProfile.BaseHits, attackPower);
    }

    public virtual List<DamageHit> CalculateSpecialHits(T ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var damageProfile = ability.GetDamageProfile(characterLevel, abilityRarity);
        var attackPower = CharacterStatsProvider.GetAttackPower(characterLevel);
        return CalculateHits(damageProfile.SpecialHits, attackPower);
    }

    public virtual List<DamageHit> CalculateDotHits(T ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var damageProfile = ability.GetDamageProfile(characterLevel, abilityRarity);
        var attackPower = CharacterStatsProvider.GetAttackPower(characterLevel);
        return CalculateHits(damageProfile.DotHits, attackPower);
    }

    /// <remarks>
    /// The time to kill is only theoretically. Abilities don't deal constant damage and therefore the real time to kill will vary.
    /// </remarks>
    public virtual double? CalculateTimeToKill(T ability, int characterLevel, AbilityRarity abilityRarity, int targetLevel)
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