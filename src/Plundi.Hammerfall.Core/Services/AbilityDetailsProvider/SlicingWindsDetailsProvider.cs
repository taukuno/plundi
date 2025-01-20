using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilityDetailsProvider;

public class SlicingWindsDetailsProvider : IAbilityDetailsProvider
{
    private readonly Dictionary<AbilityRarity, decimal> _cooldowns = new()
    {
        { AbilityRarity.Common, 14m },
        { AbilityRarity.Uncommon, 12m },
        { AbilityRarity.Rare, 10m },
        { AbilityRarity.Epic, 8m }
    };

    // The hit timing is the average between an instant cast and a fully charged one
    private readonly Dictionary<AbilityRarity, DamageProfile> _damageProfiles = new()
    {
        { AbilityRarity.Common, new() { BaseHits = [new() { Damage = 0.494m, IsRelative = true, Timing = 1.4m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Uncommon, new() { BaseHits = [new() { Damage = 0.522m, IsRelative = true, Timing = 1.4m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Rare, new() { BaseHits = [new() { Damage = 0.55m, IsRelative = true, Timing = 1.4m }], SpecialHits = [], DotHits = [] } },
        { AbilityRarity.Epic, new() { BaseHits = [new() { Damage = 0.579m, IsRelative = true, Timing = 1.4m }], SpecialHits = [], DotHits = [] } }
    };

    /// <inheritdoc />
    public bool CanHandleAbility(string abilityName)
    {
        return abilityName == "Slicing Winds";
    }

    /// <inheritdoc />
    public string GetDisplayName(string abilityName, Dictionary<string, string>? simulationSettings = null)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        return "Slicing Winds";
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

        var chargeDuration = 1.4m;
        if (simulationSettings is not null && simulationSettings.TryGetValue("charge_duration", out var chargeDurationSetting))
        {
            chargeDuration = chargeDurationSetting switch
            {
                "Stage 0 (0.35s)" => 0.35m,
                "Stage 1 (0.7s)" => 0.7m,
                "Stage 2 (1.05s)" => 1.05m,
                "Stage 3 (1.4s)" => 1.4m,
                _ => 1.4m
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

        return "slicing_winds.jpg";
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

        return abilityRarity switch
        {
            AbilityRarity.Common =>
            [
                new() { Description = "frontal dash", Duration = 0m },
                new() { Description = "dash can be charged up to 12 yards", Duration = 0m },
                new() { Description = "heavily slowed while channeling (-97%)", Duration = 1.4m }
            ],
            AbilityRarity.Uncommon =>
            [
                new() { Description = "frontal dash", Duration = 0m },
                new() { Description = "dash can be charged up to 20 yards", Duration = 0m },
                new() { Description = "heavily slowed while channeling (-97%)", Duration = 1.4m }
            ],
            AbilityRarity.Rare =>
            [
                new() { Description = "frontal dash", Duration = 0m },
                new() { Description = "dash can be charged up to 28 yards", Duration = 0m },
                new() { Description = "heavily slowed while channeling (-97%)", Duration = 1.4m }
            ],
            AbilityRarity.Epic =>
            [
                new() { Description = "frontal dash", Duration = 0m },
                new() { Description = "dash can be charged up to 36 yards", Duration = 0m },
                new() { Description = "heavily slowed while channeling (-97%)", Duration = 1.4m }
            ],
            _ =>
            [
                new() { Description = "frontal dash", Duration = 0m },
                new() { Description = "dash can be charged up to 12 yards", Duration = 0m },
                new() { Description = "heavily slowed while channeling (-97%)", Duration = 1.4m }
            ]
        };
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

        var chargeDuration = 1.4m;
        if (simulationSettings is not null && simulationSettings.TryGetValue("charge_duration", out var chargeDurationSetting))
        {
            chargeDuration = chargeDurationSetting switch
            {
                "Stage 0 (0.35s)" => 0.35m,
                "Stage 1 (0.7s)" => 0.7m,
                "Stage 2 (1.05s)" => 1.05m,
                "Stage 3 (1.4s)" => 1.4m,
                _ => 1.4m
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
            { "charge_duration", ("How long are you charging it?", ["Stage 0 (0.35s)", "Stage 1 (0.7s)", "Stage 2 (1.05s)", "Stage 3 (1.5s)"], "Stage 0 (0.35s)") },
        };
    }
}
