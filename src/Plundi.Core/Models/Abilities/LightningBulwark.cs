namespace Plundi.Core.Models.Abilities;

public class LightningBulwark : IAbility
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
        {
            AbilityRarity.Common,
            new()
            {
                DefaultHits = [], SpecialHits = [0.035, 0.035, 0.035, 0.035, 0.035, 0.035, 0.035, 0.035, 0.035, 0.035],
                DotHits = []
            }
        },
        {
            AbilityRarity.Uncommon,
            new()
            {
                DefaultHits = [], SpecialHits = [0.036, 0.036, 0.036, 0.036, 0.036, 0.036, 0.036, 0.036, 0.036, 0.036],
                DotHits = []
            }
        },
        {
            AbilityRarity.Rare,
            new()
            {
                DefaultHits = [], SpecialHits = [0.037, 0.037, 0.037, 0.037, 0.037, 0.037, 0.037, 0.037, 0.037, 0.037],
                DotHits = []
            }
        },
        {
            AbilityRarity.Epic,
            new()
            {
                DefaultHits = [], SpecialHits = [0.038, 0.038, 0.038, 0.038, 0.038, 0.038, 0.038, 0.038, 0.038, 0.038],
                DotHits = []
            }
        }
    };

    /// <inheritdoc />
    public string Name => "Lightning Bulwark";

    /// <inheritdoc />
    public double CastDuration => 0;

    /// <inheritdoc />
    public double ChannelDuration => 2;

    /// <inheritdoc />
    public string ImagePath => "lightning_bulwark.jpg";

    /// <inheritdoc />
    public AbilityType Type => AbilityType.Utility;

    /// <inheritdoc />
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity)
    {
        return
        [
            new() { Description = "immune to damage until first hit", Duration = 2 },
            new() { Description = "+120% movement speed if attack repelled", Duration = 4 },
            new() { Description = "AoE if attack repelled", Duration = 4 }
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