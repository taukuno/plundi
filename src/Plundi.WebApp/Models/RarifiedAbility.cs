using Plundi.Core.Models;

namespace Plundi.WebApp.Models;

public class RarifiedAbility
{
    private IAbility _ability;

    public RarifiedAbility(IAbility ability, AbilityRarity rarity)
    {
        _ability = ability;
    }

    public IAbility OriginalAbility => _ability;
    public string Name => _ability.Name;
    public AbilityType Type => _ability.Type;
    public AbilityRarity Rarity { get; set; } = AbilityRarity.Common;
    public double CastDuration => _ability.CastDuration;
    public double ChannelDuration => _ability.ChannelDuration;
    public string ImagePath => _ability.ImagePath;

    public List<AbilityEffect> GetEffects(int characterLevel)
    {
        return _ability.GetEffects(characterLevel, Rarity);
    }

    public double GetCooldown(int characterLevel)
    {
        return _ability.GetCooldown(characterLevel, Rarity);
    }

    public DamageProfile GetDamageProfile(int characterLevel)
    {
        return _ability.GetDamageProfile(characterLevel, Rarity);
    }
}