namespace Plundi.Core.Models.Abilities;

public class StarBomb : IAbility
{
    private readonly Dictionary<AbilityRarity, double> _cooldowns = new()
    {
        { AbilityRarity.Common, 18 },
        { AbilityRarity.Uncommon, 16 },
        { AbilityRarity.Rare, 14 },
        { AbilityRarity.Epic, 12 }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        { AbilityRarity.Common, new() { DefaultHits = [0.8], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { DefaultHits = [0.84], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { DefaultHits = [0.88], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { DefaultHits = [0.92], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public string Name => "Star Bomb";

    /// <inheritdoc />
    public double CastDuration => 2;

    /// <inheritdoc />
    public double ChannelDuration => 0;

    /// <inheritdoc />
    public string ImagePath => "star_bomb.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<(string Effect, double Duration)> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return [("frontal AoE", 0), ("pulls enemies into center on hit", 0)];
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