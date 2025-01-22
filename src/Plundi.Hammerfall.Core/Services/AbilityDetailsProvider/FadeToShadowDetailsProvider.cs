using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDetailsProvider;

public class FadeToShadowDetailsProvider : IAbilityDetailsProvider
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
        { AbilityRarity.Common, new() { BaseHits = [], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public bool CanHandleAbility(string abilityName)
    {
        return abilityName == "Fade to Shadow";
    }

    /// <inheritdoc />
    public string GetDisplayName(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "Fade to Shadow";
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

        return "fade_to_shadow.jpg";
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
            new() { Description = "blinks forward (25y)", Duration = 0m },
            new() { Description = "stealth after blink", Duration = 4m }
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
