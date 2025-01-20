using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDetailsProvider;

public class RimeArrowDetailsProvider : IAbilityDetailsProvider
{
    private readonly Dictionary<AbilityRarity, decimal> _cooldowns = new()
    {
        { AbilityRarity.Common, 10m },
        { AbilityRarity.Uncommon, 8.5m },
        { AbilityRarity.Rare, 7m },
        { AbilityRarity.Epic, 5.5m }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        { AbilityRarity.Common, new() { BaseHits = [new() { Damage = 0.212m, IsRelative = true, Timing = 0m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [new() { Damage = 0.254m, IsRelative = true, Timing = 0m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [new() { Damage = 0.296m, IsRelative = true, Timing = 0m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [new() { Damage = 0.338m, IsRelative = true, Timing = 0m }], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public bool CanHandleAbility(string abilityName)
    {
        return abilityName == "Rime Arrow";
    }

    /// <inheritdoc />
    public string GetDisplayName(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "Rime Arrow";
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

        return "rime_arrow.jpg";
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
            new() { Description = "targeted projectile", Duration = 0m },
            new() { Description = "small AoE on hit", Duration = 0m }, new() { Description = "can't miss", Duration = 0m }
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
