namespace Plundi.Hammerfall.Core.Models.Abilities;

public class HolyShield : IAbility
{
    private readonly Dictionary<AbilityRarity, double> _cooldowns = new()
    {
        { AbilityRarity.Common, 14 },
        { AbilityRarity.Uncommon, 12 },
        { AbilityRarity.Rare, 10 },
        { AbilityRarity.Epic, 8 }
    };

    // The timings for the 2nd & special hits are arbitrary
    // I chose 0.6s and 0.3s respectively as an average
    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        {
            AbilityRarity.Common,
            new()
            {
                BaseHits = [new() { Damage = 0.1275, IsRelative = true, Timing = 0 }, new() { Damage = 0.1275, IsRelative = true, Timing = 0.5 }],
                SpecialHits = [new() { Damage = 0.255, IsRelative = true, Timing = 0.25 }], 
                DotHits = []
            }
        },
        {
            AbilityRarity.Uncommon,
            new()
            {
                BaseHits = [new() { Damage = 0.135, IsRelative = true, Timing = 0 }, new() { Damage = 0.135, IsRelative = true, Timing = 0.5 }],
                SpecialHits = [new() { Damage = 0.265, IsRelative = true, Timing = 0.25 }], 
                DotHits = []
            }
        },
        {
            AbilityRarity.Rare,
            new()
            {
                BaseHits = [new() { Damage = 0.14, IsRelative = true, Timing = 0 }, new() { Damage = 0.14, IsRelative = true, Timing = 0.5 }],
                SpecialHits = [new() { Damage = 0.28, IsRelative = true, Timing = 0.25 }], 
                DotHits = []
            }
        },
        {
            AbilityRarity.Epic,
            new()
            {
                BaseHits = [new() { Damage = 0.145, IsRelative = true, Timing = 0 }, new() { Damage = 0.145, IsRelative = true, Timing = 0.5 }],
                SpecialHits = [new() { Damage = 0.29, IsRelative = true, Timing = 0.25 }],
                DotHits = []
            }
        }
    };

    /// <inheritdoc />
    public string Name => "Holy Shield";

    /// <inheritdoc />
    public double CastDuration => 1;

    /// <inheritdoc />
    public double ChannelDuration => 0;

    /// <inheritdoc />
    public string ImagePath => "holy_shield.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "frontal swirly (bommerangs)", Duration = 0 },
            new() { Description = "recast to activate AoE", Duration = 0 }
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