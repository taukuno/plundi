using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDamageCalculators;

public class HolyShieldDamageCalculator : BaseAbilityDamageCalculator
{
    // It's unlikely to hit with the default hit, but not with the special hit
    // We therefore set the minimal damage and DPS to the maximal special damage and DPS

    /// <inheritdoc />
    public HolyShieldDamageCalculator(IEnumerable<IAbilityDetailsProvider> abilityDetailsProviders) : base(abilityDetailsProviders) {}

    /// <inheritdoc />
    public override bool CanHandleAbility(string ability)
    {
        return ability == "Holy Shield";
    }

    /// <inheritdoc />
    public override DamageRange CalculateBaseDamageRange(string ability, int characterLevel, AbilityRarity abilityRarity)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        var damageRange = base.CalculateBaseDamageRange(ability, characterLevel, abilityRarity);
        return damageRange with
        {
            Min = 0,
            MinDps = 0
        };
    }

    /// <inheritdoc />
    public override DamageRange CalculateSpecialDamageRange(string ability, int characterLevel, AbilityRarity abilityRarity)
    {
        if (!CanHandleAbility(ability))
        {
            throw new ArgumentException($"Can't handle the ability '{ability}'.", nameof(ability));
        }

        var damageRange = base.CalculateBaseDamageRange(ability, characterLevel, abilityRarity);
        return damageRange with
        {
            Min = damageRange.Max,
            MinDps = damageRange.MaxDps
        };
    }
}
