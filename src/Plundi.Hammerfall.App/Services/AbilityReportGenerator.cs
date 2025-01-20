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

    public DamageReport GenerateDamageReport(string abilityName, AbilityRarity abilityRarity, int characterLevel)
    {
        var abilityDamageCalculator =_abilityServiceProvider.GetAbilityDamageCalculator(abilityName);

        return new()
        {
            TotalDamageRange = abilityDamageCalculator.CalculateTotalDamageRange(abilityName, abilityRarity, characterLevel),
            BaseDamageRange = abilityDamageCalculator.CalculateBaseDamageRange(abilityName, abilityRarity, characterLevel),
            SpecialDamageRange = abilityDamageCalculator.CalculateSpecialDamageRange(abilityName, abilityRarity, characterLevel),
            DotDamageRange = abilityDamageCalculator.CalculateDotDamageRange(abilityName, abilityRarity, characterLevel),
            BaseHits = abilityDamageCalculator.CalculateBaseHits(abilityName, abilityRarity, characterLevel),
            SpecialHits = abilityDamageCalculator.CalculateSpecialHits(abilityName, abilityRarity, characterLevel),
            DotHits =abilityDamageCalculator.CalculateDotHits(abilityName, abilityRarity, characterLevel)
        };
    }

    public bool TryGenerateKillReportBasedOnSimulation(string abilityName, AbilityRarity abilityRarity, int characterLevel, int targetLevel, out KillReport? killReport)
    {
        var abilityDamageCalculator = _abilityServiceProvider.GetAbilityDamageCalculator(abilityName);
        var timeToKill = abilityDamageCalculator.CalculateTimeToKillBasedOnSimulation(abilityName, abilityRarity, characterLevel, targetLevel);

        killReport = null;
        if (timeToKill is null)
        {
            return false;
        }

        killReport = new()
        {
            TargetHitPoints = CharacterStatsProvider.GetHitPoints(targetLevel),
            TimeToKill = timeToKill.Value
        };

        return true;
    }

    public bool TryGenerateKillReportBasedOnDps(string abilityName, AbilityRarity abilityRarity, int characterLevel, int targetLevel, out KillReport? killReport)
    {
        var abilityDamageCalculator = _abilityServiceProvider.GetAbilityDamageCalculator(abilityName);
        var timeToKill = abilityDamageCalculator.CalculateTimeToKillBasedOnDps(abilityName, abilityRarity, characterLevel, targetLevel);

        killReport = null;
        if (timeToKill is null)
        {
            return false;
        }

        killReport = new()
        {
            TargetHitPoints = CharacterStatsProvider.GetHitPoints(targetLevel),
            TimeToKill = timeToKill.Value
        };

        return true;
    }
}
