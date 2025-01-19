using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDetailsProvider;

public class SteelTrapsDetailsProvider : IAbilityDetailsProvider
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
        { AbilityRarity.Common, new() { BaseHits = [new() { Damage = 0.339m, IsRelative = true, Timing = 0m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [new() { Damage = 0.353m, IsRelative = true, Timing = 0m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [new() { Damage = 0.367m, IsRelative = true, Timing = 0m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [new() { Damage = 0.381m, IsRelative = true, Timing = 0m }], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public bool CanHandleAbility(string abilityName)
    {
        return abilityName == "Steel Traps";
    }

    /// <inheritdoc />
    public string GetDisplayName(string ability)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return "Steel Traps";
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

        return 0;
    }

    /// <inheritdoc />
    public string GetImagePath(string ability)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return "steel_traps.jpg";
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
            new() { Description = "throws 5x root AoEs around you", Duration = 0m },
            new() { Description = "root on hit", Duration = 3m }
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
