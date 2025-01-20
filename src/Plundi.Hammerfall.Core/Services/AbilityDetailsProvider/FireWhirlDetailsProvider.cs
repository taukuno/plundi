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
    public string GetDisplayName(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "Fire Whirl";
    }

    /// <inheritdoc />
    public decimal GetCastDuration(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return 1m;
    }

    /// <inheritdoc />
    public decimal GetChannelDuration(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the abilityName '{abilityName}'.", nameof(abilityName));
        }

        return 3;
    }

    /// <inheritdoc />
    public string GetImagePath(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "fire_whirl.jpg";
    }

    /// <inheritdoc />
    public AbilityType GetAbilityType(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return AbilityType.Damage;
    }

    /// <inheritdoc />
    public IEnumerable<AbilityEffect> GetEffects(string abilityName, AbilityRarity abilityRarity, int characterLevel, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return
        [
            new() { Description = "AoE", Duration = 0m },
            new() { Description = "+70% movement speed", Duration = 3m }
        ];
    }

    /// <inheritdoc />
    public decimal GetCooldown(string abilityName, AbilityRarity abilityRarity, int characterLevel, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return _cooldowns[abilityRarity];
    }

    /// <inheritdoc />
    public DamageProfile GetDamageProfile(string abilityName, AbilityRarity abilityRarity, int characterLevel, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var numberOfHitsToKeep = 15;
        if (simulationSettings is not null && simulationSettings.TryGetValue("successful_hits", out var successfulHitsSetting))
        {
            numberOfHitsToKeep = int.Parse(successfulHitsSetting);
        }

        var damageProfile = _damageProfiles[abilityRarity];
        var adjustedDamageProfile = damageProfile with
        {
            BaseHits = damageProfile.BaseHits.Take(numberOfHitsToKeep).ToList()
        };

        return adjustedDamageProfile;
    }

    /// <inheritdoc />
    public Dictionary<string, (string Description, List<string> PossibleValues, string DefaultValue)> GetPossibleSimulationSettings(string abilityName)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return new()
        {
            { "successful_hits", ("How many hits are you hitting?", ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15"], "10") }
        };
    }
}
