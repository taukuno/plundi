using Plundi.Hammerfall.Core.Services;

namespace Plundi.Hammerfall.Core.Models.Abilities;

public class ToxicSmackerel : IAbility
{
    private readonly Dictionary<AbilityRarity, double> _cooldowns = new()
    {
        { AbilityRarity.Common, 7 },
        { AbilityRarity.Uncommon, 6.5 },
        { AbilityRarity.Rare, 6 },
        { AbilityRarity.Epic, 5.5 }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        {
            AbilityRarity.Common,
            new()
            {
                BaseHits = [new() { Damage = 0.161, IsRelative = true, Timing = 0 }],
                SpecialHits = [new() { Damage = 0.254, IsRelative = true, Timing = 0 }],
                DotHits =
                [
                    new() { Damage = 0.0466, IsRelative = true, Timing = 1 }, new() { Damage = 0.0466, IsRelative = true, Timing = 2 },
                    new() { Damage = 0.0466, IsRelative = true, Timing = 3 }, new() { Damage = 0.0466, IsRelative = true, Timing = 4 },
                    new() { Damage = 0.0466, IsRelative = true, Timing = 5 }, new() { Damage = 0.0466, IsRelative = true, Timing = 6 },
                    new() { Damage = 0.0466, IsRelative = true, Timing = 7 }, new() { Damage = 0.0466, IsRelative = true, Timing = 8 }
                ]
            }
        },
        {
            AbilityRarity.Uncommon,
            new()
            {
                BaseHits = [new() { Damage = 0.169, IsRelative = true, Timing = 0 }],
                SpecialHits = [new() { Damage = 0.268, IsRelative = true, Timing = 0 }],
                DotHits =
                [
                    new() { Damage = 0.0492, IsRelative = true, Timing = 1 }, new() { Damage = 0.0492, IsRelative = true, Timing = 2 },
                    new() { Damage = 0.0492, IsRelative = true, Timing = 3 }, new() { Damage = 0.0492, IsRelative = true, Timing = 4 },
                    new() { Damage = 0.0492, IsRelative = true, Timing = 5 }, new() { Damage = 0.0492, IsRelative = true, Timing = 6 },
                    new() { Damage = 0.0492, IsRelative = true, Timing = 7 }, new() { Damage = 0.0492, IsRelative = true, Timing = 8 }
                ]
            }
        },
        {
            AbilityRarity.Rare,
            new()
            {
                BaseHits = [new() { Damage = 0.178, IsRelative = true, Timing = 0 }],
                SpecialHits = [new() { Damage = 0.282, IsRelative = true, Timing = 0 }],
                DotHits =
                [
                    new() { Damage = 0.05188, IsRelative = true, Timing = 1 }, new() { Damage = 0.05188, IsRelative = true, Timing = 2 },
                    new() { Damage = 0.05188, IsRelative = true, Timing = 3 }, new() { Damage = 0.05188, IsRelative = true, Timing = 4 },
                    new() { Damage = 0.05188, IsRelative = true, Timing = 5 }, new() { Damage = 0.05188, IsRelative = true, Timing = 6 },
                    new() { Damage = 0.05188, IsRelative = true, Timing = 7 }, new() { Damage = 0.05188, IsRelative = true, Timing = 8 }
                ]
            }
        },
        {
            AbilityRarity.Epic,
            new()
            {
                BaseHits = [new() { Damage = 0.1863, IsRelative = true, Timing = 0 }],
                SpecialHits = [new() { Damage = 0.2962, IsRelative = true, Timing = 0 }],
                DotHits =
                [
                    new() { Damage = 0.0543, IsRelative = true, Timing = 1 }, new() { Damage = 0.0543, IsRelative = true, Timing = 2 },
                    new() { Damage = 0.0543, IsRelative = true, Timing = 3 }, new() { Damage = 0.0543, IsRelative = true, Timing = 4 },
                    new() { Damage = 0.0543, IsRelative = true, Timing = 5 }, new() { Damage = 0.0543, IsRelative = true, Timing = 6 },
                    new() { Damage = 0.0543, IsRelative = true, Timing = 7 }, new() { Damage = 0.0543, IsRelative = true, Timing = 8 }
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