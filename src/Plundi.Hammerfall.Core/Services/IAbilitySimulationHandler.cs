using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.Core.Services;

public interface IAbilitySimulationHandler
{
    bool CanHandleAbility(string abilityName);

    void UseAbility(string abilityName, AbilityRarity abilityRarity, int characterLevel, LoadoutSimulationContext loadoutSimulationContext);
    void HandleActiveAbility(AbilitySimulationContext abilitySimulationContext, LoadoutSimulationContext loadoutSimulationContext);
}
