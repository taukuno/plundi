using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDetailsProvider;

public class ManaSphereDetailsProvider : IAbilityDetailsProvider
{
    private readonly Dictionary<AbilityRarity, decimal> _cooldowns = new()
    {
        { AbilityRarity.Common, 14m },
        { AbilityRarity.Uncommon, 12m },
        { AbilityRarity.Rare, 10m },
        { AbilityRarity.Epic, 8m }
    };

    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        { AbilityRarity.Common, new() { BaseHits = [new() { Damage = 0.56m, IsRelative = true, Timing = 1.8m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [new() { Damage = 0.59m, IsRelative = true, Timing = 1.8m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [new() { Damage = 0.62m, IsRelative = true, Timing = 1.8m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [new() { Damage = 0.65m, IsRelative = true, Timing = 1.8m }], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public bool CanHandleAbility(string abilityName)
    {
        return abilityName == "Mana Sphere";
    }

    /// <inheritdoc />
    public string GetDisplayName(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "Mana Sphere";
    }

    /// <inheritdoc />
    public decimal GetCastDuration(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return 0m;
    }

    /// <inheritdoc />
    public decimal GetChannelDuration(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the abilityName '{abilityName}'.", nameof(abilityName));
        }

        var chargeDuration = 1.8m;
        if (simulationSettings is not null && simulationSettings.TryGetValue("charge_duration", out var chargeDurationSetting))
        {
            chargeDuration = chargeDurationSetting switch
            {
                "Stage 0 (0.6s)" => 0.6m,
                "Stage 1 (1s)" => 1m,
                "Stage 2 (1.4s)" => 1.4m,
                "Stage 3 (1.8s)" => 1.8m,
                _ => 1.8m
            };
        }

        return chargeDuration;
    }

    /// <inheritdoc />
    public string GetImagePath(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "mana_sphere.jpg";
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
            new() { Description = "frontal orb", Duration = 0m },
            new() { Description = "medium knockback on hit", Duration = 0m },
            new() { Description = "size of orb can be charged", Duration = 0m },
            new() { Description = "heavily slowed while channeling (-97%)", Duration = 1.8m }
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

        var chargeDuration = 1.8m;
        if (simulationSettings is not null && simulationSettings.TryGetValue("charge_duration", out var chargeDurationSetting))
        {
            chargeDuration = chargeDurationSetting switch
            {
                "Stage 0 (0.6s)" => 0.6m,
                "Stage 1 (1s)" => 1m,
                "Stage 2 (1.4s)" => 1.4m,
                "Stage 3 (1.8s)" => 1.8m,
                _ => 1.8m
            };
        }

        var damageProfile = _damageProfiles[abilityRarity];
        var adjustedDamageProfile = damageProfile with
        {
            BaseHits = [damageProfile.BaseHits[0] with { Timing = chargeDuration }],
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
            { "charge_duration", ("How long are you charging it?", ["Stage 0 (0.6s)", "Stage 1 (1s)", "Stage 2 (1.4s)", "Stage 3 (1.8s)"], "Stage 0 (0.6s)") },
        };
    }
}
