namespace Plundi.Hammerfall.Core.Services;

public class AbilityServiceProvider
{
    private readonly IServiceProvider _serviceProvider;

    private IEnumerable<IAbilityDetailsProvider>? _abilityDetailsProviders;
    private IEnumerable<IAbilityDamageCalculator>? _abilityDamageCalculators;
    private IEnumerable<IAbilitySimulationHandler>? _abilitySimulationHandlers;

    private readonly Dictionary<string, IAbilityDetailsProvider> _abilityDetailsProviderCache = new();
    private readonly Dictionary<string, IAbilityDamageCalculator> _abilityDamageCalculatorsCache = new();
    private readonly Dictionary<string, IAbilitySimulationHandler> _abilitySimulationHandlersCache = new();

    public AbilityServiceProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IAbilityDetailsProvider GetAbilityDetailsProvider(string abilityName)
    {
        if (_abilityDetailsProviderCache.TryGetValue(abilityName, out var abilityDetailsProvider))
        {
            return abilityDetailsProvider;
        }

        _abilityDetailsProviders ??= (IEnumerable<IAbilityDetailsProvider>)_serviceProvider.GetService(typeof(IEnumerable<IAbilityDetailsProvider>))!;
        abilityDetailsProvider = _abilityDetailsProviders.FirstOrDefault(x => x.CanHandleAbility(abilityName));
        if (abilityDetailsProvider == null)
        {
            throw new ArgumentException($"No ability details provider provided for the ability '{abilityName}'.", nameof(abilityName));
        }

        _abilityDetailsProviderCache.Add(abilityName, abilityDetailsProvider);
        return abilityDetailsProvider;
    }

    public IAbilityDamageCalculator GetAbilityDamageCalculator(string abilityName)
    {
        if (_abilityDamageCalculatorsCache.TryGetValue(abilityName, out var abilityDamageCalculator))
        {
            return abilityDamageCalculator;
        }

        _abilityDamageCalculators ??= (IEnumerable<IAbilityDamageCalculator>)_serviceProvider.GetService(typeof(IEnumerable<IAbilityDamageCalculator>))!;
        abilityDamageCalculator = _abilityDamageCalculators.FirstOrDefault(x => x.CanHandleAbility(abilityName));
        if (abilityDamageCalculator == null)
        {
            throw new ArgumentException($"No ability damage calculator provided for the ability '{abilityName}'.", nameof(abilityName));
        }

        _abilityDamageCalculatorsCache.Add(abilityName, abilityDamageCalculator);
        return abilityDamageCalculator;
    }

    public IAbilitySimulationHandler GetAbilitySimulationHandler(string abilityName)
    {
        if (_abilitySimulationHandlersCache.TryGetValue(abilityName, out var abilitySimulationHandler))
        {
            return abilitySimulationHandler;
        }

        _abilitySimulationHandlers ??= (IEnumerable<IAbilitySimulationHandler>)_serviceProvider.GetService(typeof(IEnumerable<IAbilitySimulationHandler>))!;
        abilitySimulationHandler = _abilitySimulationHandlers.FirstOrDefault(x => x.CanHandleAbility(abilityName));
        if (abilitySimulationHandler == null)
        {
            throw new ArgumentException($"No ability simulation handler provided for the ability '{abilityName}'.", nameof(abilityName));
        }

        _abilitySimulationHandlersCache.Add(abilityName, abilitySimulationHandler);
        return abilitySimulationHandler;
    }
}
