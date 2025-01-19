namespace Plundi.Hammerfall.Core.Models.Abilities;

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
        { AbilityRarity.Common, new() { BaseHits = [new() { Damage = 0.339, IsRelative = true, Timing = 0 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [new() { Damage = 0.353, IsRelative = true, Timing = 0 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [new() { Damage = 0.367, IsRelative = true, Timing = 0 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [new() { Damage = 0.381, IsRelative = true, Timing = 0 }], SpecialHits = [], DotHits = [] } }
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
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "throws 5x root AoEs around you", Duration = 0 },
            new() { Description = "root on hit", Duration = 3 }
        ];
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