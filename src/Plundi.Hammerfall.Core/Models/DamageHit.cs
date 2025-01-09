namespace Plundi.Hammerfall.Core.Models;

public record DamageHit
{
    public double Damage { get; init; }
    public bool IsRelative { get; init; }
    public double Timing { get; init; }
}