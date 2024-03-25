namespace Plundi.Core.Models;

public record DamageReport
{
    public required List<double> DefaultHits { get; init; }
    public required List<double> SpecialHits { get; init; }
    public required List<double> DotHits { get; init; }
    public required (double Min, double Max) DamageRange { get; init; }
    public required (double Min, double Max) HitDamageRange { get; init; }
    public required (double Min, double Max) SpecialDamageRange { get; init; }
    public required (double Min, double Max) DotDamageRange { get; init; }
}