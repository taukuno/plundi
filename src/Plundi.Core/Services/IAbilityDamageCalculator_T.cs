using Plundi.Core.Models;

namespace Plundi.Core.Services;

public interface IAbilityDamageCalculator<in T> where T : IAbility
{
    DamageRange CalculateTotalDamageRange(T ability, int characterLevel, AbilityRarity abilityRarity);

    DamageRange CalculateBaseDamageRange(T ability, int characterLevel, AbilityRarity abilityRarity);

    DamageRange CalculateSpecialDamageRange(T ability, int characterLevel, AbilityRarity abilityRarity);

    DamageRange CalculateDotDamageRange(T ability, int characterLevel, AbilityRarity abilityRarity);

    List<DamageHit> CalculateBaseHits(T ability, int characterLevel, AbilityRarity abilityRarity);

    List<DamageHit> CalculateSpecialHits(T ability, int characterLevel, AbilityRarity abilityRarity);

    List<DamageHit> CalculateDotHits(T ability, int characterLevel, AbilityRarity abilityRarity);

    double? CalculateTimeToKillBasedOnSimulation(T ability, int characterLevel, AbilityRarity abilityRarity, int targetLevel);
    double? CalculateTimeToKillBasedOnDps(T ability, int characterLevel, AbilityRarity abilityRarity, int targetLevel);
}