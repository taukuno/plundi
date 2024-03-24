namespace Plundi.Core.Models.Abilities;

public class HolyShield : IAbility
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
        { AbilityRarity.Common, new() { DefaultHits = [0.1275, 0.1275], SpecialHits = [0.255], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { DefaultHits = [0.135, 0.135], SpecialHits = [0.265], DotHits = [] } },
        { AbilityRarity.Rare, new() { DefaultHits = [0.14, 0.14], SpecialHits = [0.28], DotHits = [] } },
        { AbilityRarity.Epic, new() { DefaultHits = [0.145, 0.145], SpecialHits = [0.29], DotHits = [] } }
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
    public List<(string Effect, double Duration)> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return [("frontal swirly (bommerangs)", 0), ("recast to activate AoE", 0)];
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