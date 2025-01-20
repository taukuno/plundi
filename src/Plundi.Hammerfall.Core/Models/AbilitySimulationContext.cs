namespace Plundi.Hammerfall.Core.Models;

public class AbilitySimulationContext
{
    public required string AbilityName { get; set; }
    public required AbilityRarity AbilityRarity { get; set; }
    public required int PlayerLevel { get; set; }

    public required decimal AdjustedCastDuration { get; set; }
    public required decimal AdjustedChannelDuration { get; set; }
    public required DamageProfile AdjustedDamageProfile { get; set; }

    public decimal SimulationStartedAt { get; set; } = -1;
    public decimal SimulationFinishedAt { get; set; } = -1;

    public decimal CastStartedAt { get; set; } = -1;
    public decimal CastFinishedAt { get; set; } = -1;

    public decimal ChannelStartedAt { get; set; } = -1;
    public decimal ChannelFinishedAt { get; set; } = -1;

    public bool IsFinished { get; set; }
    public decimal NextHandlingNeededAt { get; set; } = -1;
    public object? Payload { get; set; }
}
