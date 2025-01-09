using Plundi.Hammerfall.Core.Services;

namespace Plundi.Hammerfall.Core.Models.Abilities;

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
                BaseHits = [new() { Damage = 0.114, IsRelative = true, Timing = 0 }],
                SpecialHits = [new() { Damage = 0.18, IsRelative = true, Timing = 0 }],
                DotHits =
                [
                    new() { Damage = 0.0333, IsRelative = true, Timing = 1 }, new() { Damage = 0.0333, IsRelative = true, Timing = 2 },
                    new() { Damage = 0.0333, IsRelative = true, Timing = 3 }, new() { Damage = 0.0333, IsRelative = true, Timing = 4 },
                    new() { Damage = 0.0333, IsRelative = true, Timing = 5 }, new() { Damage = 0.0333, IsRelative = true, Timing = 6 },
                    new() { Damage = 0.0333, IsRelative = true, Timing = 7 }, new() { Damage = 0.0333, IsRelative = true, Timing = 8 }
                ]
            }
        },
        {
            AbilityRarity.Uncommon,
            new()
            {
                BaseHits = [new() { Damage = 0.12, IsRelative = true, Timing = 0 }],
                SpecialHits = [new() { Damage = 0.19, IsRelative = true, Timing = 0 }],
                DotHits =
                [
                    new() { Damage = 0.0350, IsRelative = true, Timing = 1 }, new() { Damage = 0.0350, IsRelative = true, Timing = 2 },
                    new() { Damage = 0.0350, IsRelative = true, Timing = 3 }, new() { Damage = 0.0350, IsRelative = true, Timing = 4 },
                    new() { Damage = 0.0350, IsRelative = true, Timing = 5 }, new() { Damage = 0.0350, IsRelative = true, Timing = 6 },
                    new() { Damage = 0.0350, IsRelative = true, Timing = 7 }, new() { Damage = 0.0350, IsRelative = true, Timing = 8 }
                ]
            }
        },
        {
            AbilityRarity.Rare,
            new()
            {
                BaseHits = [new() { Damage = 0.126, IsRelative = true, Timing = 0 }],
                SpecialHits = [new() { Damage = 0.20, IsRelative = true, Timing = 0 }],
                DotHits =
                [
                    new() { Damage = 0.0368, IsRelative = true, Timing = 1 }, new() { Damage = 0.0368, IsRelative = true, Timing = 2 },
                    new() { Damage = 0.0368, IsRelative = true, Timing = 3 }, new() { Damage = 0.0368, IsRelative = true, Timing = 4 },
                    new() { Damage = 0.0368, IsRelative = true, Timing = 5 }, new() { Damage = 0.0368, IsRelative = true, Timing = 6 },
                    new() { Damage = 0.0368, IsRelative = true, Timing = 7 }, new() { Damage = 0.0368, IsRelative = true, Timing = 8 }
                ]
            }
        },
        {
            AbilityRarity.Epic,
            new()
            {
                BaseHits = [new() { Damage = 0.132, IsRelative = true, Timing = 0 }],
                SpecialHits = [new() { Damage = 0.21, IsRelative = true, Timing = 0 }],
                DotHits =
                [
                    new() { Damage = 0.0385, IsRelative = true, Timing = 1 }, new() { Damage = 0.0385, IsRelative = true, Timing = 2 },
                    new() { Damage = 0.0385, IsRelative = true, Timing = 3 }, new() { Damage = 0.0385, IsRelative = true, Timing = 4 },
                    new() { Damage = 0.0385, IsRelative = true, Timing = 5 }, new() { Damage = 0.0385, IsRelative = true, Timing = 6 },
                    new() { Damage = 0.0385, IsRelative = true, Timing = 7 }, new() { Damage = 0.0385, IsRelative = true, Timing = 8 }
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
        var bonusDamage = Math.Round(_damageProfiles[abilityRarity].SpecialHits[0].Damage * CharacterStatsProvider.GetAttackPower(characterLevel), 1);

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
