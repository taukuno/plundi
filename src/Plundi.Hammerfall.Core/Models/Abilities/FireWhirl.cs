namespace Plundi.Hammerfall.Core.Models.Abilities;

public class FireWhirl : IAbility
{
    private readonly Dictionary<AbilityRarity, decimal> _cooldowns = new()
    {
        { AbilityRarity.Common, 18m },
        { AbilityRarity.Uncommon, 16m },
        { AbilityRarity.Rare, 14m },
        { AbilityRarity.Epic, 12m }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        {
            AbilityRarity.Common,
            new()
            {
                BaseHits =
                [
                    new() { Damage = 0.104m, IsRelative = true, Timing = 0.2m }, new() { Damage = 0.104m, IsRelative = true, Timing = 0.4m },
                    new() { Damage = 0.104m, IsRelative = true, Timing = 0.6m }, new() { Damage = 0.104m, IsRelative = true, Timing = 0.8m },
                    new() { Damage = 0.104m, IsRelative = true, Timing = 1m }, new() { Damage = 0.104m, IsRelative = true, Timing = 1.2m },
                    new() { Damage = 0.104m, IsRelative = true, Timing = 1.4m }, new() { Damage = 0.104m, IsRelative = true, Timing = 1.6m },
                    new() { Damage = 0.104m, IsRelative = true, Timing = 1.8m }, new() { Damage = 0.104m, IsRelative = true, Timing = 2m },
                    new() { Damage = 0.104m, IsRelative = true, Timing = 2.2m }, new() { Damage = 0.104m, IsRelative = true, Timing = 2.4m },
                    new() { Damage = 0.104m, IsRelative = true, Timing = 2.6m }, new() { Damage = 0.104m, IsRelative = true, Timing = 2.8m },
                    new() { Damage = 0.104m, IsRelative = true, Timing = 3m }
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
                    new() { Damage = 0.109m, IsRelative = true, Timing = 0.2m }, new() { Damage = 0.109m, IsRelative = true, Timing = 0.4m },
                    new() { Damage = 0.109m, IsRelative = true, Timing = 0.6m }, new() { Damage = 0.109m, IsRelative = true, Timing = 0.8m },
                    new() { Damage = 0.109m, IsRelative = true, Timing = 1m }, new() { Damage = 0.109m, IsRelative = true, Timing = 1.2m },
                    new() { Damage = 0.109m, IsRelative = true, Timing = 1.4m }, new() { Damage = 0.109m, IsRelative = true, Timing = 1.6m },
                    new() { Damage = 0.109m, IsRelative = true, Timing = 1.8m }, new() { Damage = 0.109m, IsRelative = true, Timing = 2m },
                    new() { Damage = 0.109m, IsRelative = true, Timing = 2.2m }, new() { Damage = 0.109m, IsRelative = true, Timing = 2.4m },
                    new() { Damage = 0.109m, IsRelative = true, Timing = 2.6m }, new() { Damage = 0.109m, IsRelative = true, Timing = 2.8m },
                    new() { Damage = 0.109m, IsRelative = true, Timing = 3m }
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
                    new() { Damage = 0.115m, IsRelative = true, Timing = 0.2m }, new() { Damage = 0.115m, IsRelative = true, Timing = 0.4m },
                    new() { Damage = 0.115m, IsRelative = true, Timing = 0.6m }, new() { Damage = 0.115m, IsRelative = true, Timing = 0.8m },
                    new() { Damage = 0.115m, IsRelative = true, Timing = 1m }, new() { Damage = 0.115m, IsRelative = true, Timing = 1.2m },
                    new() { Damage = 0.115m, IsRelative = true, Timing = 1.4m }, new() { Damage = 0.115m, IsRelative = true, Timing = 1.6m },
                    new() { Damage = 0.115m, IsRelative = true, Timing = 1.8m }, new() { Damage = 0.115m, IsRelative = true, Timing = 2m },
                    new() { Damage = 0.115m, IsRelative = true, Timing = 2.2m }, new() { Damage = 0.115m, IsRelative = true, Timing = 2.4m },
                    new() { Damage = 0.115m, IsRelative = true, Timing = 2.6m }, new() { Damage = 0.115m, IsRelative = true, Timing = 2.8m },
                    new() { Damage = 0.115m, IsRelative = true, Timing = 3m }
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
                    new() { Damage = 0.12m, IsRelative = true, Timing = 0.2m }, new() { Damage = 0.12m, IsRelative = true, Timing = 0.4m },
                    new() { Damage = 0.12m, IsRelative = true, Timing = 0.6m }, new() { Damage = 0.12m, IsRelative = true, Timing = 0.8m },
                    new() { Damage = 0.12m, IsRelative = true, Timing = 1m }, new() { Damage = 0.12m, IsRelative = true, Timing = 1.2m },
                    new() { Damage = 0.12m, IsRelative = true, Timing = 1.4m }, new() { Damage = 0.12m, IsRelative = true, Timing = 1.6m },
                    new() { Damage = 0.12m, IsRelative = true, Timing = 1.8m }, new() { Damage = 0.12m, IsRelative = true, Timing = 2m },
                    new() { Damage = 0.12m, IsRelative = true, Timing = 2.2m }, new() { Damage = 0.12m, IsRelative = true, Timing = 2.4m },
                    new() { Damage = 0.12m, IsRelative = true, Timing = 2.6m }, new() { Damage = 0.12m, IsRelative = true, Timing = 2.8m },
                    new() { Damage = 0.12m, IsRelative = true, Timing = 3m }
                ],
                SpecialHits = [],
                DotHits = []
            }
        }
    };

    /// <inheritdoc />
    public string Name => "Fire Whirl";

    /// <inheritdoc />
    public decimal CastDuration => 1m;

    /// <inheritdoc />
    public decimal ChannelDuration => 3;

    /// <inheritdoc />
    public string ImagePath => "fire_whirl.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "AoE", Duration = 0m },
            new() { Description = "+70% movement speed", Duration = 3m }
        ];
    }

    /// <inheritdoc />
    public decimal GetCooldown(int characterLevel, AbilityRarity abilityRarity)
    {
        return _cooldowns[abilityRarity];
    }

    /// <inheritdoc />
    public DamageProfile GetDamageProfile(int characterLevel, AbilityRarity abilityRarity)
    {
        return _damageProfiles[abilityRarity];
    }
}
