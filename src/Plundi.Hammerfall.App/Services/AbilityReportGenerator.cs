using Plundi.Hammerfall.Core.Models;
using Plundi.Hammerfall.Core.Services;

namespace Plundi.Hammerfall.App.Services;

public class AbilityReportGenerator
{
    private readonly AbilityServiceProvider _abilityServiceProvider;

    public AbilityReportGenerator(AbilityServiceProvider abilityServiceProvider)
    {
        _abilityServiceProvider = abilityServiceProvider;
    }

    public DamageReport GenerateDamageReport(string abilityName, AbilityRarity abilityRarity, int playerLevel)
    {
        var abilityDamageCalculator =_abilityServiceProvider.GetAbilityDamageCalculator(abilityName);

        return new()
        {
            TotalDamageRange = abilityDamageCalculator.CalculateTotalDamageRange(abilityName, abilityRarity, playerLevel),
            BaseDamageRange = abilityDamageCalculator.CalculateBaseDamageRange(abilityName, abilityRarity, playerLevel),
            SpecialDamageRange = abilityDamageCalculator.CalculateSpecialDamageRange(abilityName, abilityRarity, playerLevel),
            DotDamageRange = abilityDamageCalculator.CalculateDotDamageRange(abilityName, abilityRarity, playerLevel),
            BaseHits = abilityDamageCalculator.CalculateBaseHits(abilityName, abilityRarity, playerLevel),
            SpecialHits = abilityDamageCalculator.CalculateSpecialHits(abilityName, abilityRarity, playerLevel),
            DotHits =abilityDamageCalculator.CalculateDotHits(abilityName, abilityRarity, playerLevel)
        };
    }

    public bool TryGenerateKillReportBasedOnSimulation(string abilityName, AbilityRarity abilityRarity, int playerLevel, int targetLevel, out KillReport? killReport)
    {
        var abilityDamageCalculator = _abilityServiceProvider.GetAbilityDamageCalculator(abilityName);
        var timeToKill = abilityDamageCalculator.CalculateTimeToKillBasedOnSimulation(abilityName, abilityRarity, playerLevel, targetLevel);

        killReport = null;
        if (timeToKill is null)
        {
            return false;
        }

        killReport = new()
        {
            TargetHitPoints = PlayerStatsProvider.GetHitPoints(targetLevel),
            TimeToKill = timeToKill.Value
        };

        return true;
    }

    public bool TryGenerateKillReportBasedOnDps(string abilityName, AbilityRarity abilityRarity, int playerLevel, int targetLevel, out KillReport? killReport)
    {
        var abilityDamageCalculator = _abilityServiceProvider.GetAbilityDamageCalculator(abilityName);
        var timeToKill = abilityDamageCalculator.CalculateTimeToKillBasedOnDps(abilityName, abilityRarity, playerLevel, targetLevel);

        killReport = null;
        if (timeToKill is null)
        {
            return false;
        }

        killReport = new()
        {
            TargetHitPoints = PlayerStatsProvider.GetHitPoints(targetLevel),
            TimeToKill = timeToKill.Value
        };

        return true;
    }
}
