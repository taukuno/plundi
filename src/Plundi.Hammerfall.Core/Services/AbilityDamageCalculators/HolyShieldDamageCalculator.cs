using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDamageCalculators;

public class HolyShieldDamageCalculator : DefaultAbilityDamageCalculator
{
    // It's unlikely to hit with the default hit, but not with the special hit
    // We therefore set the minimal damage and DPS to the maximal special damage and DPS

    /// <inheritdoc />
    public HolyShieldDamageCalculator(AbilityServiceProvider abilityServiceProvider) : base(abilityServiceProvider) {}

    /// <inheritdoc />
    public override bool CanHandleAbility(string abilityName)
    {
        return abilityName == "Holy Shield";
    }

    /// <inheritdoc />
    public override DamageRange CalculateBaseDamageRange(string abilityName, AbilityRarity abilityRarity, int playerLevel)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var damageRange = base.CalculateBaseDamageRange(abilityName, abilityRarity, playerLevel);
        return damageRange with
        {
            Min = 0,
            MinDps = 0
        };
    }

    /// <inheritdoc />
    public override DamageRange CalculateSpecialDamageRange(string abilityName, AbilityRarity abilityRarity, int playerLevel)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var damageRange = base.CalculateBaseDamageRange(abilityName, abilityRarity, playerLevel);
        return damageRange with
        {
            Min = damageRange.Max,
            MinDps = damageRange.MaxDps
        };
    }
}
