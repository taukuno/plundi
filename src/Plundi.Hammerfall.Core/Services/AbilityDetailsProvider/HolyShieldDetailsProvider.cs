using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDetailsProvider;

public class HolyShieldDetailsProvider : IAbilityDetailsProvider
{
    private readonly Dictionary<AbilityRarity, decimal> _cooldowns = new()
    {
        { AbilityRarity.Common, 14m },
        { AbilityRarity.Uncommon, 12m },
        { AbilityRarity.Rare, 10m },
        { AbilityRarity.Epic, 8m }
    };

    // The timings for the 2nd and special hits are arbitrary
    // I chose 0.6 s and 0.3 s respectively as an average
    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        {
            AbilityRarity.Common,
            new()
            {
                BaseHits = [new() { Damage = 0.18m, IsRelative = true, Timing = 0m }, new() { Damage = 0.18m, IsRelative = true, Timing = 0.5m }],
                SpecialHits = [new() { Damage = 0.36m, IsRelative = true, Timing = 0.25m }],
                DotHits = []
            }
        },
        {
            AbilityRarity.Uncommon,
            new()
            {
                BaseHits = [new() { Damage = 0.19m, IsRelative = true, Timing = 0m }, new() { Damage = 0.19m, IsRelative = true, Timing = 0.5m }],
                SpecialHits = [new() { Damage = 0.374m, IsRelative = true, Timing = 0.25m }],
                DotHits = []
            }
        },
        {
            AbilityRarity.Rare,
            new()
            {
                BaseHits = [new() { Damage = 0.198m, IsRelative = true, Timing = 0m }, new() { Damage = 0.198m, IsRelative = true, Timing = 0.5m }],
                SpecialHits = [new() { Damage = 0.395m, IsRelative = true, Timing = 0.25m }],
                DotHits = []
            }
        },
        {
            AbilityRarity.Epic,
            new()
            {
                BaseHits = [new() { Damage = 0.2m, IsRelative = true, Timing = 0m }, new() { Damage = 0.2m, IsRelative = true, Timing = 0.5m }],
                SpecialHits = [new() { Damage = 0.409m, IsRelative = true, Timing = 0.25m }],
                DotHits = []
            }
        }
    };

    /// <inheritdoc />
    public bool CanHandleAbility(string abilityName)
    {
        return abilityName == "Holy Shield";
    }

    /// <inheritdoc />
    public string GetDisplayName(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "Holy Shield";
    }

    /// <inheritdoc />
    public decimal GetCastDuration(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return 1m;
    }

    /// <inheritdoc />
    public decimal GetChannelDuration(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the abilityName '{abilityName}'.", nameof(abilityName));
        }

        return 0m;
    }

    /// <inheritdoc />
    public string GetImagePath(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "holy_shield.jpg";
    }

    /// <inheritdoc />
    public bool CanBeCastedDuringGlobalCooldown(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        return false;
    }

    /// <inheritdoc />
    public AbilityType GetAbilityType(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return AbilityType.Damage;
    }

    /// <inheritdoc />
    public IEnumerable<AbilityEffect> GetEffects(string abilityName, AbilityRarity abilityRarity, int characterLevel, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return
        [
            new() { Description = "frontal swirly (bommerangs)", Duration = 0m },
            new() { Description = "recast to activate AoE", Duration = 0m }
        ];
    }

    /// <inheritdoc />
    public decimal GetCooldown(string abilityName, AbilityRarity abilityRarity, int characterLevel, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return _cooldowns[abilityRarity];
    }

    /// <inheritdoc />
    public DamageProfile GetDamageProfile(string abilityName, AbilityRarity abilityRarity, int characterLevel, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var hitSpecial = true;
        var hitOnBoomerang = true;
        if (simulationSettings is not null && simulationSettings.TryGetValue("hit_special", out var hitSpecialSetting))
        {
            hitSpecial = hitSpecialSetting == "Yes";
        }

        if (simulationSettings is not null && simulationSettings.TryGetValue("hit_on_boomerang", out var hitOnBoomerangSetting))
        {
            hitOnBoomerang = hitOnBoomerangSetting == "Yes";
        }

        var damageProfile = _damageProfiles[abilityRarity];
        var adjustedDamageProfile = damageProfile with
        {
            BaseHits = damageProfile.BaseHits.Take(hitOnBoomerang ? 2 : 1).ToList(),
            SpecialHits = damageProfile.SpecialHits.Take(hitSpecial ? 1 : 0).ToList(),
        };

        return adjustedDamageProfile;
    }

    /// <inheritdoc />
    public Dictionary<string, (string Description, List<string> PossibleValues, string DefaultValue)> GetPossibleSimulationSettings(string abilityName)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return new()
        {
            { "hit_special", ("Are you hitting with the special?", ["No", "Yes"], "Yes") },
            { "hit_on_boomerang", ("Is the projectile also hitting on the way back?", ["No", "Yes"], "No") },
        };
    }
}
