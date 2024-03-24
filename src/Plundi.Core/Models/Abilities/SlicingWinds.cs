namespace Plundi.Core.Models.Abilities;

public class SlicingWinds : IAbility
{
    private readonly Dictionary<AbilityRarity, double> _cooldowns = new()
    {
        { AbilityRarity.Common, 12 },
        { AbilityRarity.Uncommon, 10 },
        { AbilityRarity.Rare, 8 },
        { AbilityRarity.Epic, 6 }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        { AbilityRarity.Common, new() { DefaultHits = [0.35], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { DefaultHits = [0.37], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { DefaultHits = [0.39], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { DefaultHits = [0.41], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public string Name => "Slicing Winds";

    /// <inheritdoc />
    public double CastDuration => 0;

    /// <inheritdoc />
    public double ChannelDuration => 0;

    /// <inheritdoc />
    public string ImagePath => "slicing_winds.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<(string Effect, double Duration)> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return abilityRarity switch
        {
            AbilityRarity.Common => [("frontal dash", 0), ("dash can be charged up to 12 yards", 0)],
            AbilityRarity.Uncommon => [("frontal dash", 0), ("dash can be charged up to 20 yards", 0)],
            AbilityRarity.Rare => [("frontal dash", 0), ("dash can be charged up to 28 yards", 0)],
            AbilityRarity.Epic => [("frontal dash", 0), ("dash can be charged up to 36 yards", 0)],
            _ => [("frontal dash", 0), ("dash can be charged up to 12 yards", 0)]
        };
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