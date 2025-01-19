namespace Plundi.Hammerfall.Core.Models.Abilities;

public class Snowdrift : IAbility
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
        {
            AbilityRarity.Common,
            new()
            {
                BaseHits =
                [
                    new() { Damage = 0.1412m, IsRelative = true, Timing = 0m },
                    new() { Damage = 0.1412m, IsRelative = true, Timing = 0.5m },
                    new() { Damage = 0.1412m, IsRelative = true, Timing = 1m }
                ],
                SpecialHits = [],
                DotHits = []
            }
        },
        {
            AbilityRarity.Uncommon,
            new()
            {
                BaseHits =
                [
                    new() { Damage = 0.1497m, IsRelative = true, Timing = 0m },
                    new() { Damage = 0.1497m, IsRelative = true, Timing = 0.5m },
                    new() { Damage = 0.1497m, IsRelative = true, Timing = 1m }
                ],
                SpecialHits = [],
                DotHits = []
            }
        },
        {
            AbilityRarity.Rare,
            new()
            {
                BaseHits =
                [
                    new() { Damage = 0.158m, IsRelative = true, Timing = 0m },
                    new() { Damage = 0.158m, IsRelative = true, Timing = 0.5m },
                    new() { Damage = 0.158m, IsRelative = true, Timing = 1m }
                ],
                SpecialHits = [],
                DotHits = []
            }
        },
        {
            AbilityRarity.Epic,
            new()
            {
                BaseHits =
                [
                    new() { Damage = 0.166m, IsRelative = true, Timing = 0m },
                    new() { Damage = 0.166m, IsRelative = true, Timing = 0.5m },
                    new() { Damage = 0.166m, IsRelative = true, Timing = 1m }
                ],
                SpecialHits = [],
                DotHits = []
            }
        }
    };

    /// <inheritdoc />
    public string Name => "Snowdrift";

    /// <inheritdoc />
    public decimal CastDuration => 0.5m;

    /// <inheritdoc />
    public decimal ChannelDuration => 1;

    /// <inheritdoc />
    public string ImagePath => "snowdrift.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Utility;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "AoE", Duration = 0m },
            new() { Description = "stacking slow (30-90%) on hit", Duration = 4.5m }
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
