using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services;

public interface IAbilityDamageCalculator
{
    bool CanHandleAbility(string abilityName);

    DamageRange CalculateTotalDamageRange(string abilityName, AbilityRarity abilityRarity, int playerLevel);

    DamageRange CalculateBaseDamageRange(string abilityName, AbilityRarity abilityRarity, int playerLevel);

    DamageRange CalculateSpecialDamageRange(string abilityName, AbilityRarity abilityRarity, int playerLevel);

    DamageRange CalculateDotDamageRange(string abilityName, AbilityRarity abilityRarity, int playerLevel);

    List<DamageHit> CalculateBaseHits(string abilityName, AbilityRarity abilityRarity, int playerLevel);

    List<DamageHit> CalculateSpecialHits(string abilityName, AbilityRarity abilityRarity, int playerLevel);

    List<DamageHit> CalculateDotHits(string abilityName, AbilityRarity abilityRarity, int playerLevel);

    decimal? CalculateTimeToKillBasedOnSimulation(string abilityName, AbilityRarity abilityRarity, int playerLevel, int targetLevel);

    decimal? CalculateTimeToKillBasedOnDps(string abilityName, AbilityRarity abilityRarity, int playerLevel, int targetLevel);
}
