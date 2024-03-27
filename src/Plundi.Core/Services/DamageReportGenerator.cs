using Plundi.Core.Models;
using Plundi.Core.Models.Abilities;

namespace Plundi.Core.Services;

public static class DamageReportGenerator
{
    private static Dictionary<Type, Func<IAbility, int, AbilityRarity, DamageReport, DamageReport>> _fixReportActions = new()
    {
        {typeof(HolyShield), FixHolyShieldDamageReport},
        {typeof(ToxicSmackerel), FixToxicSmackerelDamageReport}
    };

    public static DamageReport GenerateDamageReport(IAbility ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var damageProfile = ability.GetDamageProfile(characterLevel, abilityRarity);
        var attackPower = CharacterStatsProvider.GetAttackPower(characterLevel);
        var realCooldown = GetRealCooldown(ability, characterLevel, abilityRarity);

        var defaultHits = CalculateHits(damageProfile.DefaultHits, attackPower);
        var specialHits = CalculateHits(damageProfile.SpecialHits, attackPower);
        var dotHits = CalculateHits(damageProfile.DotHits, attackPower);

        var maxDefaultDmg = defaultHits.Sum(x => x.Damage);
        var maxSpecialDmg = specialHits.Sum(x => x.Damage);
        var maxDotDmg = dotHits.Sum(x => x.Damage);
        var maxDmg = maxDefaultDmg + maxSpecialDmg + maxDotDmg;

        var minDefaultDmg = defaultHits.FirstOrDefault().Damage;
        var minSpecialDmg = 0;
        var minDotDmg = maxDotDmg;
        var minDmg = minDefaultDmg + minSpecialDmg + minDotDmg;

        var maxDefaultDps = CalculateDps(defaultHits, realCooldown);
        var maxSpecialDps = CalculateDps(specialHits, realCooldown);
        var maxDotDps = CalculateDps(dotHits, realCooldown);
        var maxDps = maxDefaultDps + maxSpecialDps + maxDotDps;

        var minDefaultDps = CalculateDps([defaultHits.FirstOrDefault()], realCooldown);
        var minSpecialDps = 0;
        var minDotDps = maxDotDps;
        var minDps = minDefaultDps + minSpecialDps + minDotDps;

        var report = new DamageReport
        {
            DamageRange = (minDmg, maxDmg, minDps, maxDps),
            DefaultDamageRange = (minDefaultDmg, maxDefaultDmg, minDefaultDps, maxDefaultDps),
            SpecialDamageRange = (minSpecialDmg, maxSpecialDmg, minSpecialDps, maxSpecialDps),
            DotDamageRange = (minDotDmg, maxDotDmg, minDotDps, maxDotDps),
            DefaultHits = defaultHits,
            SpecialHits = specialHits,
            DotHits = dotHits
        };
        
        if (_fixReportActions.ContainsKey(ability.GetType()))
        {
            return _fixReportActions[ability.GetType()](ability, characterLevel, abilityRarity, report);
        }

        return report;
    }

    private static double GetRealCooldown(IAbility ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var cooldown = ability.GetCooldown(characterLevel, abilityRarity);
        cooldown += ability.CastDuration;

        return cooldown;
    }

    private static List<(double Damage, double Timing)> CalculateHits(IEnumerable<(double RelativeDamage, double Timing)> relativeHits,
        int attackPower)
    {
        return relativeHits.Select(x => (x.RelativeDamage * attackPower, x.Timing)).ToList();
    }

    private static double CalculateDps(IEnumerable<(double Damage, double Timing)> hits, double realCooldown)
    {
        // If hits happen after the real cooldown finished we do not include them - they will be overwritten by the following cast
        return hits.Where(x => x.Timing <= realCooldown).Sum(x => x.Damage) / realCooldown;
    }
    
    private static DamageReport FixHolyShieldDamageReport(IAbility ability, int characterLevel, AbilityRarity abilityRarity, DamageReport report)
    {
        // For Holy Shield it's unlikely to hit with the default hit, but not with the special hit
        // We therefore set the min damage to the special damage
        return report with
        {
            DamageRange = (report.SpecialDamageRange.Max, report.DamageRange.Max, report.SpecialDamageRange.MaxDps, report.DamageRange.MaxDps),
            DefaultDamageRange = (0, report.DefaultDamageRange.Max, 0, report.DefaultDamageRange.MaxDps),
            SpecialDamageRange = (report.SpecialDamageRange.Max, report.SpecialDamageRange.Max, report.SpecialDamageRange.MaxDps,
                report.SpecialDamageRange.MaxDps)
        };
    }
    
    private static DamageReport FixToxicSmackerelDamageReport(IAbility ability, int characterLevel, AbilityRarity abilityRarity, DamageReport report)
    {
        // For Toxic Smackerel we have to recalculate the max DPS because it is extremely unlikely to hit a pre-dotted target
        // We reduce it by 33.33% to simulate a scenario where we use Toxic Smackerel 3 times on a not yet posined target
        var fixedMaxSpecialDps = report.SpecialDamageRange.MaxDps * 0.6667;
        var fixedMaxDps = report.DefaultDamageRange.MaxDps + fixedMaxSpecialDps + report.DotDamageRange.MaxDps;
        return report with
        {
            DamageRange = (report.DamageRange.Min, report.DamageRange.Max, report.DamageRange.MinDps, fixedMaxDps),
            SpecialDamageRange = (report.SpecialDamageRange.Min, report.SpecialDamageRange.Max, report.SpecialDamageRange.MinDps, fixedMaxSpecialDps)
        };
    }
}