namespace Plundi.Hammerfall.Core.Models.Abilities;

public class QuakingLeap : IAbility
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
        { AbilityRarity.Common, new() { BaseHits = [new() { Damage = 0.395, IsRelative = true, Timing = 0.5 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [new() { Damage = 0.408, IsRelative = true, Timing = 0.5 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [new() { Damage = 0.424, IsRelative = true, Timing = 0.5 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [new() { Damage = 0.438, IsRelative = true, Timing = 0.5 }], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public string Name => "Quaking Leap";

    /// <inheritdoc />
    public double CastDuration => 0;

    /// <inheritdoc />
    public double ChannelDuration => 0.5;

    /// <inheritdoc />
    public string ImagePath => "quaking_leap.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Utility;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "stun AoE on crash down", Duration = 0 },
            new() { Description = "stuns on hit", Duration = 0.75 },
            new() { Description = "can be recast mid air to crash down early", Duration = 0 },
            new() { Description = "heavily slowed while channeling (-99%)", Duration = 0.5 }
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