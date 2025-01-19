namespace Plundi.Hammerfall.Core.Models.Abilities;

public class ManaSphere : IAbility
{
    private readonly Dictionary<AbilityRarity, decimal> _cooldowns = new()
    {
        { AbilityRarity.Common, 14m },
        { AbilityRarity.Uncommon, 12m },
        { AbilityRarity.Rare, 10m },
        { AbilityRarity.Epic, 8m }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        { AbilityRarity.Common, new() { BaseHits = [new() { Damage = 0.56m, IsRelative = true, Timing = 1.8m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [new() { Damage = 0.59m, IsRelative = true, Timing = 1.8m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [new() { Damage = 0.62m, IsRelative = true, Timing = 1.8m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [new() { Damage = 0.65m, IsRelative = true, Timing = 1.8m }], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public string Name => "Mana Sphere";

    /// <inheritdoc />
    public decimal CastDuration => 0m;

    /// <inheritdoc />
    public decimal ChannelDuration => 1.8m;

    /// <inheritdoc />
    public string ImagePath => "mana_sphere.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "frontal orb", Duration = 0m },
            new() { Description = "medium knockback on hit", Duration = 0m },
            new() { Description = "size of orb can be charged", Duration = 0m },
            new() { Description = "heavily slowed while channeling (-97%)", Duration = 1.8m }
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
