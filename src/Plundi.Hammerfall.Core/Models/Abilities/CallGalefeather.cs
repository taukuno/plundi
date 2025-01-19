namespace Plundi.Hammerfall.Core.Models.Abilities;

public class CallGalefeather : IAbility
{
    private readonly Dictionary<AbilityRarity, decimal> _cooldowns = new()
    {
        { AbilityRarity.Common, 18m },
        { AbilityRarity.Uncommon, 16m },
        { AbilityRarity.Rare, 14m },
        { AbilityRarity.Epic, 12m }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        { AbilityRarity.Common, new() { BaseHits = [], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public string Name => "Call Galefeather";

    /// <inheritdoc />
    public decimal CastDuration => 0m;

    /// <inheritdoc />
    public decimal ChannelDuration => 0;

    /// <inheritdoc />
    public string ImagePath => "call_galefeather.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Utility;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "targetable AoE", Duration = 0m },
            new() { Description = "big knockback on hit", Duration = 8m }
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
