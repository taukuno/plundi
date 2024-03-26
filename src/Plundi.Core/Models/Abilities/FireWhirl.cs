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
                DefaultHits =
                [
                    (0.0575, 0.2), (0.0575, 0.4), (0.0575, 0.6), (0.0575, 0.8), (0.0575, 1), (0.0575, 1.2),
                    (0.0575, 1.4), (0.0575, 1.6), (0.0575, 1.8), (0.0575, 2), (0.0575, 2.2), (0.0575, 2.4),
                    (0.0575, 2.6), (0.0575, 2.8), (0.0575, 3)
                ],
                SpecialHits = [],
                DotHits = []
            }
        },
        {
            AbilityRarity.Uncommon,
            new()
            {
                DefaultHits =
                [
                    (0.0605, 0.2), (0.0605, 0.4), (0.0605, 0.6), (0.0605, 0.8), (0.0605, 1), (0.0605, 1.2),
                    (0.0605, 1.4), (0.0605, 1.6), (0.0605, 1.8), (0.0605, 2), (0.0605, 2.2), (0.0605, 2.4),
                    (0.0605, 2.6), (0.0605, 2.8), (0.0605, 3)
                ],
                SpecialHits = [],
                DotHits = []
            }
        },
        {
            AbilityRarity.Rare,
            new()
            {
                DefaultHits =
                [
                    (0.0638, 0.2), (0.0638, 0.4), (0.0638, 0.6), (0.0638, 0.8), (0.0638, 1), (0.0638, 1.2),
                    (0.0638, 1.4), (0.0638, 1.6), (0.0638, 1.8), (0.0638, 2), (0.0638, 2.2), (0.0638, 2.4),
                    (0.0638, 2.6), (0.0638, 2.8), (0.0638, 3)
                ],
                SpecialHits = [],
                DotHits = []
            }
        },
        {
            AbilityRarity.Epic,
            new()
            {
                DefaultHits =
                [
                    (0.0665, 0.2), (0.0665, 0.4), (0.0665, 0.6), (0.0665, 0.8), (0.0665, 1), (0.0665, 1.2),
                    (0.0665, 1.4), (0.0665, 1.6), (0.0665, 1.8), (0.0665, 2), (0.0665, 2.2), (0.0665, 2.4),
                    (0.0665, 2.6), (0.0665, 2.8), (0.0665, 3)
                ],
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
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "AoE", Duration = 0 },
            new() { Description = "+70% movement speed", Duration = 3 }
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