using Plundi.Core.Models;

namespace Plundi.Core.Services;

public static class DamageReportGenerator
{
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

        // Currently Toxic Smackerel is the only ability to apply a dot
        // The dot gets applied with the first hit and therefore should be included in the minimal damage
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

        return new()
        {
            DamageRange = (minDmg, maxDmg, minDps, maxDps),
            DefaultDamageRange = (minDefaultDmg, maxDefaultDmg, minDefaultDps, maxDefaultDps),
            SpecialDamageRange = (minSpecialDmg, maxSpecialDmg, minSpecialDps, maxSpecialDps),
            DotDamageRange = (minDotDmg, maxDotDmg, minDotDps, maxDotDps),
            DefaultHits = defaultHits,
            SpecialHits = specialHits,
            DotHits = dotHits
        };
    }

    private static double GetRealCooldown(IAbility ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var cooldown = ability.GetCooldown(characterLevel, abilityRarity);
        cooldown += ability.CastDuration;

        return cooldown;
    }

    private static List<(double Damage, double Timing)> CalculateHits(
        IEnumerable<(double RelativeDamage, double Timing)> relativeHits, int attackPower)
    {
        return relativeHits.Select(x => (x.RelativeDamage * attackPower, x.Timing)).ToList();
    }

    private static double CalculateDps(IEnumerable<(double Damage, double Timing)> hits, double realCooldown)
    {
        // If hits happen after the real cooldown finished we do not include them
        // The player would/could potentially activate the cooldown exactly after the cooldown finished
        return hits.Where(x => x.Timing <= realCooldown).Sum(x => x.Damage) / realCooldown;
    }
}