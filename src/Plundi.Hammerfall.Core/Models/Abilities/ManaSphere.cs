namespace Plundi.Hammerfall.Core.Models.Abilities;

public class ManaSphere : IAbility
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
        { AbilityRarity.Common, new() { BaseHits = [new() { Damage = 0.56, IsRelative = true, Timing = 1.8 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [new() { Damage = 0.59, IsRelative = true, Timing = 1.8 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [new() { Damage = 0.62, IsRelative = true, Timing = 1.8 }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [new() { Damage = 0.65, IsRelative = true, Timing = 1.8 }], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public string Name => "Mana Sphere";

    /// <inheritdoc />
    public double CastDuration => 0;

    /// <inheritdoc />
    public double ChannelDuration => 1.8;

    /// <inheritdoc />
    public string ImagePath => "mana_sphere.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "frontal orb", Duration = 0 },
            new() { Description = "medium knockback on hit", Duration = 0 },
            new() { Description = "size of orb can be charged", Duration = 0 },
            new() { Description = "heavily slowed while channeling (-97%)", Duration = 1.8 }
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