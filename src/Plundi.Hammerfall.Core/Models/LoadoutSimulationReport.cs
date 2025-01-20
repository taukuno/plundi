namespace Plundi.Hammerfall.Core.Models;

public record LoadoutSimulationReport
{
    public required Loadout Loadout { get; init; }
    public required decimal SimulationDuration { get; init; }
    public required int CharacterLevel { get; set; }

    public required decimal TotalDamage { get; init; }

    public required List<(decimal Timestamp, string AbilityName, AbilityRarity AbilityRarity, int CharacterLevel, string Event, decimal Damage)> Events { get; init; }
}
