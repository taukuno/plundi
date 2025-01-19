using Plundi.Hammerfall.Core.Models;
using Plundi.Hammerfall.Core.Models.Abilities;

namespace Plundi.Hammerfall.Core.Services.AbilityDamageCalculators;

public class HolyShieldDamageCalculator : BaseAbilityDamageCalculator<HolyShield>
{
    // It's unlikely to hit with the default hit, but not with the special hit
    // We therefore set the minimal damage and DPS to the maximal special damage and DPS

    /// <inheritdoc />
    public override DamageRange CalculateBaseDamageRange(HolyShield ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var damageRange = base.CalculateBaseDamageRange(ability, characterLevel, abilityRarity);
        return damageRange with
        {
            Min = 0,
            MinDps = 0
        };
    }

    /// <inheritdoc />
    public override DamageRange CalculateSpecialDamageRange(HolyShield ability, int characterLevel, AbilityRarity abilityRarity)
    {
        var damageRange = base.CalculateBaseDamageRange(ability, characterLevel, abilityRarity);
        return damageRange with
        {
            Min = damageRange.Max,
            MinDps = damageRange.MaxDps
        };
    }
}