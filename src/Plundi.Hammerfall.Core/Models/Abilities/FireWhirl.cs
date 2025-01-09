namespace Plundi.Hammerfall.Core.Models.Abilities;

public class FireWhirl : IAbility
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
                    new() { Damage = 0.0575, IsRelative = true, Timing = 0.2 }, new() { Damage = 0.0575, IsRelative = true, Timing = 0.4 },
                    new() { Damage = 0.0575, IsRelative = true, Timing = 0.6 }, new() { Damage = 0.0575, IsRelative = true, Timing = 0.8 },
                    new() { Damage = 0.0575, IsRelative = true, Timing = 1 }, new() { Damage = 0.0575, IsRelative = true, Timing = 1.2 },
                    new() { Damage = 0.0575, IsRelative = true, Timing = 1.4 }, new() { Damage = 0.0575, IsRelative = true, Timing = 1.6 },
                    new() { Damage = 0.0575, IsRelative = true, Timing = 1.8 }, new() { Damage = 0.0575, IsRelative = true, Timing = 2 },
                    new() { Damage = 0.0575, IsRelative = true, Timing = 2.2 }, new() { Damage = 0.0575, IsRelative = true, Timing = 2.4 },
                    new() { Damage = 0.0575, IsRelative = true, Timing = 2.6 }, new() { Damage = 0.0575, IsRelative = true, Timing = 2.8 },
                    new() { Damage = 0.0575, IsRelative = true, Timing = 3 }
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
                    new() { Damage = 0.0605, IsRelative = true, Timing = 0.2 }, new() { Damage = 0.0605, IsRelative = true, Timing = 0.4 },
                    new() { Damage = 0.0605, IsRelative = true, Timing = 0.6 }, new() { Damage = 0.0605, IsRelative = true, Timing = 0.8 },
                    new() { Damage = 0.0605, IsRelative = true, Timing = 1 }, new() { Damage = 0.0605, IsRelative = true, Timing = 1.2 },
                    new() { Damage = 0.0605, IsRelative = true, Timing = 1.4 }, new() { Damage = 0.0605, IsRelative = true, Timing = 1.6 },
                    new() { Damage = 0.0605, IsRelative = true, Timing = 1.8 }, new() { Damage = 0.0605, IsRelative = true, Timing = 2 },
                    new() { Damage = 0.0605, IsRelative = true, Timing = 2.2 }, new() { Damage = 0.0605, IsRelative = true, Timing = 2.4 },
                    new() { Damage = 0.0605, IsRelative = true, Timing = 2.6 }, new() { Damage = 0.0605, IsRelative = true, Timing = 2.8 },
                    new() { Damage = 0.0605, IsRelative = true, Timing = 3 }
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
                    new() { Damage = 0.0638, IsRelative = true, Timing = 0.2 }, new() { Damage = 0.0638, IsRelative = true, Timing = 0.4 },
                    new() { Damage = 0.0638, IsRelative = true, Timing = 0.6 }, new() { Damage = 0.0638, IsRelative = true, Timing = 0.8 },
                    new() { Damage = 0.0638, IsRelative = true, Timing = 1 }, new() { Damage = 0.0638, IsRelative = true, Timing = 1.2 },
                    new() { Damage = 0.0638, IsRelative = true, Timing = 1.4 }, new() { Damage = 0.0638, IsRelative = true, Timing = 1.6 },
                    new() { Damage = 0.0638, IsRelative = true, Timing = 1.8 }, new() { Damage = 0.0638, IsRelative = true, Timing = 2 },
                    new() { Damage = 0.0638, IsRelative = true, Timing = 2.2 }, new() { Damage = 0.0638, IsRelative = true, Timing = 2.4 },
                    new() { Damage = 0.0638, IsRelative = true, Timing = 2.6 }, new() { Damage = 0.0638, IsRelative = true, Timing = 2.8 },
                    new() { Damage = 0.0638, IsRelative = true, Timing = 3 }
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
                    new() { Damage = 0.0665, IsRelative = true, Timing = 0.2 }, new() { Damage = 0.0665, IsRelative = true, Timing = 0.4 },
                    new() { Damage = 0.0665, IsRelative = true, Timing = 0.6 }, new() { Damage = 0.0665, IsRelative = true, Timing = 0.8 },
                    new() { Damage = 0.0665, IsRelative = true, Timing = 1 }, new() { Damage = 0.0665, IsRelative = true, Timing = 1.2 },
                    new() { Damage = 0.0665, IsRelative = true, Timing = 1.4 }, new() { Damage = 0.0665, IsRelative = true, Timing = 1.6 },
                    new() { Damage = 0.0665, IsRelative = true, Timing = 1.8 }, new() { Damage = 0.0665, IsRelative = true, Timing = 2 },
                    new() { Damage = 0.0665, IsRelative = true, Timing = 2.2 }, new() { Damage = 0.0665, IsRelative = true, Timing = 2.4 },
                    new() { Damage = 0.0665, IsRelative = true, Timing = 2.6 }, new() { Damage = 0.0665, IsRelative = true, Timing = 2.8 },
                    new() { Damage = 0.0665, IsRelative = true, Timing = 3 }
                ],
                SpecialHits = [],
                DotHits = []
            }
        }
    };

    /// <inheritdoc />
    public string Name => "Fire Whirl";

    /// <inheritdoc />
    public double CastDuration => 1;

    /// <inheritdoc />
    public double ChannelDuration => 3;

    /// <inheritdoc />
    public string ImagePath => "fire_whirl.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "AoE", Duration = 0 },
            new() { Description = "+70% movement speed", Duration = 3 }
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