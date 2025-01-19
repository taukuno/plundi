namespace Plundi.Hammerfall.Core.Models.Abilities;

public class LightningBulwark : IAbility
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
        {
            AbilityRarity.Common,
            new()
            {
                BaseHits = [],
                SpecialHits =
                [
                    new() { Damage = 0.049, IsRelative = true, Timing = 0.4 }, new() { Damage = 0.049, IsRelative = true, Timing = 0.8 },
                    new() { Damage = 0.049, IsRelative = true, Timing = 1.2 }, new() { Damage = 0.049, IsRelative = true, Timing = 1.6 },
                    new() { Damage = 0.049, IsRelative = true, Timing = 2.0 }, new() { Damage = 0.049, IsRelative = true, Timing = 2.4 },
                    new() { Damage = 0.049, IsRelative = true, Timing = 2.8 }, new() { Damage = 0.049, IsRelative = true, Timing = 3.2 },
                    new() { Damage = 0.049, IsRelative = true, Timing = 3.6 }, new() { Damage = 0.049, IsRelative = true, Timing = 4.0 }
                ],
                DotHits = []
            }
        },
        {
            AbilityRarity.Uncommon,
            new()
            {
                BaseHits = [],
                SpecialHits =
                [
                    new() { Damage = 0.051, IsRelative = true, Timing = 0.4 }, new() { Damage = 0.051, IsRelative = true, Timing = 0.8 },
                    new() { Damage = 0.051, IsRelative = true, Timing = 1.2 }, new() { Damage = 0.051, IsRelative = true, Timing = 1.6 },
                    new() { Damage = 0.051, IsRelative = true, Timing = 2.0 }, new() { Damage = 0.051, IsRelative = true, Timing = 2.4 },
                    new() { Damage = 0.051, IsRelative = true, Timing = 2.8 }, new() { Damage = 0.051, IsRelative = true, Timing = 3.2 },
                    new() { Damage = 0.051, IsRelative = true, Timing = 3.6 }, new() { Damage = 0.051, IsRelative = true, Timing = 4.0 }
                ],
                DotHits = []
            }
        },
        {
            AbilityRarity.Rare,
            new()
            {
                BaseHits = [],
                SpecialHits =
                [
                    new() { Damage = 0.0522, IsRelative = true, Timing = 0.4 }, new() { Damage = 0.0522, IsRelative = true, Timing = 0.8 },
                    new() { Damage = 0.0522, IsRelative = true, Timing = 1.2 }, new() { Damage = 0.0522, IsRelative = true, Timing = 1.6 },
                    new() { Damage = 0.0522, IsRelative = true, Timing = 2.0 }, new() { Damage = 0.0522, IsRelative = true, Timing = 2.4 },
                    new() { Damage = 0.0522, IsRelative = true, Timing = 2.8 }, new() { Damage = 0.0522, IsRelative = true, Timing = 3.2 },
                    new() { Damage = 0.0522, IsRelative = true, Timing = 3.6 }, new() { Damage = 0.0522, IsRelative = true, Timing = 4.0 }
                ],
                DotHits = []
            }
        },
        {
            AbilityRarity.Epic,
            new()
            {
                BaseHits = [],
                SpecialHits =
                [
                    new() { Damage = 0.0537, IsRelative = true, Timing = 0.4 }, new() { Damage = 0.0537, IsRelative = true, Timing = 0.8 },
                    new() { Damage = 0.0537, IsRelative = true, Timing = 1.2 }, new() { Damage = 0.0537, IsRelative = true, Timing = 1.6 },
                    new() { Damage = 0.0537, IsRelative = true, Timing = 2.0 }, new() { Damage = 0.0537, IsRelative = true, Timing = 2.4 },
                    new() { Damage = 0.0537, IsRelative = true, Timing = 2.8 }, new() { Damage = 0.0537, IsRelative = true, Timing = 3.2 },
                    new() { Damage = 0.0537, IsRelative = true, Timing = 3.6 }, new() { Damage = 0.0537, IsRelative = true, Timing = 4.0 }
                ],
                DotHits = []
            }
        }
    };

    /// <inheritdoc />
    public string Name => "Lightning Bulwark";

    /// <inheritdoc />
    public double CastDuration => 0;

    /// <inheritdoc />
    public double ChannelDuration => 2;

    /// <inheritdoc />
    public string ImagePath => "lightning_bulwark.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Utility;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "immune to damage until first hit", Duration = 2 },
            new() { Description = "+120% movement speed if attack repelled", Duration = 4 },
            new() { Description = "AoE if attack repelled", Duration = 4 },
            new() { Description = "heavily slowed while channeling (-96%)", Duration = 2 }
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