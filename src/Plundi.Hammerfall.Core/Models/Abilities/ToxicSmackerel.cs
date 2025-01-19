using Plundi.Hammerfall.Core.Services;

namespace Plundi.Hammerfall.Core.Models.Abilities;

public class ToxicSmackerel : IAbility
{
    private readonly Dictionary<AbilityRarity, decimal> _cooldowns = new()
    {
        { AbilityRarity.Common, 7m },
        { AbilityRarity.Uncommon, 6.5m },
        { AbilityRarity.Rare, 6m },
        { AbilityRarity.Epic, 5.5m }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        {
            AbilityRarity.Common,
            new()
            {
                BaseHits = [new() { Damage = 0.161m, IsRelative = true, Timing = 0m }],
                SpecialHits = [new() { Damage = 0.254m, IsRelative = true, Timing = 0m }],
                DotHits =
                [
                    new() { Damage = 0.0466m, IsRelative = true, Timing = 1m }, new() { Damage = 0.0466m, IsRelative = true, Timing = 2m },
                    new() { Damage = 0.0466m, IsRelative = true, Timing = 3m }, new() { Damage = 0.0466m, IsRelative = true, Timing = 4m },
                    new() { Damage = 0.0466m, IsRelative = true, Timing = 5m }, new() { Damage = 0.0466m, IsRelative = true, Timing = 6m },
                    new() { Damage = 0.0466m, IsRelative = true, Timing = 7m }, new() { Damage = 0.0466m, IsRelative = true, Timing = 8m }
                ]
            }
        },
        {
            AbilityRarity.Uncommon,
            new()
            {
                BaseHits = [new() { Damage = 0.169m, IsRelative = true, Timing = 0m }],
                SpecialHits = [new() { Damage = 0.268m, IsRelative = true, Timing = 0m }],
                DotHits =
                [
                    new() { Damage = 0.0492m, IsRelative = true, Timing = 1m }, new() { Damage = 0.0492m, IsRelative = true, Timing = 2m },
                    new() { Damage = 0.0492m, IsRelative = true, Timing = 3m }, new() { Damage = 0.0492m, IsRelative = true, Timing = 4m },
                    new() { Damage = 0.0492m, IsRelative = true, Timing = 5m }, new() { Damage = 0.0492m, IsRelative = true, Timing = 6m },
                    new() { Damage = 0.0492m, IsRelative = true, Timing = 7m }, new() { Damage = 0.0492m, IsRelative = true, Timing = 8m }
                ]
            }
        },
        {
            AbilityRarity.Rare,
            new()
            {
                BaseHits = [new() { Damage = 0.178m, IsRelative = true, Timing = 0m }],
                SpecialHits = [new() { Damage = 0.282m, IsRelative = true, Timing = 0m }],
                DotHits =
                [
                    new() { Damage = 0.05188m, IsRelative = true, Timing = 1m }, new() { Damage = 0.05188m, IsRelative = true, Timing = 2m },
                    new() { Damage = 0.05188m, IsRelative = true, Timing = 3m }, new() { Damage = 0.05188m, IsRelative = true, Timing = 4m },
                    new() { Damage = 0.05188m, IsRelative = true, Timing = 5m }, new() { Damage = 0.05188m, IsRelative = true, Timing = 6m },
                    new() { Damage = 0.05188m, IsRelative = true, Timing = 7m }, new() { Damage = 0.05188m, IsRelative = true, Timing = 8m }
                ]
            }
        },
        {
            AbilityRarity.Epic,
            new()
            {
                BaseHits = [new() { Damage = 0.1863m, IsRelative = true, Timing = 0m }],
                SpecialHits = [new() { Damage = 0.2962m, IsRelative = true, Timing = 0m }],
                DotHits =
                [
                    new() { Damage = 0.0543m, IsRelative = true, Timing = 1m }, new() { Damage = 0.0543m, IsRelative = true, Timing = 2m },
                    new() { Damage = 0.0543m, IsRelative = true, Timing = 3m }, new() { Damage = 0.0543m, IsRelative = true, Timing = 4m },
                    new() { Damage = 0.0543m, IsRelative = true, Timing = 5m }, new() { Damage = 0.0543m, IsRelative = true, Timing = 6m },
                    new() { Damage = 0.0543m, IsRelative = true, Timing = 7m }, new() { Damage = 0.0543m, IsRelative = true, Timing = 8m }
                ]
            }
        }
    };

    /// <inheritdoc />
    public string Name => "Toxic Smackerel";

    /// <inheritdoc />
    public decimal CastDuration => 0.5m;

    /// <inheritdoc />
    public decimal ChannelDuration => 0;

    /// <inheritdoc />
    public string ImagePath => "toxic_smackerel.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        var bonusDamage = Math.Round(_damageProfiles[abilityRarity].SpecialHits[0].Damage * CharacterStatsProvider.GetAttackPower(characterLevel), 1);

        return
        [
            new() { Description = "frontal cone", Duration = 0m },
            new() { Description = "applies poison dot", Duration = 8m },
            new() { Description = $"does bonus damage on already poisoned targets ({bonusDamage})", Duration = 8m }
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
