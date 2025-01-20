namespace Plundi.Hammerfall.Core.Models;

public record Loadout
{
    public required List<RarifiedAbility> Abilities { get; init; }
}
