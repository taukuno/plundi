namespace Plundi.Core.Models.Abilities;

public class QuakingLeap : IAbility
{
    private readonly Dictionary<AbilityRarity, double> _cooldowns = new()
    {
        { AbilityRarity.Common, 14 },
        { AbilityRarity.Uncommon, 12 },
        { AbilityRarity.Rare, 10 },
        { AbilityRarity.Epic, 8 }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        { AbilityRarity.Common, new() { DefaultHits = [0.28], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { DefaultHits = [0.29], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { DefaultHits = [0.3], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { DefaultHits = [0.31], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public string Name => "Quaking Leap";

    /// <inheritdoc />
    public double CastDuration => 0;

    /// <inheritdoc />
    public double ChannelDuration => 0.5;

    /// <inheritdoc />
    public string ImagePath => "quaking_leap.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Utility;

    /// <inheritdoc />
    public List<(string Effect, double Duration)> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return [("stun AoE on crash down", 0), ("stuns on hit", 0.75), ("can be recast mid air to crash down early", 0)];
    }

    /// <inheritdoc />
    public double GetCooldown(int characterLevel, AbilityRarity abilityRarity)
    {
        return _cooldowns[abilityRarity];
    }

    /// <inheritdoc />
    public DamageProfile GetDamageProfile(int characterLevel, AbilityRarity abilityRarity)
    {
        return _damageProfiles[abilityRarity];
    }
}