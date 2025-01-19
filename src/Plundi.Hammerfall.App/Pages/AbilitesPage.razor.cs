using Microsoft.AspNetCore.Components;
using Plundi.Hammerfall.App.Models;
using Plundi.Hammerfall.App.Services;
using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.App.Pages;

public partial class AbilitesPage : ComponentBase
{
    private const string DamageAbilitiesOrderStorageKey = "damageAbilitiesOrder";
    private const string UtilityAbilitiesOrderStorageKey = "utilityAbilitiesOrder";
    private readonly List<RarifiedAbility> _damageAbilities = [];
    private readonly List<RarifiedAbility> _utilityAbilities = [];

    private int _characterLevel = 1;
    private bool _displayDamageAbilitiesInCompactView;
    private bool _displayUtilityAbilitiesInCompactView;

    [Inject] private List<IAbility> Abilities { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private LocalStorage LocalStorage { get; set; } = null!;

    /// <inheritdoc />
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await InitializeAbilitiesAsync();
            StateHasChanged();
        }
    }

    private async Task InitializeAbilitiesAsync()
    {
        var damageAbilities = Abilities.Where(x => x.Type == AbilityType.Damage).ToList();
        var utilityAbilities = Abilities.Where(x => x.Type == AbilityType.Utility).ToList();

        var damageAbilitiesOrder = await LocalStorage.GetItemAsync<List<string>>(DamageAbilitiesOrderStorageKey);
        var utilityAbilitiesOrder = await LocalStorage.GetItemAsync<List<string>>(UtilityAbilitiesOrderStorageKey);

        foreach (var ability in from abilityName in damageAbilitiesOrder ?? []
                 select damageAbilities.SingleOrDefault(x => x.Name == abilityName))
        {
            damageAbilities.Remove(ability);
            _damageAbilities.Add(new(ability, AbilityRarity.Common));
        }

        foreach (var ability in from abilityName in utilityAbilitiesOrder ?? []
                 select utilityAbilities.SingleOrDefault(x => x.Name == abilityName))
        {
            utilityAbilities.Remove(ability);
            _utilityAbilities.Add(new(ability, AbilityRarity.Common));
        }

        _damageAbilities.AddRange(damageAbilities.Select(x => new RarifiedAbility(x, AbilityRarity.Common)));
        _utilityAbilities.AddRange(utilityAbilities.Select(x => new RarifiedAbility(x, AbilityRarity.Common)));
    }

    private void SetAllAbilityRarities(AbilityRarity rarity)
    {
        foreach (var ability in _damageAbilities.Concat(_utilityAbilities))
        {
            ability.Rarity = rarity;
        }
    }

    private static void MoveAbility(RarifiedAbility ability, List<RarifiedAbility> inList, int offset)
    {
        var originalIndex = inList.IndexOf(ability);
        if (originalIndex == -1)
        {
            return;
        }

        var maxAllowedIndex = inList.Count - 1;
        var newIndex = originalIndex + offset;
        newIndex = newIndex < 0 ? 0 : newIndex > maxAllowedIndex ? maxAllowedIndex : newIndex;

        inList.RemoveAt(originalIndex);
        inList.Insert(newIndex, ability);
    }

    private async Task MoveDamageAbilityAsync(RarifiedAbility ability, MoveDirection direction)
    {
        MoveAbility(ability, _damageAbilities, (int)direction);
        await LocalStorage.SetItemAsync(DamageAbilitiesOrderStorageKey, _damageAbilities.Select(x => x.Name));
    }

    private async Task MoveUtilityAbilityAsync(RarifiedAbility ability, MoveDirection direction)
    {
        MoveAbility(ability, _utilityAbilities, (int)direction);
        await LocalStorage.SetItemAsync(UtilityAbilitiesOrderStorageKey, _utilityAbilities.Select(x => x.Name));
    }

    private enum MoveDirection
    {
        Up = -1,
        Down = 1
    }
}