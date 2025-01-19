using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDetailsProvider;

public class RepelDetailsProvider : IAbilityDetailsProvider
{
    private readonly Dictionary<AbilityRarity, decimal> _cooldowns = new()
    {
        { AbilityRarity.Common, 18m },
        { AbilityRarity.Uncommon, 16m },
        { AbilityRarity.Rare, 14m },
        { AbilityRarity.Epic, 12m }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        { AbilityRarity.Common, new() { BaseHits = [new() { Damage = 0.2965m, IsRelative = true, Timing = 0m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [new() { Damage = 0.31m, IsRelative = true, Timing = 0m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [new() { Damage = 0.325m, IsRelative = true, Timing = 0m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [new() { Damage = 0.339m, IsRelative = true, Timing = 0m }], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public bool CanHandleAbility(string abilityName)
    {
        return abilityName == "Repel";
    }

    /// <inheritdoc />
    public string GetDisplayName(string ability)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return "Repel";
    }

    /// <inheritdoc />
    public decimal GetCastDuration(string ability)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return 0m;
    }

    /// <inheritdoc />
    public decimal GetChannelDuration(string ability)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return 1.25m;
    }

    /// <inheritdoc />
    public string GetImagePath(string ability)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return "repel.jpg";
    }

    /// <inheritdoc />
    public AbilityType GetAbilityType(string ability)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return AbilityType.Utility;
    }

    /// <inheritdoc />
    public IEnumerable<AbilityEffect> GetEffects(string ability, int characterLevel, AbilityRarity abilityRarity)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return
        [
            new() { Description = "AoE", Duration = 0m },
            new() { Description = "damage immunity", Duration = 1.25m },
            new() { Description = "cc immunity", Duration = 1.25m },
            new() { Description = "knockback on hit", Duration = 0m },
            new() { Description = "silence on hit", Duration = 2.5m },
            new() { Description = "heavily slowed while channeling (-97%)", Duration = 1.25m }
        ];
    }

    /// <inheritdoc />
    public decimal GetCooldown(string ability, int characterLevel, AbilityRarity abilityRarity)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return _cooldowns[abilityRarity];
    }

    /// <inheritdoc />
    public DamageProfile GetDamageProfile(string ability, int characterLevel, AbilityRarity abilityRarity)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return _damageProfiles[abilityRarity];
    }
}
