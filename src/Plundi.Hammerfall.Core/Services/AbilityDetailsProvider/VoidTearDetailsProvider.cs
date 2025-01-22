using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDetailsProvider;

public class VoidTearDetailsProvider : IAbilityDetailsProvider
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
        { AbilityRarity.Common, new() { BaseHits = [], SpecialHits = [new() {Damage = 0.45m, IsRelative = true, Timing = 1m}], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [], SpecialHits = [new() {Damage = 0.4725m, IsRelative = true, Timing = 1m}], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [], SpecialHits = [new() {Damage = 0.495m, IsRelative = true, Timing = 1m}], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [], SpecialHits = [new() {Damage = 0.5175m, IsRelative = true, Timing = 1m}], DotHits = [] } }
    };

    /// <inheritdoc />
    public bool CanHandleAbility(string abilityName)
    {
        return abilityName == "Void Tear";
    }

    /// <inheritdoc />
    public string GetDisplayName(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "Void Tear";
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

        return 0m;
    }

    /// <inheritdoc />
    public string GetImagePath(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "void_tear.jpg";
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
            new() { Description = "set's a portal", Duration = 10m },
            new() { Description = "recast to port back to portal", Duration = 0m },
            new() { Description = "AoE (10y radius) around portal on return", Duration = 0m },
            new() { Description = "slows on hit (-40%)", Duration = 3m }
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
