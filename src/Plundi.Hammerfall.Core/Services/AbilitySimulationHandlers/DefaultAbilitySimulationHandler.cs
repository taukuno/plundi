using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilitySimulationHandlers;

public class DefaultAbilitySimulationHandler : IAbilitySimulationHandler
{
    protected readonly AbilityServiceProvider AbilityServiceProvider;

    private readonly HashSet<string> _handledAbilities =
    [
        "Celestial Barrage",
        "Earthbreaker",
        "Fire Whirl",
        "Holy Shield",
        "Mana Sphere",
        "Rime Arrow",
        "Searing Axe",
        "Slicing Winds",
        "Star Bomb",
        "Storm Archon",
        "Call Galefeather",
        "Explosive Caltrops",
        "Fade to Shadow",
        "Faeform",
        "Hunter's Chains",
        "Lightning Bulwark",
        "Quaking Leap",
        "Repel",
        "Snowdrift",
        "Steel Traps",
        "Windstorm",
        "Void Tear",
        "Slash",
        "Zealot's Smite"
    ];

    public DefaultAbilitySimulationHandler(AbilityServiceProvider abilityServiceProvider)
    {
        AbilityServiceProvider = abilityServiceProvider;
    }

    /// <inheritdoc />
    public virtual bool CanHandleAbility(string abilityName)
    {
        return _handledAbilities.Contains(abilityName);
    }

    /// <inheritdoc />
    public virtual void UseAbility(string abilityName, AbilityRarity abilityRarity, int playerLevel, LoadoutSimulationContext loadoutSimulationContext)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var detailsProvider = AbilityServiceProvider.GetAbilityDetailsProvider(abilityName);

        var cooldown = detailsProvider.GetCooldown(abilityName, abilityRarity, playerLevel);
        if (cooldown > 0)
        {
            loadoutSimulationContext.AbilitiesOnCooldown.Add((abilityName, cooldown));
        }

        loadoutSimulationContext.AbilitySettings.TryGetValue(abilityName, out var abilitySettings);

        var castDuration = detailsProvider.GetCastDuration(abilityName, abilitySettings);
        var channelDuration = detailsProvider.GetChannelDuration(abilityName, abilitySettings);

        var abilityIsCasted = castDuration > 0;
        var abilityIsChanneled = channelDuration > 0;

        var abilitySimulationContext = new AbilitySimulationContext
        {
            AbilityName = abilityName,
            AbilityRarity = abilityRarity,
            PlayerLevel = playerLevel,

            AdjustedCastDuration = castDuration,
            AdjustedChannelDuration = channelDuration,
            AdjustedDamageProfile = detailsProvider.GetDamageProfile(abilityName, abilityRarity, playerLevel, abilitySettings),

            SimulationStartedAt = loadoutSimulationContext.CurrentTime,

            CastStartedAt = loadoutSimulationContext.CurrentTime,
        };

        loadoutSimulationContext.IsCurrentlyCasting = true;
        loadoutSimulationContext.RemainingGlobalCooldown = 1;
        loadoutSimulationContext.Events.Add((loadoutSimulationContext.CurrentTime,
            abilitySimulationContext.AbilityName,
            abilitySimulationContext.AbilityRarity,
            abilitySimulationContext.PlayerLevel,
            "Inital Cast",
            0));

        if (abilityIsCasted)
        {
            abilitySimulationContext.NextHandlingNeededAt = loadoutSimulationContext.CurrentTime + castDuration;
        }
        else
        {
            loadoutSimulationContext.IsCurrentlyCasting = false;
            loadoutSimulationContext.IsCurrentlyChanneling = true;

            abilitySimulationContext.CastFinishedAt = loadoutSimulationContext.CurrentTime;
            abilitySimulationContext.ChannelStartedAt = loadoutSimulationContext.CurrentTime;
            ProcessCurrentHits(abilitySimulationContext, loadoutSimulationContext);

            var durationUntilNextHit = CalculateDurationUntilNextHit(abilitySimulationContext, loadoutSimulationContext);
            abilitySimulationContext.NextHandlingNeededAt = durationUntilNextHit != -1 ? loadoutSimulationContext.CurrentTime + durationUntilNextHit : -1;
        }

