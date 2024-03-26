using Plundi.Core.Services;

namespace Plundi.Core.Models.Abilities;

public class ToxicSmackerel : IAbility
{
    private readonly Dictionary<AbilityRarity, double> _cooldowns = new()
    {
        { AbilityRarity.Common, 5.5 },
        { AbilityRarity.Uncommon, 5 },
        { AbilityRarity.Rare, 4.5 },
        { AbilityRarity.Epic, 4 }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        {
            AbilityRarity.Common,
            new()
            {
                DefaultHits = [(0.114, 0)], SpecialHits = [(0.18, 0)],
                DotHits =
                [
                    (0.0333, 1), (0.0333, 2), (0.0333, 3), (0.0333, 4), (0.0333, 5), (0.0333, 6), (0.0333, 7),
                    (0.0333, 8)
                ]
            }
        },
        {
            AbilityRarity.Uncommon,
            new()
            {
                DefaultHits = [(0.12, 0)], SpecialHits = [(0.19, 0)],
                DotHits =
                [
                    (0.0350, 1), (0.0350, 2), (0.0350, 3), (0.0350, 4), (0.0350, 5), (0.0350, 6), (0.0350, 7),
                    (0.0350, 8)
                ]
            }
        },
        {
            AbilityRarity.Rare,
            new()
            {
                DefaultHits = [(0.126, 0)], SpecialHits = [(0.20, 0)],
                DotHits =
                [
                    (0.0368, 1), (0.0368, 2), (0.0368, 3), (0.0368, 4), (0.0368, 5), (0.0368, 6), (0.0368, 7),
                    (0.0368, 8)
                ]
            }
        },
        {
            AbilityRarity.Epic,
            new()
            {
                DefaultHits = [(0.132, 0)], SpecialHits = [(0.21, 0)],
                DotHits =
                [
                    (0.0385, 1), (0.0385, 2), (0.0385, 3), (0.0385, 4), (0.0385, 5), (0.0385, 6), (0.0385, 7),
                    (0.0385, 8)
                ]
            }
        }
    };

    /// <inheritdoc />
    public string Name => "Toxic Smackerel";

    /// <inheritdoc />
    public double CastDuration => 0.5;

    /// <inheritdoc />
    public double ChannelDuration => 0;

    /// <inheritdoc />
    public string ImagePath => "toxic_smackerel.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Damage;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        var bonusDamage =
            Math.Round(
                _damageProfiles[abilityRarity].SpecialHits[0].RelativeDamage *
                CharacterStatsProvider.GetAttackPower(characterLevel), 1);
        return
        [
            new() { Description = "frontal cone", Duration = 0 },
            new() { Description = "applies poison dot", Duration = 8 },
            new() { Description = $"does bonus damage on already poisoned targets ({bonusDamage})", Duration = 8 }
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