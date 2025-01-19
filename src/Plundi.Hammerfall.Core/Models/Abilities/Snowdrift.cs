namespace Plundi.Hammerfall.Core.Models.Abilities;

public class Snowdrift : IAbility
{
    private readonly Dictionary<AbilityRarity, double> _cooldowns = new()
    {
        { AbilityRarity.Common, 14 },
        { AbilityRarity.Uncommon, 12 },
        { AbilityRarity.Rare, 10 },
        { AbilityRarity.Epic, 8 }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        {
            AbilityRarity.Common,
            new()
            {
                BaseHits =
                [
                    new() { Damage = 0.1412, IsRelative = true, Timing = 0 },
                    new() { Damage = 0.1412, IsRelative = true, Timing = 0.5 },
                    new() { Damage = 0.1412, IsRelative = true, Timing = 1 }
                ],
                SpecialHits = [],
                DotHits = []
            }
        },
        {
            AbilityRarity.Uncommon,
            new()
            {
                BaseHits =
                [
                    new() { Damage = 0.1497, IsRelative = true, Timing = 0 },
                    new() { Damage = 0.1497, IsRelative = true, Timing = 0.5 },
                    new() { Damage = 0.1497, IsRelative = true, Timing = 1 }
                ],
                SpecialHits = [],
                DotHits = []
            }
        },
        {
            AbilityRarity.Rare,
            new()
            {
                BaseHits =
                [
                    new() { Damage = 0.158, IsRelative = true, Timing = 0 },
                    new() { Damage = 0.158, IsRelative = true, Timing = 0.5 },
                    new() { Damage = 0.158, IsRelative = true, Timing = 1 }
                ],
                SpecialHits = [],
                DotHits = []
            }
        },
        {
            AbilityRarity.Epic,
            new()
            {
                BaseHits =
                [
                    new() { Damage = 0.166, IsRelative = true, Timing = 0 },
                    new() { Damage = 0.166, IsRelative = true, Timing = 0.5 },
                    new() { Damage = 0.166, IsRelative = true, Timing = 1 }
                ],
                SpecialHits = [],
                DotHits = []
            }
        }
    };

    /// <inheritdoc />
    public string Name => "Snowdrift";

    /// <inheritdoc />
    public double CastDuration => 0.5;

    /// <inheritdoc />
    public double ChannelDuration => 1;

    /// <inheritdoc />
    public string ImagePath => "snowdrift.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Utility;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "AoE", Duration = 0 },
            new() { Description = "stacking slow (30-90%) on hit", Duration = 4.5 }
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