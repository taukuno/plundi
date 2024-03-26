namespace Plundi.Core.Models;

public record DamageReport
{
    public required List<(double Damage, double Timing)> DefaultHits { get; init; }
    public required List<(double Damage, double Timing)> SpecialHits { get; init; }
    public required List<(double Damage, double Timing)> DotHits { get; init; }
    public required (double Min, double Max, double MinDps, double MaxDps) DamageRange { get; init; }
    public required (double Min, double Max, double MinDps, double MaxDps) DefaultDamageRange { get; init; }
    public required (double Min, double Max, double MinDps, double MaxDps) SpecialDamageRange { get; init; }
    public required (double Min, double Max, double MinDps, double MaxDps) DotDamageRange { get; init; }
}