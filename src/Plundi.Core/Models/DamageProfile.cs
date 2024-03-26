namespace Plundi.Core.Models;

public record DamageProfile
{
    public required List<(double RelativeDamage, double Timing)> DefaultHits { get; init; }
    public required List<(double RelativeDamage, double Timing)> SpecialHits { get; init; }
    public required List<(double RelativeDamage, double Timing)> DotHits { get; init; }
}