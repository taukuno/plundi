namespace Plundi.Hammerfall.Core.Models;

public record DamageRange
{
    public double Min { get; init; }
    public double Max { get; init; }
    public double MinDps { get; init; }
    public double MaxDps { get; init; }
}