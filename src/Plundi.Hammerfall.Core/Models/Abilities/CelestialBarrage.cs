namespace Plundi.Hammerfall.Core.Models.Abilities;

public class CelestialBarrage : IAbility
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
        { AbilityRarity.Common, new() { BaseHits = [new() { Damage = 0.595, IsRelative = true, Timing = 0 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [new() { Damage = 0.625, IsRelative = true, Timing = 0 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [new() { Damage = 0.654, IsRelative = true, Timing = 0 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [new() { Damage = 0.684, IsRelative = true, Timing = 0 }], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public string Name => "Celestial Barrage";

    /// <inheritdoc />
    public double CastDuration => 0;

    /// <inheritdoc />
    public double ChannelDuration => 2;

    /// <inheritdoc />
    public string ImagePath => "celestial_barrage.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "frontal beam", Duration = 0 },
            new() { Description = "length of beam can be charged", Duration = 0 },
            new() { Description = "heavily slowed while channeling (-97%)", Duration = 2 }
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