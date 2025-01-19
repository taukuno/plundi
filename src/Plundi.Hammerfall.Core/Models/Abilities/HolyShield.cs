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

    // The timings for the 2nd and special hits are arbitrary
    // I chose 0.6 s and 0.3 s respectively as an average
    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        {
            AbilityRarity.Common,
            new()
            {
                BaseHits = [new() { Damage = 0.18, IsRelative = true, Timing = 0 }, new() { Damage = 0.18, IsRelative = true, Timing = 0.5 }],
                SpecialHits = [new() { Damage = 0.36, IsRelative = true, Timing = 0.36 }],
                DotHits = []
            }
        },
        {
            AbilityRarity.Uncommon,
            new()
            {
                BaseHits = [new() { Damage = 0.19, IsRelative = true, Timing = 0 }, new() { Damage = 0.19, IsRelative = true, Timing = 0.5 }],
                SpecialHits = [new() { Damage = 0.374, IsRelative = true, Timing = 0.374 }],
                DotHits = []
            }
        },
        {
            AbilityRarity.Rare,
            new()
            {
                BaseHits = [new() { Damage = 0.198, IsRelative = true, Timing = 0 }, new() { Damage = 0.198, IsRelative = true, Timing = 0.5 }],
                SpecialHits = [new() { Damage = 0.395, IsRelative = true, Timing = 0.395 }],
                DotHits = []
            }
        },
        {
            AbilityRarity.Epic,
            new()
            {
                BaseHits = [new() { Damage = 0.2, IsRelative = true, Timing = 0 }, new() { Damage = 0.2, IsRelative = true, Timing = 0.5 }],
                SpecialHits = [new() { Damage = 0.409, IsRelative = true, Timing = 0.409 }],
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