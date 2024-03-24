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
            new() { DefaultHits = [0.114], SpecialHits = [0.18], DotHits = [0.0333, 0.0333, 0.0333, 0.0333, 0.0333, 0.0333, 0.0333, 0.0333] }
        },
        {
            AbilityRarity.Uncommon,
            new() { DefaultHits = [0.12], SpecialHits = [0.19], DotHits = [0.0350, 0.0350, 0.0350, 0.0350, 0.0350, 0.0350, 0.0350, 0.0350] }
        },
        {
            AbilityRarity.Rare,
            new() { DefaultHits = [0.126], SpecialHits = [0.20], DotHits = [0.0368, 0.0368, 0.0368, 0.0368, 0.0368, 0.0368, 0.0368, 0.0368] }
        },
        {
            AbilityRarity.Epic,
            new() { DefaultHits = [0.132], SpecialHits = [0.21], DotHits = [0.0385, 0.0385, 0.0385, 0.0385, 0.0385, 0.0385, 0.0385, 0.0385] }
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
    public List<(string Effect, double Duration)> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        var bonusDamage = Math.Round(_damageProfiles[abilityRarity].SpecialHits[0] * CharacterStatsProvider.GetAttackPower(characterLevel));
        return [("frontal cone", 0), ("applies poison dot", 8), ($"does bonus damage on already poisoned targets ({bonusDamage})", 0)];
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