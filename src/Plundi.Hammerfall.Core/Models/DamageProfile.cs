namespace Plundi.Hammerfall.Core.Models;

public record DamageProfile
{
    public required List<DamageHit> BaseHits { get; init; }
    public required List<DamageHit> SpecialHits { get; init; }
    public required List<DamageHit> DotHits { get; init; }
}