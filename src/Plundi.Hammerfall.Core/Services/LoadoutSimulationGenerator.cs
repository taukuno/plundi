using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services;

public class LoadoutSimulationGenerator
{
    private readonly AbilityServiceProvider _abilityServiceProvider;

    public LoadoutSimulationGenerator(AbilityServiceProvider abilityServiceProvider)
    {
        _abilityServiceProvider = abilityServiceProvider;
    }

    public LoadoutSimulationReport GenerateSimulationReport(Loadout loadout, decimal simulationDuration, int playerLevel, Dictionary<string, Dictionary<string, string>>? abilitySettings = null)
    {
        var context = new LoadoutSimulationContext
        {
            Loadout = loadout,
            AvailableAbilities = loadout.Abilities,
            SimulationDuration = simulationDuration,
            PlayerLevel = playerLevel,
            AbilitySettings = abilitySettings ?? new(),
        };

        while (context.CurrentTime <= context.SimulationDuration)
        {
            HandleActiveAbilities(context);
            UseAbilitiesIfPossible(context);

            var nextHandlingNeededAt = CalculateWhenNextHandlingIsNeeded(context);

            if (nextHandlingNeededAt <= context.CurrentTime)
            {
                ReduceCooldowns(context, 0.05m);
                context.CurrentTime += 0.05m;
            }
            else
            {
                var durationUntilNextHandling = nextHandlingNeededAt - context.CurrentTime;
                ReduceCooldowns(context, durationUntilNextHandling);
                context.CurrentTime = nextHandlingNeededAt;
            }
        }

        for (; context.CurrentTime <= context.SimulationDuration; context.CurrentTime += 0.05m)
        {
            ReduceCooldowns(context, 0.05m);
            HandleActiveAbilities(context);
            UseAbilitiesIfPossible(context);
        }

        return new()
        {
            Loadout = loadout,
            SimulationDuration = simulationDuration,
            PlayerLevel = playerLevel,
            TotalDamage = context.Events.Sum(x => x.Damage),
            Events = context.Events
        };
    }

    private decimal CalculateWhenNextHandlingIsNeeded(LoadoutSimulationContext context)
    {
        var unfinishedAbilities = context.AbilitySimulationContexts.Where(x => !x.IsFinished).ToList();
        var earliestAbilityHandlingNeededAt = unfinishedAbilities.Count > 0 ? unfinishedAbilities.Min(x => x.NextHandlingNeededAt) : -1;
        var globalCooldownFinishedAt = context.RemainingGlobalCooldown > 0 ? context.RemainingGlobalCooldown + context.CurrentTime : -1;
        var anyAbilityCastableDuringGlobalCooldown = context.AvailableAbilities.Any(x =>
        {
            var detailsProvider = _abilityServiceProvider.GetAbilityDetailsProvider(x.Name);
            return detailsProvider.CanBeCastedDuringGlobalCooldown(x.Name, context.AbilitySettings.GetValueOrDefault(x.Name));
        });

        var nextGlobalHandlingNeededAt = context.IsCurrentlyCasting || context.IsCurrentlyChanneling ? earliestAbilityHandlingNeededAt : anyAbilityCastableDuringGlobalCooldown ? context.CurrentTime : globalCooldownFinishedAt;

        return earliestAbilityHandlingNeededAt switch
        {
            -1 when nextGlobalHandlingNeededAt == -1 => -1,
            -1 => nextGlobalHandlingNeededAt,
            _ => nextGlobalHandlingNeededAt == -1 ? earliestAbilityHandlingNeededAt : Math.Min(nextGlobalHandlingNeededAt, earliestAbilityHandlingNeededAt)
        };
    }

    private void HandleActiveAbilities(LoadoutSimulationContext loadoutSimulationContext)
    {
        foreach (var activeAbility in loadoutSimulationContext.AbilitySimulationContexts.Where(x => !x.IsFinished))
        {
            var simulationHandler = _abilityServiceProvider.GetAbilitySimulationHandler(activeAbility.AbilityName);
            simulationHandler.HandleActiveAbility(activeAbility, loadoutSimulationContext);
        }
    }

    private void ReduceCooldowns(LoadoutSimulationContext context, decimal duration)
    {
        if (context.RemainingGlobalCooldown > 0)
        {
            context.RemainingGlobalCooldown -= duration;
        }

        for (var i = 0; i < context.AbilitiesOnCooldown.Count; i++)
        {
            context.AbilitiesOnCooldown[i] = (context.AbilitiesOnCooldown[i].AbilityName, context.AbilitiesOnCooldown[i].RemainingCooldown - duration);
        }

        var abilitiesOnCooldown = context.AbilitiesOnCooldown.Where(x => x.RemainingCooldown > 0).ToList();
        context.AbilitiesOnCooldown = abilitiesOnCooldown;
    }

    private void UseAbilitiesIfPossible(LoadoutSimulationContext loadoutSimulationContext)
    {
        for (var i = 0; i < loadoutSimulationContext.AvailableAbilities.Count; i++)
        {
            var ability = loadoutSimulationContext.AvailableAbilities[i];
            var detailsProvider = _abilityServiceProvider.GetAbilityDetailsProvider(ability.Name);
            var canBeCastedDuringGlobalCooldown = detailsProvider.CanBeCastedDuringGlobalCooldown(ability.Name, loadoutSimulationContext.AbilitySettings.GetValueOrDefault(ability.Name));

            var playerIsCastingOrChanneling = loadoutSimulationContext.IsCurrentlyCasting || loadoutSimulationContext.IsCurrentlyChanneling;
            var playerIsOnGlobalCooldown = loadoutSimulationContext.RemainingGlobalCooldown > 0;
            if (playerIsCastingOrChanneling || (playerIsOnGlobalCooldown && !canBeCastedDuringGlobalCooldown))
            {
                continue;
            }

            var abilityIsUsable = loadoutSimulationContext.AbilitiesOnCooldown.All(x => x.AbilityName != ability.Name);
            if (!abilityIsUsable)
            {
                continue;
            }

            var simulationHandler = _abilityServiceProvider.GetAbilitySimulationHandler(ability.Name);
            simulationHandler.UseAbility(ability.Name, ability.Rarity, loadoutSimulationContext.PlayerLevel, loadoutSimulationContext);
        }
    }
}
