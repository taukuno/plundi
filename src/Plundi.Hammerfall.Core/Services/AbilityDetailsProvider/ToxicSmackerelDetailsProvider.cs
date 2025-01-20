using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDetailsProvider;

public class ToxicSmackerelDetailsProvider : IAbilityDetailsProvider
{
    private readonly Dictionary<AbilityRarity, decimal> _cooldowns = new()
    {
        { AbilityRarity.Common, 7m },
        { AbilityRarity.Uncommon, 6.5m },
        { AbilityRarity.Rare, 6m },
        { AbilityRarity.Epic, 5.5m }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        {
            AbilityRarity.Common,
            new()
            {
                BaseHits = [new() { Damage = 0.161m, IsRelative = true, Timing = 0m }],
                SpecialHits = [new() { Damage = 0.254m, IsRelative = true, Timing = 0m }],
                DotHits =
                [
                    new() { Damage = 0.0466m, IsRelative = true, Timing = 1m }, new() { Damage = 0.0466m, IsRelative = true, Timing = 2m },
                    new() { Damage = 0.0466m, IsRelative = true, Timing = 3m }, new() { Damage = 0.0466m, IsRelative = true, Timing = 4m },
                    new() { Damage = 0.0466m, IsRelative = true, Timing = 5m }, new() { Damage = 0.0466m, IsRelative = true, Timing = 6m },
                    new() { Damage = 0.0466m, IsRelative = true, Timing = 7m }, new() { Damage = 0.0466m, IsRelative = true, Timing = 8m }
                ]
            }
        },
        {
            AbilityRarity.Uncommon,
            new()
            {
                BaseHits = [new() { Damage = 0.169m, IsRelative = true, Timing = 0m }],
                SpecialHits = [new() { Damage = 0.268m, IsRelative = true, Timing = 0m }],
                DotHits =
                [
                    new() { Damage = 0.0492m, IsRelative = true, Timing = 1m }, new() { Damage = 0.0492m, IsRelative = true, Timing = 2m },
                    new() { Damage = 0.0492m, IsRelative = true, Timing = 3m }, new() { Damage = 0.0492m, IsRelative = true, Timing = 4m },
                    new() { Damage = 0.0492m, IsRelative = true, Timing = 5m }, new() { Damage = 0.0492m, IsRelative = true, Timing = 6m },
                    new() { Damage = 0.0492m, IsRelative = true, Timing = 7m }, new() { Damage = 0.0492m, IsRelative = true, Timing = 8m }
                ]
            }
        },
        {
            AbilityRarity.Rare,
            new()
            {
                BaseHits = [new() { Damage = 0.178m, IsRelative = true, Timing = 0m }],
                SpecialHits = [new() { Damage = 0.282m, IsRelative = true, Timing = 0m }],
                DotHits =
                [
                    new() { Damage = 0.05188m, IsRelative = true, Timing = 1m }, new() { Damage = 0.05188m, IsRelative = true, Timing = 2m },
                    new() { Damage = 0.05188m, IsRelative = true, Timing = 3m }, new() { Damage = 0.05188m, IsRelative = true, Timing = 4m },
                    new() { Damage = 0.05188m, IsRelative = true, Timing = 5m }, new() { Damage = 0.05188m, IsRelative = true, Timing = 6m },
                    new() { Damage = 0.05188m, IsRelative = true, Timing = 7m }, new() { Damage = 0.05188m, IsRelative = true, Timing = 8m }
                ]
            }
        },
        {
            AbilityRarity.Epic,
            new()
            {
                BaseHits = [new() { Damage = 0.1863m, IsRelative = true, Timing = 0m }],
                SpecialHits = [new() { Damage = 0.2962m, IsRelative = true, Timing = 0m }],
                DotHits =
                [
                    new() { Damage = 0.0543m, IsRelative = true, Timing = 1m }, new() { Damage = 0.0543m, IsRelative = true, Timing = 2m },
                    new() { Damage = 0.0543m, IsRelative = true, Timing = 3m }, new() { Damage = 0.0543m, IsRelative = true, Timing = 4m },
                    new() { Damage = 0.0543m, IsRelative = true, Timing = 5m }, new() { Damage = 0.0543m, IsRelative = true, Timing = 6m },
                    new() { Damage = 0.0543m, IsRelative = true, Timing = 7m }, new() { Damage = 0.0543m, IsRelative = true, Timing = 8m }
                ]
            }
        }
    };

    /// <inheritdoc />
    public bool CanHandleAbility(string abilityName)
    {
        return abilityName == "Toxic Smackerel";
    }

    /// <inheritdoc />
    public string GetDisplayName(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "Toxic Smackerel";
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
            throw new ArgumentException($"Can't handle the abilityName '{abilityName}'.", nameof(abilityName));
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

        return "toxic_smackerel.jpg";
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
    public IEnumerable<AbilityEffect> GetEffects(string abilityName, AbilityRarity abilityRarity, int characterLevel, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var bonusDamage = Math.Round(_damageProfiles[abilityRarity].SpecialHits[0].Damage * CharacterStatsProvider.GetAttackPower(characterLevel), 1);

        return
        [
            new() { Description = "frontal cone", Duration = 0m },
            new() { Description = "applies poison dot", Duration = 8m },
            new() { Description = $"does bonus damage on already poisoned targets ({bonusDamage:N1})", Duration = 8m }
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

        return _damageProfiles[abilityRarity];
    }

    /// <inheritdoc />
    public Dictionary<string, (string Description, List<string> PossibleValues, string DefaultValue)> GetPossibleSimulationSettings(string abilityName)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return [];
    }
}
