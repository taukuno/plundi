namespace Plundi.Hammerfall.Core.Models.Abilities;

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
        {
            AbilityRarity.Common,
            new()
            {
                BaseHits =
                [
                    new() { Damage = 0.222, IsRelative = true, Timing = 0.75 },
                    new() { Damage = 0.222, IsRelative = true, Timing = 1.5 },
                    new() { Damage = 0.445, IsRelative = true, Timing = 2.25 }
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
                    new() { Damage = 0.237, IsRelative = true, Timing = 0.75 },
                    new() { Damage = 0.237, IsRelative = true, Timing = 1.5 },
                    new() { Damage = 0.473, IsRelative = true, Timing = 2.25 }
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
                    new() { Damage = 0.247, IsRelative = true, Timing = 0.75 },
                    new() { Damage = 0.247, IsRelative = true, Timing = 1.5 },
                    new() { Damage = 0.494, IsRelative = true, Timing = 2.25 }
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
                    new() { Damage = 0.258, IsRelative = true, Timing = 0.75 },
                    new() { Damage = 0.258, IsRelative = true, Timing = 1.5 },
                    new() { Damage = 0.515, IsRelative = true, Timing = 2.25 }
                ],
                SpecialHits = [],
                DotHits = []
            }
        }
    };

    /// <inheritdoc />
    public string Name => "Storm Archon";

    /// <inheritdoc />
    public double CastDuration => 0;

    /// <inheritdoc />
    public double ChannelDuration => 2.25;

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
            new() { Description = "can be recast twice (last cast does double-damage)", Duration = 0 },
            new() { Description = "heavily slowed while channeling (-96%)", Duration = 2.25 }
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