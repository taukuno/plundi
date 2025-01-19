using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.App.Models;

public class RarifiedAbility
{
    public RarifiedAbility(string ability, AbilityRarity rarity)
    {
        Ability = ability;
        Rarity = rarity;
    }

    public string Ability { get; }
    public AbilityRarity Rarity { get; set; }

    /*public string Name => OriginalAbility.Name;
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
    }*/
}
