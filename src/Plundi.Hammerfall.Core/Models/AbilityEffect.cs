namespace Plundi.Hammerfall.Core.Models;

public record AbilityEffect
{
    public required string Description { get; init; }
    public required decimal Duration { get; init; }
}
