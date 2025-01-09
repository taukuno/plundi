namespace Plundi.Hammerfall.Core.Models.Abilities;

public class SlicingWinds : IAbility
{
    private readonly Dictionary<AbilityRarity, double> _cooldowns = new()
    {
        { AbilityRarity.Common, 12 },
        { AbilityRarity.Uncommon, 10 },
        { AbilityRarity.Rare, 8 },
        { AbilityRarity.Epic, 6 }
    };

    // The hit timing is the average between an instant cast and a fully charged one
    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        { AbilityRarity.Common, new() { BaseHits = [new() { Damage = 0.35, IsRelative = true, Timing = 0.7 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [new() { Damage = 0.37, IsRelative = true, Timing = 0.7 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [new() { Damage = 0.39, IsRelative = true, Timing = 0.7 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [new() { Damage = 0.41, IsRelative = true, Timing = 0.7 }], SpecialHits = [], DotHits = [] } }
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
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return abilityRarity switch
        {
            AbilityRarity.Common =>
            [
                new() { Description = "frontal dash", Duration = 0 },
                new() { Description = "dash can be charged up to 12 yards", Duration = 0 }
            ],
            AbilityRarity.Uncommon =>
            [
                new() { Description = "frontal dash", Duration = 0 },
                new() { Description = "dash can be charged up to 20 yards", Duration = 0 }
            ],
            AbilityRarity.Rare =>
            [
                new() { Description = "frontal dash", Duration = 0 },
                new() { Description = "dash can be charged up to 28 yards", Duration = 0 }
            ],
            AbilityRarity.Epic =>
            [
                new() { Description = "frontal dash", Duration = 0 },
                new() { Description = "dash can be charged up to 36 yards", Duration = 0 }
            ],
            _ =>
            [
                new() { Description = "frontal dash", Duration = 0 },
                new() { Description = "dash can be charged up to 12 yards", Duration = 0 }
            ]
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