        if (!abilityIsCasted && !abilityIsChanneled)
        {
            loadoutSimulationContext.IsCurrentlyChanneling = false;

            abilitySimulationContext.ChannelFinishedAt = loadoutSimulationContext.CurrentTime;
        }

        var isFinished = IsAbilityFinished(abilitySimulationContext, loadoutSimulationContext);
        if (isFinished)
        {
            abilitySimulationContext.IsFinished = true;
            abilitySimulationContext.SimulationFinishedAt = loadoutSimulationContext.CurrentTime;
        }

        loadoutSimulationContext.AbilitySimulationContexts.Add(abilitySimulationContext);
    }

    /// <inheritdoc />
    public virtual void HandleActiveAbility(AbilitySimulationContext abilitySimulationContext, LoadoutSimulationContext loadoutSimulationContext)
    {
        if (!CanHandleAbility(abilitySimulationContext.AbilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilitySimulationContext.AbilityName}'.", nameof(abilitySimulationContext));
        }

        var castIsFinished = abilitySimulationContext.CastFinishedAt > -1;
        if (!castIsFinished)
        {
            var durationSinceCastingStarted = loadoutSimulationContext.CurrentTime - abilitySimulationContext.CastStartedAt;
            if (durationSinceCastingStarted >= abilitySimulationContext.AdjustedCastDuration)
            {
                castIsFinished = true;
                abilitySimulationContext.CastFinishedAt = loadoutSimulationContext.CurrentTime;
                loadoutSimulationContext.IsCurrentlyCasting = false;
            }
        }

        if (!castIsFinished)
        {
            return;
        }

        var channelHasStarted = abilitySimulationContext.ChannelStartedAt > -1;
        if (!channelHasStarted)
        {
            abilitySimulationContext.ChannelStartedAt = loadoutSimulationContext.CurrentTime;
            loadoutSimulationContext.IsCurrentlyChanneling = true;
        }

        var channelIsFinished = abilitySimulationContext.ChannelFinishedAt > -1;
        if (!channelIsFinished)
        {
            var durationSinceChannelStarted = loadoutSimulationContext.CurrentTime - abilitySimulationContext.ChannelStartedAt;
            if (durationSinceChannelStarted >= abilitySimulationContext.AdjustedChannelDuration)
            {
                abilitySimulationContext.ChannelFinishedAt = loadoutSimulationContext.CurrentTime;
                loadoutSimulationContext.IsCurrentlyChanneling = false;
            }
        }

        ProcessCurrentHits(abilitySimulationContext, loadoutSimulationContext);

        if (IsAbilityFinished(abilitySimulationContext, loadoutSimulationContext))
        {
            abilitySimulationContext.IsFinished = true;
            abilitySimulationContext.SimulationFinishedAt = loadoutSimulationContext.CurrentTime;
        }

        abilitySimulationContext.NextHandlingNeededAt = abilitySimulationContext.NextHandlingNeededAt > loadoutSimulationContext.CurrentTime
            ? abilitySimulationContext.NextHandlingNeededAt
            : CalculateDurationUntilNextHit(abilitySimulationContext, loadoutSimulationContext);
    }

    protected virtual bool IsAbilityFinished(AbilitySimulationContext abilitySimulationContext, LoadoutSimulationContext loadoutSimulationContext)
    {
        if (abilitySimulationContext.CastFinishedAt == -1 || abilitySimulationContext.ChannelFinishedAt == -1)
        {
            return false;
        }

        var latestBaseHit = abilitySimulationContext.AdjustedDamageProfile.BaseHits.Count != 0 ? abilitySimulationContext.AdjustedDamageProfile.BaseHits.Max(x => x.Timing) : 0;
        var latestSpecialHit = abilitySimulationContext.AdjustedDamageProfile.SpecialHits.Count != 0 ? abilitySimulationContext.AdjustedDamageProfile.SpecialHits.Max(x => x.Timing) : 0;
        var latestDotHit = abilitySimulationContext.AdjustedDamageProfile.DotHits.Count != 0 ? abilitySimulationContext.AdjustedDamageProfile.DotHits.Max(x => x.Timing) : 0;

        var latestEvent = new List<decimal> { latestBaseHit, latestSpecialHit, latestDotHit }.Max();
        return abilitySimulationContext.CastFinishedAt + latestEvent <= loadoutSimulationContext.CurrentTime;
    }

    protected virtual void ProcessCurrentHits(AbilitySimulationContext abilitySimulationContext, LoadoutSimulationContext loadoutSimulationContext)
    {
        var attackPower = PlayerStatsProvider.GetAttackPower(loadoutSimulationContext.PlayerLevel);
        var adjustedTime = loadoutSimulationContext.CurrentTime - abilitySimulationContext.ChannelStartedAt;
        var currentBaseHit = abilitySimulationContext.AdjustedDamageProfile.BaseHits.FirstOrDefault(x => x.Timing == adjustedTime);
        var currentSpecialHit = abilitySimulationContext.AdjustedDamageProfile.SpecialHits.FirstOrDefault(x => x.Timing == adjustedTime);
        var currentDotHit = abilitySimulationContext.AdjustedDamageProfile.DotHits.FirstOrDefault(x => x.Timing == adjustedTime);

        if (currentBaseHit is not null)
        {
            var damage = currentBaseHit.IsRelative ? currentBaseHit.Damage * attackPower : currentBaseHit.Damage;
            AddHitEvent("Hit", damage);
        }

        if (currentSpecialHit is not null)
        {
            var damage = currentSpecialHit.IsRelative ? currentSpecialHit.Damage * attackPower : currentSpecialHit.Damage;
            AddHitEvent("Hit (special)", damage);
        }

        if (currentDotHit is not null)
        {
            var damage = currentDotHit.IsRelative ? currentDotHit.Damage * attackPower : currentDotHit.Damage;
            AddHitEvent("Hit (dot)", damage);
        }

        return;

        void AddHitEvent(string @event, decimal damage)
        {
            if(damage == 0)
            {
                return;
            }

            loadoutSimulationContext.Events.Add(
                (
                    loadoutSimulationContext.CurrentTime,
                    abilitySimulationContext.AbilityName,
                    abilitySimulationContext.AbilityRarity,
                    abilitySimulationContext.PlayerLevel,
                    @event,
                    damage
                ));
        }
    }

    protected virtual decimal CalculateDurationUntilNextHit(AbilitySimulationContext abilitySimulationContext, LoadoutSimulationContext loadoutSimulationContext)
    {
        var adjustedTime = loadoutSimulationContext.CurrentTime - abilitySimulationContext.ChannelStartedAt;
        var nextBaseHitTiming = abilitySimulationContext.AdjustedDamageProfile.BaseHits.FirstOrDefault(x => x.Timing > adjustedTime)?.Timing ?? -1;
        var nextSpecialHitTiming = abilitySimulationContext.AdjustedDamageProfile.SpecialHits.FirstOrDefault(x => x.Timing > adjustedTime)?.Timing ?? -1;
        var nextDotHitTiming = abilitySimulationContext.AdjustedDamageProfile.DotHits.FirstOrDefault(x => x.Timing > adjustedTime)?.Timing ?? -1;

        var durationUntilNextHit = -1m;

        if (nextBaseHitTiming != -1)
        {
            durationUntilNextHit = nextBaseHitTiming - adjustedTime;
        }

        if (nextSpecialHitTiming != -1)
        {
            var durationUntilNextSpecialHit = nextSpecialHitTiming - adjustedTime;
            if (durationUntilNextSpecialHit < durationUntilNextHit || durationUntilNextHit == -1)
            {
                durationUntilNextHit = durationUntilNextSpecialHit;
            }
        }

        if (nextDotHitTiming != -1)
        {
            var durationUntilNextDotHit = nextDotHitTiming - adjustedTime;
            if (durationUntilNextDotHit < durationUntilNextHit || durationUntilNextHit == -1)
            {
                durationUntilNextHit = durationUntilNextDotHit;
            }
        }

        return durationUntilNextHit;
    }
}
