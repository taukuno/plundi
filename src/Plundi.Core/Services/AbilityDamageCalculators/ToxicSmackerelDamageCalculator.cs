using Plundi.Core.Models;
using Plundi.Core.Models.Abilities;

namespace Plundi.Core.Services.AbilityDamageCalculators;

public class ToxicSmackerelDamageCalculator : BaseAbilityDamageCalculator<ToxicSmackerel>
{
    // It's extremely unlikely to hit a pre-poisoned target, therefore the special DPS has to be recalculated
    // We reduce the DPS by 33.33% to simulate a scenario where we use Toxic Smackerel 3x on a not yet poisoned target

    public override DamageRange CalculateSpecialDamageRange(ToxicSmackerel ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var damageRange = base.CalculateSpecialDamageRange(ability, characterLevel, abilityRarity);
        return damageRange with
        {
            MaxDps = damageRange.MaxDps * 2 / 3
        };
    }

    public override double? CalculateTimeToKill(ToxicSmackerel ability, int characterLevel, AbilityRarity abilityRarity, int targetLevel)
    {
        var baseDamageRange = base.CalculateBaseDamageRange(ability, characterLevel, abilityRarity);
        var specialDamageRange = base.CalculateSpecialDamageRange(ability, characterLevel, abilityRarity);
        var dotDamageRange = base.CalculateDotDamageRange(ability, characterLevel, abilityRarity);

        var dpsWithoutBonus = baseDamageRange.MaxDps + dotDamageRange.MaxDps;
        var dpsWithBonus = dpsWithoutBonus + specialDamageRange.MaxDps;

        var targetHitPoints = Convert.ToDouble(CharacterStatsProvider.GetHitPoints(targetLevel));
        if (targetHitPoints <= 0 || dpsWithBonus <= 0)
        {
            return null;
        }

        var realCooldown = ability.CastDuration + ability.GetCooldown(characterLevel, abilityRarity);
        var damageDealtDuringFirstCooldown = dpsWithoutBonus * realCooldown;

        if (targetHitPoints <= damageDealtDuringFirstCooldown)
        {
            return targetHitPoints / dpsWithoutBonus;
        }

        return (targetHitPoints - damageDealtDuringFirstCooldown) / dpsWithBonus + realCooldown;
    }
}