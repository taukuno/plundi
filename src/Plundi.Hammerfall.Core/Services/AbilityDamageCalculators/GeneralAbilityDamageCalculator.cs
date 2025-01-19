namespace Plundi.Hammerfall.Core.Services.AbilityDamageCalculators;

public class GeneralAbilityDamageCalculator : BaseAbilityDamageCalculator
{
    private readonly HashSet<string> _handledAbilities =
    [
        "Aura of Zealotry",
        "Celestial Barrage",
        "Earthbreaker",
        "Fire Whirl",
        "Mana Sphere",
        "Rime Arrow",
        "Searing Axe",
        "Slicing Winds",
        "Star Bomb",
        "Storm Archon",
        "Call Galefeather",
        "Explosive Caltrops",
        "Fade to Shadow",
        "Faeform",
        "G.R.A.V. Glove",
        "Hunter's Chains",
        "Lightning Bulwark",
        "Quaking Leap",
        "Repel",
        "Snowdrift",
        "Steel Traps",
        "Windstorm"
    ];

    /// <inheritdoc />
    public GeneralAbilityDamageCalculator(IEnumerable<IAbilityDetailsProvider> abilityDetailsProviders) : base(abilityDetailsProviders) { }

    /// <inheritdoc />
    public override bool CanHandleAbility(string ability)
    {
        return _handledAbilities.Contains(ability);
    }
}
