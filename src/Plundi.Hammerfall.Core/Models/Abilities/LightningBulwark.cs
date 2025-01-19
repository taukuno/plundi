namespace Plundi.Hammerfall.Core.Models.Abilities;

public class LightningBulwark : IAbility
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
                BaseHits = [],
                SpecialHits =
                [
                    new() { Damage = 0.049m, IsRelative = true, Timing = 0.4m }, new() { Damage = 0.049m, IsRelative = true, Timing = 0.8m },
                    new() { Damage = 0.049m, IsRelative = true, Timing = 1.2m }, new() { Damage = 0.049m, IsRelative = true, Timing = 1.6m },
                    new() { Damage = 0.049m, IsRelative = true, Timing = 2.0m }, new() { Damage = 0.049m, IsRelative = true, Timing = 2.4m },
                    new() { Damage = 0.049m, IsRelative = true, Timing = 2.8m }, new() { Damage = 0.049m, IsRelative = true, Timing = 3.2m },
                    new() { Damage = 0.049m, IsRelative = true, Timing = 3.6m }, new() { Damage = 0.049m, IsRelative = true, Timing = 4.0m }
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
                    new() { Damage = 0.051m, IsRelative = true, Timing = 0.4m }, new() { Damage = 0.051m, IsRelative = true, Timing = 0.8m },
                    new() { Damage = 0.051m, IsRelative = true, Timing = 1.2m }, new() { Damage = 0.051m, IsRelative = true, Timing = 1.6m },
                    new() { Damage = 0.051m, IsRelative = true, Timing = 2.0m }, new() { Damage = 0.051m, IsRelative = true, Timing = 2.4m },
                    new() { Damage = 0.051m, IsRelative = true, Timing = 2.8m }, new() { Damage = 0.051m, IsRelative = true, Timing = 3.2m },
                    new() { Damage = 0.051m, IsRelative = true, Timing = 3.6m }, new() { Damage = 0.051m, IsRelative = true, Timing = 4.0m }
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
                    new() { Damage = 0.0522m, IsRelative = true, Timing = 0.4m }, new() { Damage = 0.0522m, IsRelative = true, Timing = 0.8m },
                    new() { Damage = 0.0522m, IsRelative = true, Timing = 1.2m }, new() { Damage = 0.0522m, IsRelative = true, Timing = 1.6m },
                    new() { Damage = 0.0522m, IsRelative = true, Timing = 2.0m }, new() { Damage = 0.0522m, IsRelative = true, Timing = 2.4m },
                    new() { Damage = 0.0522m, IsRelative = true, Timing = 2.8m }, new() { Damage = 0.0522m, IsRelative = true, Timing = 3.2m },
                    new() { Damage = 0.0522m, IsRelative = true, Timing = 3.6m }, new() { Damage = 0.0522m, IsRelative = true, Timing = 4.0m }
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
                    new() { Damage = 0.0537m, IsRelative = true, Timing = 0.4m }, new() { Damage = 0.0537m, IsRelative = true, Timing = 0.8m },
                    new() { Damage = 0.0537m, IsRelative = true, Timing = 1.2m }, new() { Damage = 0.0537m, IsRelative = true, Timing = 1.6m },
                    new() { Damage = 0.0537m, IsRelative = true, Timing = 2.0m }, new() { Damage = 0.0537m, IsRelative = true, Timing = 2.4m },
                    new() { Damage = 0.0537m, IsRelative = true, Timing = 2.8m }, new() { Damage = 0.0537m, IsRelative = true, Timing = 3.2m },
                    new() { Damage = 0.0537m, IsRelative = true, Timing = 3.6m }, new() { Damage = 0.0537m, IsRelative = true, Timing = 4.0m }
                ],
                DotHits = []
            }
        }
    };

    /// <inheritdoc />
    public string Name => "Lightning Bulwark";

    /// <inheritdoc />
    public decimal CastDuration => 0m;

    /// <inheritdoc />
    public decimal ChannelDuration => 2;

    /// <inheritdoc />
    public string ImagePath => "lightning_bulwark.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Utility;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "immune to damage until first hit", Duration = 2m },
            new() { Description = "+120% movement speed if attack repelled", Duration = 4m },
            new() { Description = "AoE if attack repelled", Duration = 4m },
            new() { Description = "heavily slowed while channeling (-96%)", Duration = 2m }
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
