using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services;

public interface IAbilityDetailsProvider
{
    bool CanHandleAbility(string abilityName);

    string GetDisplayName(string abilityName, Dictionary<string, string>? simulationSettings = null);

    decimal GetCastDuration(string abilityName, Dictionary<string, string>? simulationSettings = null);

    decimal GetChannelDuration(string abilityName, Dictionary<string, string>? simulationSettings = null);

    string GetImagePath(string abilityName, Dictionary<string, string>? simulationSettings = null);

    bool CanBeCastedDuringGlobalCooldown(string abilityName, Dictionary<string, string>? simulationSettings = null);

    AbilityType GetAbilityType(string abilityName, Dictionary<string, string>? simulationSettings = null);

    IEnumerable<AbilityEffect> GetEffects(string abilityName, AbilityRarity abilityRarity, int playerLevel, Dictionary<string, string>? simulationSettings = null);

    decimal GetCooldown(string abilityName, AbilityRarity abilityRarity, int playerLevel, Dictionary<string, string>? simulationSettings = null);

    DamageProfile GetDamageProfile(string abilityName, AbilityRarity abilityRarity, int playerLevel, Dictionary<string, string>? simulationSettings = null);

    Dictionary<string, (string Description, List<string> PossibleValues, string DefaultValue)> GetPossibleSimulationSettings(string abilityName);
}
