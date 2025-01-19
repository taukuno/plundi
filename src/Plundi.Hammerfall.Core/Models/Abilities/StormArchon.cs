namespace Plundi.Hammerfall.Core.Models.Abilities;

public class StormArchon : IAbility
{
    private readonly Dictionary<AbilityRarity, decimal> _cooldowns = new()
    {
        { AbilityRarity.Common, 20m },
        { AbilityRarity.Uncommon, 18m },
        { AbilityRarity.Rare, 16m },
        { AbilityRarity.Epic, 14m }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        {
            AbilityRarity.Common,
            new()
            {
                BaseHits =
                [
                    new() { Damage = 0.222m, IsRelative = true, Timing = 0.75m },
                    new() { Damage = 0.222m, IsRelative = true, Timing = 1.5m },
                    new() { Damage = 0.445m, IsRelative = true, Timing = 2.25m }
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
                    new() { Damage = 0.237m, IsRelative = true, Timing = 0.75m },
                    new() { Damage = 0.237m, IsRelative = true, Timing = 1.5m },
                    new() { Damage = 0.473m, IsRelative = true, Timing = 2.25m }
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
                    new() { Damage = 0.247m, IsRelative = true, Timing = 0.75m },
                    new() { Damage = 0.247m, IsRelative = true, Timing = 1.5m },
                    new() { Damage = 0.494m, IsRelative = true, Timing = 2.25m }
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
                    new() { Damage = 0.258m, IsRelative = true, Timing = 0.75m },
                    new() { Damage = 0.258m, IsRelative = true, Timing = 1.5m },
                    new() { Damage = 0.515m, IsRelative = true, Timing = 2.25m }
                ],
                SpecialHits = [],
                DotHits = []
            }
        }
    };

    /// <inheritdoc />
    public string Name => "Storm Archon";

    /// <inheritdoc />
    public decimal CastDuration => 0m;

    /// <inheritdoc />
    public decimal ChannelDuration => 2.25m;

    /// <inheritdoc />
    public string ImagePath => "storm_archon.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "frontal barrage", Duration = 0m },
            new() { Description = "can be recast twice (last cast does decimal-damage)", Duration = 0m },
            new() { Description = "heavily slowed while channeling (-96%)", Duration = 2.25m }
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
