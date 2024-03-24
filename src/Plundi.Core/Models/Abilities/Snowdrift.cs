namespace Plundi.Core.Models.Abilities;

public class Snowdrift : IAbility
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
        { AbilityRarity.Common, new() { DefaultHits = [0.1, 0.1, 0.1], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { DefaultHits = [0.106, 0.106, 0.106], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { DefaultHits = [0.112, 0.112, 0.112], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { DefaultHits = [0.118, 0.118, 0.118], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public string Name => "Snowdrift";

    /// <inheritdoc />
    public double CastDuration => 0.5;

    /// <inheritdoc />
    public double ChannelDuration => 2;

    /// <inheritdoc />
    public string ImagePath => "snowdrift.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Utility;

    /// <inheritdoc />
    public List<(string Effect, double Duration)> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return [("AoE", 0), ("stacking slow (30-90%) on hit", 4.5)];
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