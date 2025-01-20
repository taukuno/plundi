using Plundi.Hammerfall.Core.Services;

namespace Plundi.Hammerfall.Core.Models;

public class LoadoutSimulationContext
{
    public required Loadout Loadout { get; set; }

    public required List<RarifiedAbility> AvailableAbilities { get; set; }

    public required decimal SimulationDuration { get; set; }

    public required int CharacterLevel { get; set; }

    public Dictionary<string, Dictionary<string, string>> AbilitySettings { get; set; } = new();

    public decimal CurrentTime { get; set; }

    public List<(decimal Timestamp, string AbilityName, AbilityRarity AbilityRarity, int CharacterLevel, string Event, decimal Damage)> Events { get; set; } = [];

    public bool IsFinished { get; set; }

    public decimal RemainingGlobalCooldown { get; set; }

    public bool IsGlobalOnCooldown { get; set; }

    public bool IsCurrentlyCasting { get; set; }

    public bool IsCurrentlyChanneling { get; set; }

    public List<AbilitySimulationContext> AbilitySimulationContexts { get; set; } = [];

    public List<(string AbilityName, decimal RemainingCooldown)> AbilitiesOnCooldown { get; set; } = [];

    public decimal NextGlobalHandlingNeededAt { get; set; }
    }
