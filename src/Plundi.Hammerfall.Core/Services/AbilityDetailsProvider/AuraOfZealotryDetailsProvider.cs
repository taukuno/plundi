using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDetailsProvider;

public class AuraOfZealotryDetailsProvider : IAbilityDetailsProvider
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
                    new() { Damage = 0.08m, IsRelative = true, Timing = 0m }, new() { Damage = 0.08m, IsRelative = true, Timing = 1m }, new() { Damage = 0.08m, IsRelative = true, Timing = 2m }, new() { Damage = 0.08m, IsRelative = true, Timing = 3m },
                    new() { Damage = 0.08m, IsRelative = true, Timing = 4m }, new() { Damage = 0.08m, IsRelative = true, Timing = 5m }, new() { Damage = 0.08m, IsRelative = true, Timing = 6m }, new() { Damage = 0.08m, IsRelative = true, Timing = 7m },
                    new() { Damage = 0.08m, IsRelative = true, Timing = 8m }
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
                    new() { Damage = 0.085m, IsRelative = true, Timing = 0m }, new() { Damage = 0.085m, IsRelative = true, Timing = 1m }, new() { Damage = 0.085m, IsRelative = true, Timing = 2m }, new() { Damage = 0.085m, IsRelative = true, Timing = 3m },
                    new() { Damage = 0.085m, IsRelative = true, Timing = 4m }, new() { Damage = 0.085m, IsRelative = true, Timing = 5m }, new() { Damage = 0.085m, IsRelative = true, Timing = 6m }, new() { Damage = 0.085m, IsRelative = true, Timing = 7m },
                    new() { Damage = 0.085m, IsRelative = true, Timing = 8m }
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
                    new() { Damage = 0.0859m, IsRelative = true, Timing = 0m }, new() { Damage = 0.0859m, IsRelative = true, Timing = 1m }, new() { Damage = 0.0859m, IsRelative = true, Timing = 2m },
                    new() { Damage = 0.0859m, IsRelative = true, Timing = 3m }, new() { Damage = 0.0859m, IsRelative = true, Timing = 4m }, new() { Damage = 0.0859m, IsRelative = true, Timing = 5m },
                    new() { Damage = 0.0859m, IsRelative = true, Timing = 6m }, new() { Damage = 0.0859m, IsRelative = true, Timing = 7m }, new() { Damage = 0.0859m, IsRelative = true, Timing = 8m }
                ],
                SpecialHits = [],
                DotHits = []
            }
        }
    };

    /// <inheritdoc />
    public bool CanHandleAbility(string abilityName)
    {
        return abilityName == "Aura of Zealotry";
    }

    /// <inheritdoc />
    public string GetDisplayName(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "Aura of Zealotry";
    }

    /// <inheritdoc />
    public decimal GetCastDuration(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return 0.5m;
    }

    /// <inheritdoc />
    public decimal GetChannelDuration(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return 0m;
    }

    /// <inheritdoc />
    public string GetImagePath(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "aura_of_zealotry.jpg";
    }

    /// <inheritdoc />
    public string GetWowheadLink(string abilityName)
    {
        const int spellId = 473810;
        return $"https://www.wowhead.com/spell={spellId}";
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
            new() { Description = "AoE (12y radius)", Duration = 8m },
            new() { Description = "+12% movement speed passively", Duration = 0m },
            new() { Description = "+12% movement speed passively for nearby party members (12y)", Duration = 0m },
            new() { Description = "+70% movement speed while inside AoE", Duration = 8m },
            new() { Description = $"increases melee damage (+{bonusMeleeDamage:P0}) while inside AoE", Duration = 8m },
            new() { Description = "increases melee range (12y) while inside AoE", Duration = 8m },
            new() {Description = "auras persist shortly after leaving AoE", Duration = 1.5m}
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

        var numberOfHitsToKeep = 9;
        if (simulationSettings is not null && simulationSettings.TryGetValue("duration_inside_aoe", out var durationInsideAoeSetting))
        {
            numberOfHitsToKeep = int.Parse(durationInsideAoeSetting[0].ToString()) + 1;
        }

        var damageProfile = _damageProfiles[abilityRarity];
        var adjustedBaseHits = damageProfile.BaseHits.Take(numberOfHitsToKeep).ToList();
        adjustedBaseHits.Add(new() { Damage = 0m, IsRelative = true, Timing = (adjustedBaseHits.LastOrDefault()?.Timing ?? 0) + 1.5m });

        var adjustedDamageProfile = damageProfile with
        {
            BaseHits = adjustedBaseHits
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
            { "duration_inside_aoe", ("How long do you stand in the AoE?", ["1s", "2s", "3s", "4s", "5s", "6s", "7s", "8s"], "5s") }
        };
    }
}
