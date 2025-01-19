using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services;

public interface IAbilityDamageCalculator
{
    bool CanHandleAbility(string ability);

    DamageRange CalculateTotalDamageRange(string ability, int characterLevel, AbilityRarity abilityRarity);

    DamageRange CalculateBaseDamageRange(string ability, int characterLevel, AbilityRarity abilityRarity);

    DamageRange CalculateSpecialDamageRange(string ability, int characterLevel, AbilityRarity abilityRarity);

    DamageRange CalculateDotDamageRange(string ability, int characterLevel, AbilityRarity abilityRarity);

    List<DamageHit> CalculateBaseHits(string ability, int characterLevel, AbilityRarity abilityRarity);

    List<DamageHit> CalculateSpecialHits(string ability, int characterLevel, AbilityRarity abilityRarity);

    List<DamageHit> CalculateDotHits(string ability, int characterLevel, AbilityRarity abilityRarity);

    decimal? CalculateTimeToKillBasedOnSimulation(string ability, int characterLevel, AbilityRarity abilityRarity, int targetLevel);

    decimal? CalculateTimeToKillBasedOnDps(string ability, int characterLevel, AbilityRarity abilityRarity, int targetLevel);
}
