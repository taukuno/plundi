namespace Plundi.Core.Models.Abilities;

public class SteelTraps : IAbility
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
        { AbilityRarity.Common, new() { DefaultHits = [0.24], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { DefaultHits = [0.25], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { DefaultHits = [0.26], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { DefaultHits = [0.27], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public string Name => "Steel Traps";

    /// <inheritdoc />
    public double CastDuration => 0;

    /// <inheritdoc />
    public double ChannelDuration => 0;

    /// <inheritdoc />
    public string ImagePath => "steel_traps.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Utility;

    /// <inheritdoc />
    public List<(string Effect, double Duration)> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return [("throws 5x root AoEs around you", 0), ("root on hit", 3)];
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