namespace Plundi.Hammerfall.Core.Models;

public record DamageRange
{
    public decimal Min { get; init; }
    public decimal Max { get; init; }
    public decimal MinDps { get; init; }
    public decimal MaxDps { get; init; }
}
