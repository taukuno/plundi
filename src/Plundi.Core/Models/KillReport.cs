namespace Plundi.Core.Models;

public record KillReport
{
    public required int TargetHitPoints { get; init; }
    public required double TimeToKill { get; init; }
}