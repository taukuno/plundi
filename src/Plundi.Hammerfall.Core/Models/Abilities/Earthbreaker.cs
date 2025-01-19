namespace Plundi.Hammerfall.Core.Models.Abilities;

public class Earthbreaker : IAbility
{
    private readonly Dictionary<AbilityRarity, decimal> _cooldowns = new()
    {
        { AbilityRarity.Common, 16m },
        { AbilityRarity.Uncommon, 14m },
        { AbilityRarity.Rare, 12m },
        { AbilityRarity.Epic, 10m }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        { AbilityRarity.Common, new() { BaseHits = [new() { Damage = 0.621m, IsRelative = true, Timing = 2m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [new() { Damage = 0.663m, IsRelative = true, Timing = 2m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [new() { Damage = 0.7m, IsRelative = true, Timing = 2m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [new() { Damage = 0.73m, IsRelative = true, Timing = 2m }], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public string Name => "Earthbreaker";

    /// <inheritdoc />
    public decimal CastDuration => 0m;

    /// <inheritdoc />
    public decimal ChannelDuration => 2;

    /// <inheritdoc />
    public string ImagePath => "earthbreaker.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "AoE", Duration = 0m },
            new() { Description = "immune to cc while channeling", Duration = 2m },
            new() { Description = "stuns on hit", Duration = 2m },
            new() { Description = "big knockback on hit", Duration = 0m },
            new() { Description = "heavily slowed while channeling (-97%)", Duration = 2m }
        ];
    }

    /// <inheritdoc />
    public decimal GetCooldown(int characterLevel, AbilityRarity abilityRarity)
    {
        return _cooldowns[abilityRarity];
    }

    /// <inheritdoc />
    public DamageProfile GetDamageProfile(int characterLevel, AbilityRarity abilityRarity)
    {
        return _damageProfiles[abilityRarity];
    }
}
