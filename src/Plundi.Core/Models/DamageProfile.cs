namespace Plundi.Core.Models;

public record DamageProfile
{
    public required List<double> DefaultHits { get; init; }
    public required List<double> SpecialHits { get; init; }
    public required List<double> DotHits { get; init; }
}