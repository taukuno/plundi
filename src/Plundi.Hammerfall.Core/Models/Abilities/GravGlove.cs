namespace Plundi.Hammerfall.Core.Models.Abilities;

public class GravGlove : IAbility
{
    private readonly Dictionary<AbilityRarity, double> _cooldowns = new()
    {
        { AbilityRarity.Common, 18 },
        { AbilityRarity.Uncommon, 16 },
        { AbilityRarity.Rare, 14 },
        { AbilityRarity.Epic, 12 }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        { AbilityRarity.Common, new() { BaseHits = [], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public string Name => "G.R.A.V. Glove";

    /// <inheritdoc />
    public double CastDuration => 0;

    /// <inheritdoc />
    public double ChannelDuration => 0;

    /// <inheritdoc />
    public string ImagePath => "grav_glove.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Utility;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "set's a portal", Duration = 10 },
            new() { Description = "recast to port back to portal", Duration = 0 }
        ];
    }

    /// <inheritdoc />
    public double GetCooldown(int characterLevel, AbilityRarity abilityRarity)
    {
        return _cooldowns[abilityRarity];
    }

    /// <inheritdoc />
    public DamageProfile GetDamageProfile(int characterLevel, AbilityRarity abilityRarity)
    {
        return _damageProfiles[abilityRarity];
    }
}