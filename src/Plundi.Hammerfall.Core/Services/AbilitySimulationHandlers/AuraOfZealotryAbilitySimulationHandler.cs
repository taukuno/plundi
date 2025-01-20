using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services.AbilitySimulationHandlers;

public class AuraOfZealotrySimulationHandler : DefaultAbilitySimulationHandler
{
    public AuraOfZealotrySimulationHandler(AbilityServiceProvider abilityServiceProvider) : base(abilityServiceProvider) { }

    /// <inheritdoc />
    public override bool CanHandleAbility(string abilityName)
    {
        return abilityName == "Aura of Zealotry";
    }

    /// <inheritdoc />
    public override void UseAbility(string abilityName, AbilityRarity abilityRarity, int playerLevel, LoadoutSimulationContext loadoutSimulationContext)
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

        var abilitySimulationContext = new AbilitySimulationContext
        {
            AbilityName = abilityName,
            AbilityRarity = abilityRarity,
            PlayerLevel = playerLevel,

            AdjustedCastDuration = castDuration,
            AdjustedChannelDuration = 0,
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

        abilitySimulationContext.NextHandlingNeededAt = loadoutSimulationContext.CurrentTime + castDuration;
        loadoutSimulationContext.AbilitySimulationContexts.Add(abilitySimulationContext);

        var meleeAbilityIndex = loadoutSimulationContext.AvailableAbilities.FindIndex(x => x.Name == "Melee");
        if (meleeAbilityIndex != -1)
        {
            loadoutSimulationContext.AvailableAbilities[meleeAbilityIndex] = new()
            {
                Name = "Zealot's Smite",
                Rarity = abilityRarity
            };
        }
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

            var zealotsSmiteAbilityIndex = loadoutSimulationContext.AvailableAbilities.FindIndex(x => x.Name == "Zealot's Smite");
            if (zealotsSmiteAbilityIndex != -1)
            {
                loadoutSimulationContext.AvailableAbilities[zealotsSmiteAbilityIndex] = new()
                {
                    Name = "Melee",
                    Rarity = AbilityRarity.Common
                };
            }
        }

        abilitySimulationContext.NextHandlingNeededAt = abilitySimulationContext.NextHandlingNeededAt > loadoutSimulationContext.CurrentTime
            ? abilitySimulationContext.NextHandlingNeededAt
            : CalculateDurationUntilNextHit(abilitySimulationContext, loadoutSimulationContext);
    }
}
