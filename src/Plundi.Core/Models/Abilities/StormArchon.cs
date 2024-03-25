namespace Plundi.Core.Models.Abilities;

public class StormArchon : IAbility
{
    private readonly Dictionary<AbilityRarity, double> _cooldowns = new()
    {
        { AbilityRarity.Common, 20 },
        { AbilityRarity.Uncommon, 18 },
        { AbilityRarity.Rare, 16 },
        { AbilityRarity.Epic, 14 }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        { AbilityRarity.Common, new() { DefaultHits = [0.1575, 0.1575, 0.315], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { DefaultHits = [0.1675, 0.1675, 0.335], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { DefaultHits = [0.175, 0.175, 0.35], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { DefaultHits = [0.1825, 0.1825, 0.365], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public string Name => "Storm Archon";

    /// <inheritdoc />
    public double CastDuration => 0.75;

    /// <inheritdoc />
    public double ChannelDuration => 0;

    /// <inheritdoc />
    public string ImagePath => "storm_archon.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "frontal barrage", Duration = 0 },
            new() { Description = "can be recast twice (last cast does double-damage)", Duration = 0 }
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