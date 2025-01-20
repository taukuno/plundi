using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services;

public interface IAbilityDamageCalculator
{
    bool CanHandleAbility(string abilityName);

    DamageRange CalculateTotalDamageRange(string abilityName, AbilityRarity abilityRarity, int characterLevel);

    DamageRange CalculateBaseDamageRange(string abilityName, AbilityRarity abilityRarity, int characterLevel);

    DamageRange CalculateSpecialDamageRange(string abilityName, AbilityRarity abilityRarity, int characterLevel);

    DamageRange CalculateDotDamageRange(string abilityName, AbilityRarity abilityRarity, int characterLevel);

    List<DamageHit> CalculateBaseHits(string abilityName, AbilityRarity abilityRarity, int characterLevel);

    List<DamageHit> CalculateSpecialHits(string abilityName, AbilityRarity abilityRarity, int characterLevel);

    List<DamageHit> CalculateDotHits(string abilityName, AbilityRarity abilityRarity, int characterLevel);

    decimal? CalculateTimeToKillBasedOnSimulation(string abilityName, AbilityRarity abilityRarity, int characterLevel, int targetLevel);

    decimal? CalculateTimeToKillBasedOnDps(string abilityName, AbilityRarity abilityRarity, int characterLevel, int targetLevel);
}
