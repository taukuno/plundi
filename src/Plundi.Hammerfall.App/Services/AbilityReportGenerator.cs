using Plundi.Hammerfall.Core.Models;
using Plundi.Hammerfall.Core.Services;

namespace Plundi.Hammerfall.App.Services;

public class AbilityReportGenerator
{
    private readonly IEnumerable<IAbilityDetailsProvider> _abilityDetailsProviders;
    private readonly IEnumerable<IAbilityDamageCalculator> _abilityDamageCalculators;

    public AbilityReportGenerator(IEnumerable<IAbilityDetailsProvider> abilityDetailsProviders, IEnumerable<IAbilityDamageCalculator> abilityDamageCalculators)
    {
        _abilityDetailsProviders = abilityDetailsProviders;
        _abilityDamageCalculators = abilityDamageCalculators;
    }

    public DamageReport GenerateDamageReport(string ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var abilityDamageCalculator = GetAbilityDamageCalculator(ability);

        return new()
        {
            TotalDamageRange = abilityDamageCalculator.CalculateTotalDamageRange(ability, characterLevel, abilityRarity),
            BaseDamageRange = abilityDamageCalculator.CalculateBaseDamageRange(ability, characterLevel, abilityRarity),
            SpecialDamageRange = abilityDamageCalculator.CalculateSpecialDamageRange(ability, characterLevel, abilityRarity),
            DotDamageRange = abilityDamageCalculator.CalculateDotDamageRange(ability, characterLevel, abilityRarity),
            BaseHits = abilityDamageCalculator.CalculateBaseHits(ability, characterLevel, abilityRarity),
            SpecialHits = abilityDamageCalculator.CalculateSpecialHits(ability, characterLevel, abilityRarity),
            DotHits =abilityDamageCalculator.CalculateDotHits(ability, characterLevel, abilityRarity)
        };
    }

    public bool TryGenerateKillReportBasedOnSimulation(string ability, int characterLevel, AbilityRarity abilityRarity, int targetLevel, out KillReport? killReport)
    {
        var abilityDamageCalculator = GetAbilityDamageCalculator(ability);
        var timeToKill = abilityDamageCalculator.CalculateTimeToKillBasedOnSimulation(ability, characterLevel, abilityRarity, targetLevel);

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

    public bool TryGenerateKillReportBasedOnDps(string ability, int characterLevel, AbilityRarity abilityRarity, int targetLevel, out KillReport? killReport)
    {
        var abilityDamageCalculator = GetAbilityDamageCalculator(ability);
        var timeToKill = abilityDamageCalculator.CalculateTimeToKillBasedOnDps(ability, characterLevel, abilityRarity, targetLevel);

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

    private IAbilityDetailsProvider GetAbilityDetailsProvider(string ability)
    {
        return _abilityDetailsProviders.FirstOrDefault(x => x.CanHandleAbility(ability)) ?? throw new InvalidOperationException($"No details provider registered for the ability '{ability}'.");
    }

    private IAbilityDamageCalculator GetAbilityDamageCalculator(string ability)
    {
        return _abilityDamageCalculators.FirstOrDefault(x => x.CanHandleAbility(ability)) ?? throw new InvalidOperationException($"No details provider registered for the ability '{ability}'.");
    }
}
