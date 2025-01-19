using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.App.Models;

public class RarifiedAbility
{
    public RarifiedAbility(IAbility ability, AbilityRarity rarity)
    {
        OriginalAbility = ability;
    }

    public IAbility OriginalAbility { get; }

    public string Name => OriginalAbility.Name;
    public AbilityType Type => OriginalAbility.Type;
    public AbilityRarity Rarity { get; set; } = AbilityRarity.Common;
    public decimal CastDuration => OriginalAbility.CastDuration;
    public decimal ChannelDuration => OriginalAbility.ChannelDuration;
    public string ImagePath => OriginalAbility.ImagePath;

    public List<AbilityEffect> GetEffects(int characterLevel)
    {
        return OriginalAbility.GetEffects(characterLevel, Rarity);
    }

    public decimal GetCooldown(int characterLevel)
    {
        return OriginalAbility.GetCooldown(characterLevel, Rarity);
    }

    public DamageProfile GetDamageProfile(int characterLevel)
    {
        return OriginalAbility.GetDamageProfile(characterLevel, Rarity);
    }
}
