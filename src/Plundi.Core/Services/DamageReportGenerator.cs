using Plundi.Core.Models;

namespace Plundi.Core.Services;

public class DamageReportGenerator
{
    public static DamageReport GenerateDamageReport(IAbility ability, int characterLevel, AbilityRarity abilityRarity)
    {
        if (characterLevel is < 1 or > 10)
        {
            throw new ArgumentOutOfRangeException(nameof(characterLevel), "Character level must be between 1 and 10.");
        }

        return GenerateDamageReport(ability.GetDamageProfile(characterLevel, abilityRarity), characterLevel);
    }

    private static DamageReport GenerateDamageReport(DamageProfile profile, int characterLevel)
    {
        var attackPower = CharacterStatsProvider.GetAttackPower(characterLevel);

        var defaultHits = profile.DefaultHits.Select(x => Math.Round(x * attackPower, 1)).ToList();
        var specialHits = profile.SpecialHits.Select(x => Math.Round(x * attackPower, 1)).ToList();
        var dotHits = profile.DotHits.Select(x => Math.Round(x * attackPower, 1)).ToList();

        var maxDmg = Math.Round(defaultHits.Sum() + specialHits.Sum() + dotHits.Sum(), 1);
        var minDmg = Math.Round(defaultHits.FirstOrDefault(), 1);

        return new()
        {
            DamageRange = (minDmg, maxDmg),
            HitDamageRange = (defaultHits.FirstOrDefault(), Math.Round(defaultHits.Sum(), 1)),
            SpecialDamageRange = (0, Math.Round(specialHits.Sum(), 1)),
            DotDamageRange = (0, Math.Round(dotHits.Sum(), 1)),
            DefaultHits = defaultHits,
            SpecialHits = specialHits,
            DotHits = dotHits
        };
    }
}