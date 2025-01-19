using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDetailsProvider;

public class LightningBulwarkDetailsProvider : IAbilityDetailsProvider
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
    public bool CanHandleAbility(string abilityName)
    {
        return abilityName == "Lightning Bulwark";
    }

    /// <inheritdoc />
    public string GetDisplayName(string ability)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return "Lightning Bulwark";
    }

    /// <inheritdoc />
    public decimal GetCastDuration(string ability)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return 0m;
    }

    /// <inheritdoc />
    public decimal GetChannelDuration(string ability)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return 2;
    }

    /// <inheritdoc />
    public string GetImagePath(string ability)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return "lightning_bulwark.jpg";
    }

    /// <inheritdoc />
    public AbilityType GetAbilityType(string ability)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return AbilityType.Utility;
    }

    /// <inheritdoc />
    public IEnumerable<AbilityEffect> GetEffects(string ability, int characterLevel, AbilityRarity abilityRarity)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return
        [
            new() { Description = "immune to damage until first hit", Duration = 2m },
            new() { Description = "+120% movement speed if attack repelled", Duration = 4m },
            new() { Description = "AoE if attack repelled", Duration = 4m },
            new() { Description = "heavily slowed while channeling (-96%)", Duration = 2m }
        ];
    }

    /// <inheritdoc />
    public decimal GetCooldown(string ability, int characterLevel, AbilityRarity abilityRarity)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return _cooldowns[abilityRarity];
    }

    /// <inheritdoc />
    public DamageProfile GetDamageProfile(string ability, int characterLevel, AbilityRarity abilityRarity)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return _damageProfiles[abilityRarity];
    }
}
