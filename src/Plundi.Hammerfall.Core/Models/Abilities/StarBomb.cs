namespace Plundi.Hammerfall.Core.Models.Abilities;

public class StarBomb : IAbility
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
        { AbilityRarity.Common, new() { BaseHits = [new() { Damage = 1.129m, IsRelative = true, Timing = 0m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [new() { Damage = 1.186m, IsRelative = true, Timing = 0m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [new() { Damage = 1.242m, IsRelative = true, Timing = 0m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [new() { Damage = 1.3m, IsRelative = true, Timing = 0m }], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public string Name => "Star Bomb";

    /// <inheritdoc />
    public decimal CastDuration => 2m;

    /// <inheritdoc />
    public decimal ChannelDuration => 0;

    /// <inheritdoc />
    public string ImagePath => "star_bomb.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "frontal AoE", Duration = 0m },
            new() { Description = "pulls enemies into center on hit", Duration = 0m },
            new() { Description = "heavily slowed while casting (-97%)", Duration = 2m }
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
