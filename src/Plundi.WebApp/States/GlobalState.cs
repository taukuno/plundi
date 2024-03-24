using Plundi.Core.Models;
using Plundi.WebApp.Common.Services;

namespace Plundi.WebApp.States;

public sealed class GlobalState
{
    private readonly LocalStorage _localStorage;
    private readonly List<IAbility> _damageAbilities = [];
    private readonly List<IAbility> _utilityAbilities = [];
    private int _characterLevel = 1;

    public GlobalState(LocalStorage localStorage)
    {
        _localStorage = localStorage;
    }

    public int CharacterLevel
    {
        get => _characterLevel;
        set
        {
            _characterLevel = value < 1 ? 1 : value > 10 ? 10 : value;
            NotifyStateChanged();
        }
    }

    public IEnumerable<IAbility> DamageAbilities => _damageAbilities.AsReadOnly();
    public IEnumerable<IAbility> UtilityAbilities => _utilityAbilities.AsReadOnly();

    public event Action? OnChange;

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }

    public async Task MoveDamageAbilityUpAsync(IAbility ability)
    {
        var index = _damageAbilities.IndexOf(ability);
        if (index <= 0)
        {
            return;
        }

        _damageAbilities.Remove(ability);
        _damageAbilities.Insert(index - 1, ability);

        await _localStorage.SetItemAsync("damageAbilitiesOrder", _damageAbilities.Select(x => x.Name));
        NotifyStateChanged();
    }

    public async Task MoveDamageAbilityDownAsync(IAbility ability)
    {
        var index = _damageAbilities.IndexOf(ability);
        if (index < 0 || index >= _damageAbilities.Count - 1)
        {
            return;
        }

        _damageAbilities.Remove(ability);
        _damageAbilities.Insert(index + 1, ability);

        await _localStorage.SetItemAsync("damageAbilitiesOrder", _damageAbilities.Select(x => x.Name));
        NotifyStateChanged();
    }

    public async Task MoveUtilityAbilityUpAsync(IAbility ability)
    {
        var index = _utilityAbilities.IndexOf(ability);
        if (index <= 0)
        {
            return;
        }

        _utilityAbilities.Remove(ability);
        _utilityAbilities.Insert(index - 1, ability);

        await _localStorage.SetItemAsync("utilityAbilitiesOrder", _utilityAbilities.Select(x => x.Name));
        NotifyStateChanged();
    }

    public async Task MoveUtilityAbilityDownAsync(IAbility ability)
    {
        var index = _utilityAbilities.IndexOf(ability);
        if (index < 0 || index >= _utilityAbilities.Count - 1)
        {
            return;
        }

        _utilityAbilities.Remove(ability);
        _utilityAbilities.Insert(index + 1, ability);

        await _localStorage.SetItemAsync("utilityAbilitiesOrder", _utilityAbilities.Select(x => x.Name));
        NotifyStateChanged();
    }

    public async Task InitializeAbilitiesAsync(List<IAbility> abilities)
    {
        var damageAbilities = abilities.Where(x => x.Type == AbilityType.Damage).ToList();
        var utilityAbilities = abilities.Where(x => x.Type == AbilityType.Utility).ToList();

        var damageAbilitiesOrder = await _localStorage.GetItemAsync<List<string>>("damageAbilitiesOrder");
        var utilityAbilitiesOrder = await _localStorage.GetItemAsync<List<string>>("utilityAbilitiesOrder");

        foreach (var ability in from abilityName in damageAbilitiesOrder ?? [] select damageAbilities.SingleOrDefault(x => x.Name == abilityName))
        {
            damageAbilities.Remove(ability);
            _damageAbilities.Add(ability);
        }

        foreach (var ability in from abilityName in utilityAbilitiesOrder ?? [] select utilityAbilities.SingleOrDefault(x => x.Name == abilityName))
        {
            utilityAbilities.Remove(ability);
            _utilityAbilities.Add(ability);
        }

        _damageAbilities.AddRange(damageAbilities);
        _utilityAbilities.AddRange(utilityAbilities);

        OnChange?.Invoke();
    }
}