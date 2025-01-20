using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilitySimulationHandlers;

public class ToxicSmackerelSimulationHandler : DefaultAbilitySimulationHandler
{
    public ToxicSmackerelSimulationHandler(AbilityServiceProvider abilityServiceProvider) : base(abilityServiceProvider) {}

    /// <inheritdoc />
    public override bool CanHandleAbility(string abilityName)
    {
        return abilityName == "Toxic Smackerel";
    }

    /// <inheritdoc />
    public override void UseAbility(string abilityName, AbilityRarity abilityRarity, int playerLevel, LoadoutSimulationContext loadoutSimulationContext)
    {
        if (!CanHandleAbility(abilityName))
        {
            throw new ArgumentException($"Can't handle the ability '{abilityName}'.", nameof(abilityName));
        }

        var alreadyActiveToxicSmackerl = loadoutSimulationContext.AbilitySimulationContexts.FirstOrDefault(x => x.AbilityName == abilityName && !x.IsFinished);
        var applySpecialDamage = alreadyActiveToxicSmackerl is not null;

        var detailsProvider = AbilityServiceProvider.GetAbilityDetailsProvider(abilityName);

        var cooldown = detailsProvider.GetCooldown(abilityName, abilityRarity, playerLevel);
        if (cooldown > 0)
        {
            loadoutSimulationContext.AbilitiesOnCooldown.Add((abilityName, cooldown));
        }

        var castDuration = detailsProvider.GetCastDuration(abilityName);

        var abilitySimulationContext = new AbilitySimulationContext
        {
            AbilityName = abilityName,
            AbilityRarity = abilityRarity,
            PlayerLevel = playerLevel,

            AdjustedCastDuration = castDuration,
            AdjustedChannelDuration = 0m,
            AdjustedDamageProfile = detailsProvider.GetDamageProfile(abilityName, abilityRarity, playerLevel),

            SimulationStartedAt = loadoutSimulationContext.CurrentTime,

            CastStartedAt = loadoutSimulationContext.CurrentTime,
            Payload = (ApplySepcialDamage: applySpecialDamage, AlreadyActiveToxicSmackerl: alreadyActiveToxicSmackerl)
        };

        loadoutSimulationContext.IsCurrentlyCasting = true;
        loadoutSimulationContext.RemainingGlobalCooldown = 1;
        loadoutSimulationContext.Events.Add((loadoutSimulationContext.CurrentTime,
            abilitySimulationContext.AbilityName,
            abilitySimulationContext.AbilityRarity,
            abilitySimulationContext.PlayerLevel,
            "Inital Cast",
            0));

        abilitySimulationContext.NextHandlingNeededAt = loadoutSimulationContext.CurrentTime + castDuration;
        loadoutSimulationContext.AbilitySimulationContexts.Add(abilitySimulationContext);
    }

    /// <inheritdoc />
    public override void HandleActiveAbility(AbilitySimulationContext abilitySimulationContext, LoadoutSimulationContext loadoutSimulationContext)
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
                abilitySimulationContext.ChannelStartedAt = loadoutSimulationContext.CurrentTime;
                abilitySimulationContext.ChannelFinishedAt = loadoutSimulationContext.CurrentTime;
                loadoutSimulationContext.IsCurrentlyCasting = false;

                var payload = ((bool ApplySepcialDamage, AbilitySimulationContext? AlreadyActiveToxicSmackerl))abilitySimulationContext.Payload!;
                if (payload.AlreadyActiveToxicSmackerl is not null)
                {
                    payload.AlreadyActiveToxicSmackerl.IsFinished = true;
                    payload.AlreadyActiveToxicSmackerl.SimulationFinishedAt = loadoutSimulationContext.CurrentTime;
                }
            }
        }

        if (!castIsFinished)
        {
            return;
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

    protected override void ProcessCurrentHits(AbilitySimulationContext abilitySimulationContext, LoadoutSimulationContext loadoutSimulationContext)
    {
        var attackPower = PlayerStatsProvider.GetAttackPower(loadoutSimulationContext.PlayerLevel);
        var adjustedTime = loadoutSimulationContext.CurrentTime - abilitySimulationContext.ChannelStartedAt;
        var currentBaseHit = abilitySimulationContext.AdjustedDamageProfile.BaseHits.FirstOrDefault(x => x.Timing == adjustedTime);
        var currentSpecialHit = abilitySimulationContext.AdjustedDamageProfile.SpecialHits.FirstOrDefault(x => x.Timing == adjustedTime);
        var currentDotHit = abilitySimulationContext.AdjustedDamageProfile.DotHits.FirstOrDefault(x => x.Timing == adjustedTime);

        if (currentBaseHit is not null)
        {
            var damage = currentBaseHit.IsRelative ? currentBaseHit.Damage * attackPower : currentBaseHit.Damage;
            loadoutSimulationContext.Events.Add(
                (
                    loadoutSimulationContext.CurrentTime,
                    abilitySimulationContext.AbilityName,
                    abilitySimulationContext.AbilityRarity,
                    abilitySimulationContext.PlayerLevel,
                    "Hit",
                    damage
                ));
        }

        var payload = ((bool ApplySepcialDamage, AbilitySimulationContext? AlreadyActiveToxicSmackerl))abilitySimulationContext.Payload!;
        if (currentSpecialHit is not null && payload.ApplySepcialDamage)
        {
            var damage = currentSpecialHit.IsRelative ? currentSpecialHit.Damage * attackPower : currentSpecialHit.Damage;
            loadoutSimulationContext.Events.Add(
                (
                    loadoutSimulationContext.CurrentTime,
                    abilitySimulationContext.AbilityName,
                    abilitySimulationContext.AbilityRarity,
                    abilitySimulationContext.PlayerLevel,
                    "Hit (special)",
                    damage
                ));
        }

        if (currentDotHit is not null)
        {
            var damage = currentDotHit.IsRelative ? currentDotHit.Damage * attackPower : currentDotHit.Damage;
            loadoutSimulationContext.Events.Add(
                (
                    loadoutSimulationContext.CurrentTime,
                    abilitySimulationContext.AbilityName,
                    abilitySimulationContext.AbilityRarity,
                    abilitySimulationContext.PlayerLevel,
                    "Hit (dot)",
                    damage
                ));
        }
    }
}
