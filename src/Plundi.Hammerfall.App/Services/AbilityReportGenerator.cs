using Plundi.Hammerfall.Core.Models;
using Plundi.Hammerfall.Core.Services;

namespace Plundi.Hammerfall.App.Services;

public class AbilityReportGenerator
{
    private readonly IServiceProvider _serviceProvider;

    public AbilityReportGenerator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public DamageReport GenerateDamageReport(IAbility ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var abilityDamageCalculatorType = typeof(IAbilityDamageCalculator<>).MakeGenericType(ability.GetType());
        var abilityDamageCalculator = _serviceProvider.GetServices(abilityDamageCalculatorType).FirstOrDefault();

        if (abilityDamageCalculator is null)
        {
            return new()
            {
                TotalDamageRange = new(),
                BaseDamageRange = new(),
                SpecialDamageRange = new(),
                DotDamageRange = new(),
                BaseHits = [],
                SpecialHits = [],
                DotHits = []
            };
        }

        var calcTotalDamageRangeMethod = abilityDamageCalculatorType.GetMethod(nameof(IAbilityDamageCalculator<IAbility>.CalculateTotalDamageRange))!;
        var calcBaseDamageRangeMethod = abilityDamageCalculatorType.GetMethod(nameof(IAbilityDamageCalculator<IAbility>.CalculateBaseDamageRange))!;
        var calcSpecialDamageRangeMethod = abilityDamageCalculatorType.GetMethod(nameof(IAbilityDamageCalculator<IAbility>.CalculateSpecialDamageRange))!;
        var calcDotDamageRangeMethod = abilityDamageCalculatorType.GetMethod(nameof(IAbilityDamageCalculator<IAbility>.CalculateDotDamageRange))!;
        var calcBaseHitsMethod = abilityDamageCalculatorType.GetMethod(nameof(IAbilityDamageCalculator<IAbility>.CalculateBaseHits))!;
        var calcSpecialHitsMethod = abilityDamageCalculatorType.GetMethod(nameof(IAbilityDamageCalculator<IAbility>.CalculateSpecialHits))!;
        var calcDotHitsMethod = abilityDamageCalculatorType.GetMethod(nameof(IAbilityDamageCalculator<IAbility>.CalculateDotHits))!;

        return new()
        {
            TotalDamageRange = (DamageRange)calcTotalDamageRangeMethod.Invoke(abilityDamageCalculator, [ability, characterLevel, abilityRarity])!,
            BaseDamageRange = (DamageRange)calcBaseDamageRangeMethod.Invoke(abilityDamageCalculator, [ability, characterLevel, abilityRarity])!,
            SpecialDamageRange = (DamageRange)calcSpecialDamageRangeMethod.Invoke(abilityDamageCalculator, [ability, characterLevel, abilityRarity])!,
            DotDamageRange = (DamageRange)calcDotDamageRangeMethod.Invoke(abilityDamageCalculator, [ability, characterLevel, abilityRarity])!,
            BaseHits = (List<DamageHit>)calcBaseHitsMethod.Invoke(abilityDamageCalculator, [ability, characterLevel, abilityRarity])!,
            SpecialHits = (List<DamageHit>)calcSpecialHitsMethod.Invoke(abilityDamageCalculator, [ability, characterLevel, abilityRarity])!,
            DotHits = (List<DamageHit>)calcDotHitsMethod.Invoke(abilityDamageCalculator, [ability, characterLevel, abilityRarity])!
        };
    }

    public bool TryGenerateKillReportBasedOnSimulation(IAbility ability, int characterLevel, AbilityRarity abilityRarity, int targetLevel, out KillReport? killReport)
    {
        var abilityDamageCalculatorType = typeof(IAbilityDamageCalculator<>).MakeGenericType(ability.GetType());
        var abilityDamageCalculator = _serviceProvider.GetServices(abilityDamageCalculatorType).FirstOrDefault();
        var calcTimeToKillMethod = abilityDamageCalculatorType.GetMethod(nameof(IAbilityDamageCalculator<IAbility>.CalculateTimeToKillBasedOnSimulation))!;

        var timeToKill = (decimal?)calcTimeToKillMethod.Invoke(abilityDamageCalculator, [ability, characterLevel, abilityRarity, targetLevel]);

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

    public bool TryGenerateKillReportBasedOnDps(IAbility ability, int characterLevel, AbilityRarity abilityRarity, int targetLevel, out KillReport? killReport)
    {
        var abilityDamageCalculatorType = typeof(IAbilityDamageCalculator<>).MakeGenericType(ability.GetType());
        var abilityDamageCalculator = _serviceProvider.GetServices(abilityDamageCalculatorType).FirstOrDefault();
        var calcTimeToKillMethod = abilityDamageCalculatorType.GetMethod(nameof(IAbilityDamageCalculator<IAbility>.CalculateTimeToKillBasedOnDps))!;

        var timeToKill = (decimal?)calcTimeToKillMethod.Invoke(abilityDamageCalculator, [ability, characterLevel, abilityRarity, targetLevel]);

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
