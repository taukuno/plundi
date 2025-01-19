namespace Plundi.Hammerfall.Core.Models;

public record DamageHit
{
    public decimal Damage { get; init; }
    public bool IsRelative { get; init; }
    public decimal Timing { get; init; }
}
