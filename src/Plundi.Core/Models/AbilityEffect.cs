namespace Plundi.Core.Models;

public record AbilityEffect
{
    public required string Description { get; init; }
    public required double Duration { get; init; }
}