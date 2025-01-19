namespace Plundi.Hammerfall.Core.Models.Abilities;

public class AuraOfZealotry : IAbility
{
    private readonly Dictionary<AbilityRarity, double> _cooldowns = new()
    {
        { AbilityRarity.Common, 18 },
        { AbilityRarity.Uncommon, 16 },
        { AbilityRarity.Rare, 14 },
        { AbilityRarity.Epic, 12 }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        {
            AbilityRarity.Common,
            new()
            {
                BaseHits =
                [
                    new() { Damage = 0.077, IsRelative = true, Timing = 0 }, new() { Damage = 0.077, IsRelative = true, Timing = 1 }, new() { Damage = 0.077, IsRelative = true, Timing = 2 }, new() { Damage = 0.077, IsRelative = true, Timing = 3 },
                    new() { Damage = 0.077, IsRelative = true, Timing = 4 }, new() { Damage = 0.077, IsRelative = true, Timing = 5 }, new() { Damage = 0.077, IsRelative = true, Timing = 6 }, new() { Damage = 0.077, IsRelative = true, Timing = 7 },
                    new() { Damage = 0.077, IsRelative = true, Timing = 8 }
                ],
                SpecialHits =
                [
                    new() { Damage = 0.046, IsRelative = true, Timing = 1 }, new() { Damage = 0.046, IsRelative = true, Timing = 2 }, new() { Damage = 0.046, IsRelative = true, Timing = 3 }, new() { Damage = 0.046, IsRelative = true, Timing = 4 },
                    new() { Damage = 0.046, IsRelative = true, Timing = 5 }, new() { Damage = 0.046, IsRelative = true, Timing = 6 }, new() { Damage = 0.046, IsRelative = true, Timing = 7 }, new() { Damage = 0.046, IsRelative = true, Timing = 8 }
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
                    new() { Damage = 0.08, IsRelative = true, Timing = 0 }, new() { Damage = 0.08, IsRelative = true, Timing = 1 }, new() { Damage = 0.08, IsRelative = true, Timing = 2 }, new() { Damage = 0.08, IsRelative = true, Timing = 3 },
                    new() { Damage = 0.08, IsRelative = true, Timing = 4 }, new() { Damage = 0.08, IsRelative = true, Timing = 5 }, new() { Damage = 0.08, IsRelative = true, Timing = 6 }, new() { Damage = 0.08, IsRelative = true, Timing = 7 },
                    new() { Damage = 0.08, IsRelative = true, Timing = 8 }
                ],
                SpecialHits =
                [
                    new() { Damage = 0.059, IsRelative = true, Timing = 1 }, new() { Damage = 0.059, IsRelative = true, Timing = 2 }, new() { Damage = 0.059, IsRelative = true, Timing = 3 }, new() { Damage = 0.059, IsRelative = true, Timing = 4 },
                    new() { Damage = 0.059, IsRelative = true, Timing = 5 }, new() { Damage = 0.059, IsRelative = true, Timing = 6 }, new() { Damage = 0.059, IsRelative = true, Timing = 7 }, new() { Damage = 0.059, IsRelative = true, Timing = 8 }
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
                    new() { Damage = 0.085, IsRelative = true, Timing = 0 }, new() { Damage = 0.085, IsRelative = true, Timing = 1 }, new() { Damage = 0.085, IsRelative = true, Timing = 2 }, new() { Damage = 0.085, IsRelative = true, Timing = 3 },
                    new() { Damage = 0.085, IsRelative = true, Timing = 4 }, new() { Damage = 0.085, IsRelative = true, Timing = 5 }, new() { Damage = 0.085, IsRelative = true, Timing = 6 }, new() { Damage = 0.085, IsRelative = true, Timing = 7 },
                    new() { Damage = 0.085, IsRelative = true, Timing = 8 }
                ],
                SpecialHits =
                [
                    new() { Damage = 0.073, IsRelative = true, Timing = 1 }, new() { Damage = 0.073, IsRelative = true, Timing = 2 }, new() { Damage = 0.073, IsRelative = true, Timing = 3 }, new() { Damage = 0.073, IsRelative = true, Timing = 4 },
                    new() { Damage = 0.073, IsRelative = true, Timing = 5 }, new() { Damage = 0.073, IsRelative = true, Timing = 6 }, new() { Damage = 0.073, IsRelative = true, Timing = 7 }, new() { Damage = 0.073, IsRelative = true, Timing = 8 }
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
                    new() { Damage = 0.0859, IsRelative = true, Timing = 0 }, new() { Damage = 0.0859, IsRelative = true, Timing = 1 }, new() { Damage = 0.0859, IsRelative = true, Timing = 2 }, new() { Damage = 0.0859, IsRelative = true, Timing = 3 },
                    new() { Damage = 0.0859, IsRelative = true, Timing = 4 }, new() { Damage = 0.0859, IsRelative = true, Timing = 5 }, new() { Damage = 0.0859, IsRelative = true, Timing = 6 }, new() { Damage = 0.0859, IsRelative = true, Timing = 7 },
                    new() { Damage = 0.0859, IsRelative = true, Timing = 8 }
                ],
                SpecialHits =
                [
                    new() { Damage = 0.087, IsRelative = true, Timing = 1 }, new() { Damage = 0.087, IsRelative = true, Timing = 2 }, new() { Damage = 0.087, IsRelative = true, Timing = 3 }, new() { Damage = 0.087, IsRelative = true, Timing = 4 },
                    new() { Damage = 0.087, IsRelative = true, Timing = 5 }, new() { Damage = 0.087, IsRelative = true, Timing = 6 }, new() { Damage = 0.087, IsRelative = true, Timing = 7 }, new() { Damage = 0.087, IsRelative = true, Timing = 8 }
                ],
                DotHits = []
            }
        }
    };

    /// <inheritdoc />
    public string Name => "Aura of Zealotry";

    /// <inheritdoc />
    public double CastDuration => 0.5;

    /// <inheritdoc />
    public double ChannelDuration => 0;

    /// <inheritdoc />
    public string ImagePath => "aura_of_zealotry.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        const double baseMeleeDamage = 0.226;
        var improvedMeleeDamage = new Dictionary<AbilityRarity, double>
        {
            { AbilityRarity.Common, 0.272 },
            { AbilityRarity.Uncommon, 0.285 },
            { AbilityRarity.Rare, 0.299 },
            { AbilityRarity.Epic, 0.313 }
        };

        var bonusMeleeDamage = improvedMeleeDamage[abilityRarity] / baseMeleeDamage - 1;

        return
        [
            new() { Description = "AoE", Duration = 8 },
            new() { Description = "+12% movement speed", Duration = 0 },
            new() { Description = "+70% movement speed while inside AoE", Duration = 8 },
            new() { Description = $"increases melee damage (+{bonusMeleeDamage:P0})", Duration = 8 }
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