namespace Plundi.Hammerfall.Core.Models.Abilities;

public class AuraOfZealotry : IAbility
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
        {
            AbilityRarity.Common,
            new()
            {
                BaseHits =
                [
                    new() { Damage = 0.077m, IsRelative = true, Timing = 0m }, new() { Damage = 0.077m, IsRelative = true, Timing = 1m }, new() { Damage = 0.077m, IsRelative = true, Timing = 2m }, new() { Damage = 0.077m, IsRelative = true, Timing = 3m },
                    new() { Damage = 0.077m, IsRelative = true, Timing = 4m }, new() { Damage = 0.077m, IsRelative = true, Timing = 5m }, new() { Damage = 0.077m, IsRelative = true, Timing = 6m }, new() { Damage = 0.077m, IsRelative = true, Timing = 7m },
                    new() { Damage = 0.077m, IsRelative = true, Timing = 8m }
                ],
                SpecialHits =
                [
                    new() { Damage = 0.046m, IsRelative = true, Timing = 1m }, new() { Damage = 0.046m, IsRelative = true, Timing = 2m }, new() { Damage = 0.046m, IsRelative = true, Timing = 3m }, new() { Damage = 0.046m, IsRelative = true, Timing = 4m },
                    new() { Damage = 0.046m, IsRelative = true, Timing = 5m }, new() { Damage = 0.046m, IsRelative = true, Timing = 6m }, new() { Damage = 0.046m, IsRelative = true, Timing = 7m }, new() { Damage = 0.046m, IsRelative = true, Timing = 8m }
                ],
                DotHits = []
            }
        },
        {
            AbilityRarity.Uncommon,
            new()
            {
                BaseHits =
                [
                    new() { Damage = 0.08m, IsRelative = true, Timing = 0m }, new() { Damage = 0.08m, IsRelative = true, Timing = 1m }, new() { Damage = 0.08m, IsRelative = true, Timing = 2m }, new() { Damage = 0.08m, IsRelative = true, Timing = 3m },
                    new() { Damage = 0.08m, IsRelative = true, Timing = 4m }, new() { Damage = 0.08m, IsRelative = true, Timing = 5m }, new() { Damage = 0.08m, IsRelative = true, Timing = 6m }, new() { Damage = 0.08m, IsRelative = true, Timing = 7m },
                    new() { Damage = 0.08m, IsRelative = true, Timing = 8m }
                ],
                SpecialHits =
                [
                    new() { Damage = 0.059m, IsRelative = true, Timing = 1m }, new() { Damage = 0.059m, IsRelative = true, Timing = 2m }, new() { Damage = 0.059m, IsRelative = true, Timing = 3m }, new() { Damage = 0.059m, IsRelative = true, Timing = 4m },
                    new() { Damage = 0.059m, IsRelative = true, Timing = 5m }, new() { Damage = 0.059m, IsRelative = true, Timing = 6m }, new() { Damage = 0.059m, IsRelative = true, Timing = 7m }, new() { Damage = 0.059m, IsRelative = true, Timing = 8m }
                ],
                DotHits = []
            }
        },
        {
            AbilityRarity.Rare,
            new()
            {
                BaseHits =
                [
                    new() { Damage = 0.085m, IsRelative = true, Timing = 0m }, new() { Damage = 0.085m, IsRelative = true, Timing = 1m }, new() { Damage = 0.085m, IsRelative = true, Timing = 2m }, new() { Damage = 0.085m, IsRelative = true, Timing = 3m },
                    new() { Damage = 0.085m, IsRelative = true, Timing = 4m }, new() { Damage = 0.085m, IsRelative = true, Timing = 5m }, new() { Damage = 0.085m, IsRelative = true, Timing = 6m }, new() { Damage = 0.085m, IsRelative = true, Timing = 7m },
                    new() { Damage = 0.085m, IsRelative = true, Timing = 8m }
                ],
                SpecialHits =
                [
                    new() { Damage = 0.073m, IsRelative = true, Timing = 1m }, new() { Damage = 0.073m, IsRelative = true, Timing = 2m }, new() { Damage = 0.073m, IsRelative = true, Timing = 3m }, new() { Damage = 0.073m, IsRelative = true, Timing = 4m },
                    new() { Damage = 0.073m, IsRelative = true, Timing = 5m }, new() { Damage = 0.073m, IsRelative = true, Timing = 6m }, new() { Damage = 0.073m, IsRelative = true, Timing = 7m }, new() { Damage = 0.073m, IsRelative = true, Timing = 8m }
                ],
                DotHits = []
            }
        },
        {
            AbilityRarity.Epic,
            new()
            {
                BaseHits =
                [
                    new() { Damage = 0.0859m, IsRelative = true, Timing = 0m }, new() { Damage = 0.0859m, IsRelative = true, Timing = 1m }, new() { Damage = 0.0859m, IsRelative = true, Timing = 2m }, new() { Damage = 0.0859m, IsRelative = true, Timing = 3m },
                    new() { Damage = 0.0859m, IsRelative = true, Timing = 4m }, new() { Damage = 0.0859m, IsRelative = true, Timing = 5m }, new() { Damage = 0.0859m, IsRelative = true, Timing = 6m }, new() { Damage = 0.0859m, IsRelative = true, Timing = 7m },
                    new() { Damage = 0.0859m, IsRelative = true, Timing = 8m }
                ],
                SpecialHits =
                [
                    new() { Damage = 0.087m, IsRelative = true, Timing = 1m }, new() { Damage = 0.087m, IsRelative = true, Timing = 2m }, new() { Damage = 0.087m, IsRelative = true, Timing = 3m }, new() { Damage = 0.087m, IsRelative = true, Timing = 4m },
                    new() { Damage = 0.087m, IsRelative = true, Timing = 5m }, new() { Damage = 0.087m, IsRelative = true, Timing = 6m }, new() { Damage = 0.087m, IsRelative = true, Timing = 7m }, new() { Damage = 0.087m, IsRelative = true, Timing = 8m }
                ],
                DotHits = []
            }
        }
    };

    /// <inheritdoc />
    public string Name => "Aura of Zealotry";

    /// <inheritdoc />
    public decimal CastDuration => 0.5m;

    /// <inheritdoc />
    public decimal ChannelDuration => 0;

    /// <inheritdoc />
    public string ImagePath => "aura_of_zealotry.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        const decimal baseMeleeDamage = 0.226m;
        var improvedMeleeDamage = new Dictionary<AbilityRarity, decimal>
        {
            { AbilityRarity.Common, 0.272m },
            { AbilityRarity.Uncommon, 0.285m },
            { AbilityRarity.Rare, 0.299m },
            { AbilityRarity.Epic, 0.313m }
        };

        var bonusMeleeDamage = improvedMeleeDamage[abilityRarity] / baseMeleeDamage - 1;

        return
        [
            new() { Description = "AoE", Duration = 8m },
            new() { Description = "+12% movement speed", Duration = 0m },
            new() { Description = "+70% movement speed while inside AoE", Duration = 8m },
            new() { Description = $"increases melee damage (+{bonusMeleeDamage:P0})", Duration = 8m }
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
