namespace Plundi.Hammerfall.Core.Models;

public record DamageReport
{
    public required DamageRange TotalDamageRange { get; init; }
    public required DamageRange BaseDamageRange { get; init; }
    public required DamageRange SpecialDamageRange { get; init; }
    public required DamageRange DotDamageRange { get; init; }
    public required List<DamageHit> BaseHits { get; init; }
    public required List<DamageHit> SpecialHits { get; init; }
    public required List<DamageHit> DotHits { get; init; }
}