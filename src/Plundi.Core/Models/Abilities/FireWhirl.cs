namespace Plundi.Core.Models.Abilities;

public class FireWhirl : IAbility
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
        {
            AbilityRarity.Common,
            new()
            {
                DefaultHits = [0.0575, 0.0575, 0.0575, 0.0575, 0.0575, 0.0575, 0.0575, 0.0575, 0.0575, 0.0575, 0.0575, 0.0575, 0.0575, 0.0575, 0.0575],
                SpecialHits = [],
                DotHits = []
            }
        },
        {
            AbilityRarity.Uncommon,
            new()
            {
                DefaultHits = [0.0605, 0.0605, 0.0605, 0.0605, 0.0605, 0.0605, 0.0605, 0.0605, 0.0605, 0.0605, 0.0605, 0.0605, 0.0605, 0.0605, 0.0605],
                SpecialHits = [],
                DotHits = []
            }
        },
        {
            AbilityRarity.Rare,
            new()
            {
                DefaultHits = [0.0638, 0.0638, 0.0638, 0.0638, 0.0638, 0.0638, 0.0638, 0.0638, 0.0638, 0.0638, 0.0638, 0.0638, 0.0638, 0.0638, 0.0638],
                SpecialHits = [],
                DotHits = []
            }
        },
        {
            AbilityRarity.Epic,
            new()
            {
                DefaultHits = [0.0665, 0.0665, 0.0665, 0.0665, 0.0665, 0.0665, 0.0665, 0.0665, 0.0665, 0.0665, 0.0665, 0.0665, 0.0665, 0.0665, 0.0665],
                SpecialHits = [],
                DotHits = []
            }
        }
    };

    /// <inheritdoc />
    public string Name => "Fire Whirl";

    /// <inheritdoc />
    public double CastDuration => 1;

    /// <inheritdoc />
    public double ChannelDuration => 3;

    /// <inheritdoc />
    public string ImagePath => "fire_whirl.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<(string Effect, double Duration)> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return [("AoE", 0), ("+70% movement speed", 3)];
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