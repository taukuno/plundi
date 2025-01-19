namespace Plundi.Hammerfall.Core.Models.Abilities;

public class HolyShield : IAbility
{
    private readonly Dictionary<AbilityRarity, decimal> _cooldowns = new()
    {
        { AbilityRarity.Common, 14m },
        { AbilityRarity.Uncommon, 12m },
        { AbilityRarity.Rare, 10m },
        { AbilityRarity.Epic, 8m }
    };

    // The timings for the 2nd and special hits are arbitrary
    // I chose 0.6 s and 0.3 s respectively as an average
    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        {
            AbilityRarity.Common,
            new()
            {
                BaseHits = [new() { Damage = 0.18m, IsRelative = true, Timing = 0m }, new() { Damage = 0.18m, IsRelative = true, Timing = 0.5m }],
                SpecialHits = [new() { Damage = 0.36m, IsRelative = true, Timing = 0.36m }],
                DotHits = []
            }
        },
        {
            AbilityRarity.Uncommon,
            new()
            {
                BaseHits = [new() { Damage = 0.19m, IsRelative = true, Timing = 0m }, new() { Damage = 0.19m, IsRelative = true, Timing = 0.5m }],
                SpecialHits = [new() { Damage = 0.374m, IsRelative = true, Timing = 0.374m }],
                DotHits = []
            }
        },
        {
            AbilityRarity.Rare,
            new()
            {
                BaseHits = [new() { Damage = 0.198m, IsRelative = true, Timing = 0m }, new() { Damage = 0.198m, IsRelative = true, Timing = 0.5m }],
                SpecialHits = [new() { Damage = 0.395m, IsRelative = true, Timing = 0.395m }],
                DotHits = []
            }
        },
        {
            AbilityRarity.Epic,
            new()
            {
                BaseHits = [new() { Damage = 0.2m, IsRelative = true, Timing = 0m }, new() { Damage = 0.2m, IsRelative = true, Timing = 0.5m }],
                SpecialHits = [new() { Damage = 0.409m, IsRelative = true, Timing = 0.409m }],
                DotHits = []
            }
        }
    };

    /// <inheritdoc />
    public string Name => "Holy Shield";

    /// <inheritdoc />
    public decimal CastDuration => 1m;

    /// <inheritdoc />
    public decimal ChannelDuration => 0;

    /// <inheritdoc />
    public string ImagePath => "holy_shield.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "frontal swirly (bommerangs)", Duration = 0m },
            new() { Description = "recast to activate AoE", Duration = 0m }
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
