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
    public string GetDisplayName(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "Lightning Bulwark";
    }

    /// <inheritdoc />
    public decimal GetCastDuration(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return 0m;
    }

    /// <inheritdoc />
    public decimal GetChannelDuration(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the abilityName '{abilityName}'.", nameof(abilityName));
        }

        return 2;
    }

    /// <inheritdoc />
    public string GetImagePath(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "lightning_bulwark.jpg";
    }

    /// <inheritdoc />
    public bool CanBeCastedDuringGlobalCooldown(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        return true;
    }

    /// <inheritdoc />
    public AbilityType GetAbilityType(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return AbilityType.Utility;
    }

    /// <inheritdoc />
    public IEnumerable<AbilityEffect> GetEffects(string abilityName, AbilityRarity abilityRarity, int playerLevel, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return
        [
            new() { Description = "repels one hit", Duration = 2m },
            new() { Description = "triggers AoE (6y radius) on repel", Duration = 4m },
            new() { Description = "+120% movement speed on repel", Duration = 4m },
            new() { Description = "castable during GCD", Duration = 0m },
            new() { Description = "heavily slowed while channeling (-96%)", Duration = 2m }

        ];
    }

    /// <inheritdoc />
    public decimal GetCooldown(string abilityName, AbilityRarity abilityRarity, int playerLevel, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return _cooldowns[abilityRarity];
    }

    /// <inheritdoc />
    public DamageProfile GetDamageProfile(string abilityName, AbilityRarity abilityRarity, int playerLevel, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var numberOfHitsToKeep = 10;
        if (simulationSettings is not null && simulationSettings.TryGetValue("successful_hits", out var successfulHitsSetting))
        {
            numberOfHitsToKeep = int.Parse(successfulHitsSetting);
        }

        var damageProfile = _damageProfiles[abilityRarity];
        var adjustedDamageProfile = damageProfile with
        {
            SpecialHits = damageProfile.SpecialHits.Take(numberOfHitsToKeep).ToList()
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
            { "successful_hits", ("How many hits are you hitting?", ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10"], "7") }
        };
    }
}
