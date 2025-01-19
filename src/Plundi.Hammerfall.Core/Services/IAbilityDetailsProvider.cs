using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services;

public interface IAbilityDetailsProvider
{
    bool CanHandleAbility(string abilityName);

    string GetDisplayName(string ability);

    decimal GetCastDuration(string ability);

    decimal GetChannelDuration(string ability);

    string GetImagePath(string ability);

    AbilityType GetAbilityType(string ability);

    IEnumerable<AbilityEffect> GetEffects(string ability, int characterLevel, AbilityRarity abilityRarity);

    decimal GetCooldown(string ability, int characterLevel, AbilityRarity abilityRarity);

    DamageProfile GetDamageProfile(string ability, int characterLevel, AbilityRarity abilityRarity);
}
