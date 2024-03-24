namespace Plundi.Core.Models.Abilities;

public class FadeToShadow : IAbility
{
    private readonly Dictionary<AbilityRarity, double> _cooldowns = new()
    {
        { AbilityRarity.Common, 18 },
        { AbilityRarity.Uncommon, 16 },
        { AbilityRarity.Rare, 14 },
        { AbilityRarity.Epic, 12 }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        { AbilityRarity.Common, new() { DefaultHits = [], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { DefaultHits = [], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { DefaultHits = [], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { DefaultHits = [], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public string Name => "Fade to Shadow";

    /// <inheritdoc />
    public double CastDuration => 0;

    /// <inheritdoc />
    public double ChannelDuration => 0;

    /// <inheritdoc />
    public string ImagePath => "fade_to_shadow.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Utility;

    /// <inheritdoc />
    public List<(string Effect, double Duration)> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return [("blinks 25 yards", 0), ("stealth after blink", 4)];
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