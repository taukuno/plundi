namespace Plundi.Hammerfall.Core.Models;

public record KillReport
{
    public required int TargetHitPoints { get; init; }
    public required decimal TimeToKill { get; init; }
}
