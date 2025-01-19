namespace Plundi.Hammerfall.Core.Models.Abilities;

public class Earthbreaker : IAbility
{
    private readonly Dictionary<AbilityRarity, double> _cooldowns = new()
    {
        { AbilityRarity.Common, 16 },
        { AbilityRarity.Uncommon, 14 },
        { AbilityRarity.Rare, 12 },
        { AbilityRarity.Epic, 10 }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        { AbilityRarity.Common, new() { BaseHits = [new() { Damage = 0.621, IsRelative = true, Timing = 2 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [new() { Damage = 0.663, IsRelative = true, Timing = 2 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [new() { Damage = 0.7, IsRelative = true, Timing = 2 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [new() { Damage = 0.73, IsRelative = true, Timing = 2 }], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public string Name => "Earthbreaker";

    /// <inheritdoc />
    public double CastDuration => 0;

    /// <inheritdoc />
    public double ChannelDuration => 2;

    /// <inheritdoc />
    public string ImagePath => "earthbreaker.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "AoE", Duration = 0 },
            new() { Description = "immune to cc while channeling", Duration = 2 },
            new() { Description = "stuns on hit", Duration = 2 },
            new() { Description = "big knockback on hit", Duration = 0 },
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