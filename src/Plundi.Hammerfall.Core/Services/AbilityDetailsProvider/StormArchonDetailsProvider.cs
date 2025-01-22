using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDetailsProvider;

public class StormArchonDetailsProvider : IAbilityDetailsProvider
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
    public bool CanHandleAbility(string abilityName)
    {
        return abilityName == "Storm Archon";
    }

    /// <inheritdoc />
    public string GetDisplayName(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "Storm Archon";
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

        return 2.25m;
    }

    /// <inheritdoc />
    public string GetImagePath(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "storm_archon.jpg";
    }

    /// <inheritdoc />
    public bool CanBeCastedDuringGlobalCooldown(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        return false;
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
    public IEnumerable<AbilityEffect> GetEffects(string abilityName, AbilityRarity abilityRarity, int playerLevel, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return
        [
            new() { Description = "frontal barrage (50y, 5y radius)", Duration = 0m },
            new() { Description = "can be recast twice (last cast does decimal-damage)", Duration = 0m },
            new() { Description = "heavily slowed while channeling (-96%)", Duration = 2.25m }
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

        var numberOfHitsToKeep = 3;
        if (simulationSettings is not null && simulationSettings.TryGetValue("successful_hits", out var successfulHitsSetting))
        {
            numberOfHitsToKeep = int.Parse(successfulHitsSetting);
        }

        var damageProfile = _damageProfiles[abilityRarity];
        var adjustedDamageProfile = damageProfile with
        {
            BaseHits = damageProfile.BaseHits.Take(numberOfHitsToKeep).ToList()
        };

        return adjustedDamageProfile;;
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
            { "successful_hits", ("How many hits are you hitting?", ["1", "2", "3"], "3") }
        };
    }
}
