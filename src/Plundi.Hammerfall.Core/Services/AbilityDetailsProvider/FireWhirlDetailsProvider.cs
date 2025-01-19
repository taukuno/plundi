using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDetailsProvider;

public class FireWhirlDetailsProvider : IAbilityDetailsProvider
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
                    new() { Damage = 0.104m, IsRelative = true, Timing = 0.2m }, new() { Damage = 0.104m, IsRelative = true, Timing = 0.4m },
                    new() { Damage = 0.104m, IsRelative = true, Timing = 0.6m }, new() { Damage = 0.104m, IsRelative = true, Timing = 0.8m },
                    new() { Damage = 0.104m, IsRelative = true, Timing = 1m }, new() { Damage = 0.104m, IsRelative = true, Timing = 1.2m },
                    new() { Damage = 0.104m, IsRelative = true, Timing = 1.4m }, new() { Damage = 0.104m, IsRelative = true, Timing = 1.6m },
                    new() { Damage = 0.104m, IsRelative = true, Timing = 1.8m }, new() { Damage = 0.104m, IsRelative = true, Timing = 2m },
                    new() { Damage = 0.104m, IsRelative = true, Timing = 2.2m }, new() { Damage = 0.104m, IsRelative = true, Timing = 2.4m },
                    new() { Damage = 0.104m, IsRelative = true, Timing = 2.6m }, new() { Damage = 0.104m, IsRelative = true, Timing = 2.8m },
                    new() { Damage = 0.104m, IsRelative = true, Timing = 3m }
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
                    new() { Damage = 0.109m, IsRelative = true, Timing = 0.2m }, new() { Damage = 0.109m, IsRelative = true, Timing = 0.4m },
                    new() { Damage = 0.109m, IsRelative = true, Timing = 0.6m }, new() { Damage = 0.109m, IsRelative = true, Timing = 0.8m },
                    new() { Damage = 0.109m, IsRelative = true, Timing = 1m }, new() { Damage = 0.109m, IsRelative = true, Timing = 1.2m },
                    new() { Damage = 0.109m, IsRelative = true, Timing = 1.4m }, new() { Damage = 0.109m, IsRelative = true, Timing = 1.6m },
                    new() { Damage = 0.109m, IsRelative = true, Timing = 1.8m }, new() { Damage = 0.109m, IsRelative = true, Timing = 2m },
                    new() { Damage = 0.109m, IsRelative = true, Timing = 2.2m }, new() { Damage = 0.109m, IsRelative = true, Timing = 2.4m },
                    new() { Damage = 0.109m, IsRelative = true, Timing = 2.6m }, new() { Damage = 0.109m, IsRelative = true, Timing = 2.8m },
                    new() { Damage = 0.109m, IsRelative = true, Timing = 3m }
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
                    new() { Damage = 0.115m, IsRelative = true, Timing = 0.2m }, new() { Damage = 0.115m, IsRelative = true, Timing = 0.4m },
                    new() { Damage = 0.115m, IsRelative = true, Timing = 0.6m }, new() { Damage = 0.115m, IsRelative = true, Timing = 0.8m },
                    new() { Damage = 0.115m, IsRelative = true, Timing = 1m }, new() { Damage = 0.115m, IsRelative = true, Timing = 1.2m },
                    new() { Damage = 0.115m, IsRelative = true, Timing = 1.4m }, new() { Damage = 0.115m, IsRelative = true, Timing = 1.6m },
                    new() { Damage = 0.115m, IsRelative = true, Timing = 1.8m }, new() { Damage = 0.115m, IsRelative = true, Timing = 2m },
                    new() { Damage = 0.115m, IsRelative = true, Timing = 2.2m }, new() { Damage = 0.115m, IsRelative = true, Timing = 2.4m },
                    new() { Damage = 0.115m, IsRelative = true, Timing = 2.6m }, new() { Damage = 0.115m, IsRelative = true, Timing = 2.8m },
                    new() { Damage = 0.115m, IsRelative = true, Timing = 3m }
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
                    new() { Damage = 0.12m, IsRelative = true, Timing = 0.2m }, new() { Damage = 0.12m, IsRelative = true, Timing = 0.4m },
                    new() { Damage = 0.12m, IsRelative = true, Timing = 0.6m }, new() { Damage = 0.12m, IsRelative = true, Timing = 0.8m },
                    new() { Damage = 0.12m, IsRelative = true, Timing = 1m }, new() { Damage = 0.12m, IsRelative = true, Timing = 1.2m },
                    new() { Damage = 0.12m, IsRelative = true, Timing = 1.4m }, new() { Damage = 0.12m, IsRelative = true, Timing = 1.6m },
                    new() { Damage = 0.12m, IsRelative = true, Timing = 1.8m }, new() { Damage = 0.12m, IsRelative = true, Timing = 2m },
                    new() { Damage = 0.12m, IsRelative = true, Timing = 2.2m }, new() { Damage = 0.12m, IsRelative = true, Timing = 2.4m },
                    new() { Damage = 0.12m, IsRelative = true, Timing = 2.6m }, new() { Damage = 0.12m, IsRelative = true, Timing = 2.8m },
                    new() { Damage = 0.12m, IsRelative = true, Timing = 3m }
                ],
                SpecialHits = [],
                DotHits = []
            }
        }
    };

    /// <inheritdoc />
    public bool CanHandleAbility(string abilityName)
    {
        return abilityName == "Fire Whirl";
    }

    /// <inheritdoc />
    public string GetDisplayName(string ability)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return "Fire Whirl";
    }

    /// <inheritdoc />
    public decimal GetCastDuration(string ability)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return 1m;
    }

    /// <inheritdoc />
    public decimal GetChannelDuration(string ability)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return 3;
    }

    /// <inheritdoc />
    public string GetImagePath(string ability)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return "fire_whirl.jpg";
    }

    /// <inheritdoc />
    public AbilityType GetAbilityType(string ability)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        return AbilityType.Damage;
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
            new() { Description = "AoE", Duration = 0m },
            new() { Description = "+70% movement speed", Duration = 3m }
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
