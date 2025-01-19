namespace Plundi.Hammerfall.Core.Models.Abilities;

public class SlicingWinds : IAbility
{
    private readonly Dictionary<AbilityRarity, decimal> _cooldowns = new()
    {
        { AbilityRarity.Common, 14m },
        { AbilityRarity.Uncommon, 12m },
        { AbilityRarity.Rare, 10m },
        { AbilityRarity.Epic, 8m }
    };

    // The hit timing is the average between an instant cast and a fully charged one
    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        { AbilityRarity.Common, new() { BaseHits = [new() { Damage = 0.494m, IsRelative = true, Timing = 1.4m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [new() { Damage = 0.522m, IsRelative = true, Timing = 1.4m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [new() { Damage = 0.55m, IsRelative = true, Timing = 1.4m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [new() { Damage = 0.579m, IsRelative = true, Timing = 1.4m }], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public string Name => "Slicing Winds";

    /// <inheritdoc />
    public decimal CastDuration => 0m;

    /// <inheritdoc />
    public decimal ChannelDuration => 1.4m;

    /// <inheritdoc />
    public string ImagePath => "slicing_winds.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return abilityRarity switch
        {
            AbilityRarity.Common =>
            [
                new() { Description = "frontal dash", Duration = 0m },
                new() { Description = "dash can be charged up to 12 yards", Duration = 0m },
                new() { Description = "heavily slowed while channeling (-97%)", Duration = 1.4m }
            ],
            AbilityRarity.Uncommon =>
            [
                new() { Description = "frontal dash", Duration = 0m },
                new() { Description = "dash can be charged up to 20 yards", Duration = 0m },
                new() { Description = "heavily slowed while channeling (-97%)", Duration = 1.4m }
            ],
            AbilityRarity.Rare =>
            [
                new() { Description = "frontal dash", Duration = 0m },
                new() { Description = "dash can be charged up to 28 yards", Duration = 0m },
                new() { Description = "heavily slowed while channeling (-97%)", Duration = 1.4m }
            ],
            AbilityRarity.Epic =>
            [
                new() { Description = "frontal dash", Duration = 0m },
                new() { Description = "dash can be charged up to 36 yards", Duration = 0m },
                new() { Description = "heavily slowed while channeling (-97%)", Duration = 1.4m }
            ],
            _ =>
            [
                new() { Description = "frontal dash", Duration = 0m },
                new() { Description = "dash can be charged up to 12 yards", Duration = 0m },
                new() { Description = "heavily slowed while channeling (-97%)", Duration = 1.4m }
            ]
        };
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
