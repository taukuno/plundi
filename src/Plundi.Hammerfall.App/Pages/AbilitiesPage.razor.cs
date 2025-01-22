using Microsoft.AspNetCore.Components;
using Plundi.Hammerfall.App.Services;
using Plundi.Hammerfall.Core.Models;
using Plundi.Hammerfall.Core.Services;

namespace Plundi.Hammerfall.App.Pages;

public partial class AbilitiesPage : ComponentBase
{
    private const string DamageAbilitiesOrderStorageKey = "damageAbilitiesOrder";
    private const string UtilityAbilitiesOrderStorageKey = "utilityAbilitiesOrder";
    private const string MeleeAbilitiesOrderStorageKey = "utilityAbilitiesOrder";
    private readonly List<RarifiedAbility> _damageAbilities = [];
    private readonly List<RarifiedAbility> _utilityAbilities = [];
    private readonly List<RarifiedAbility> _meleeAbilities = [];

    private int _playerLevel = 10;
    private bool _displayDamageAbilitiesInCompactView;
    private bool _displayUtilityAbilitiesInCompactView;
    private bool _displayMeleeAbilitiesInCompactView;

    [Inject] private List<string> Abilities { get; set; } = null!;
    [Inject] private AbilityServiceProvider AbilityServiceProvider { get; set; } = null!;
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
        var damageAbilities = Abilities.Where(x => AbilityServiceProvider.GetAbilityDetailsProvider(x).GetAbilityType(x) == AbilityType.Damage).ToList();
        var utilityAbilities = Abilities.Where(x => AbilityServiceProvider.GetAbilityDetailsProvider(x).GetAbilityType(x) == AbilityType.Utility).ToList();
        var meleeAbilities = Abilities.Where(x => AbilityServiceProvider.GetAbilityDetailsProvider(x).GetAbilityType(x) == AbilityType.Melee).ToList();

        var damageAbilitiesOrder = await LocalStorage.GetItemAsync<List<string>>(DamageAbilitiesOrderStorageKey);
        var utilityAbilitiesOrder = await LocalStorage.GetItemAsync<List<string>>(UtilityAbilitiesOrderStorageKey);
        var meleeAbilitiesOrder = await LocalStorage.GetItemAsync<List<string>>(MeleeAbilitiesOrderStorageKey);

        foreach (var ability in damageAbilitiesOrder ?? [])
        {
            damageAbilities.Remove(ability);
            _damageAbilities.Add(new() {Name = ability, Rarity = AbilityRarity.Epic });
        }

        foreach (var ability in utilityAbilitiesOrder ?? [])
        {
            utilityAbilities.Remove(ability);
            _utilityAbilities.Add(new() {Name = ability, Rarity = AbilityRarity.Epic });
        }

        foreach (var ability in meleeAbilitiesOrder ?? [])
        {
            meleeAbilities.Remove(ability);
            _utilityAbilities.Add(new() {Name = ability, Rarity = AbilityRarity.Epic });
        }

        _damageAbilities.AddRange(damageAbilities.Select(x => new RarifiedAbility {Name = x, Rarity = AbilityRarity.Epic } ));
        _utilityAbilities.AddRange(utilityAbilities.Select(x => new RarifiedAbility {Name = x, Rarity = AbilityRarity.Epic } ));
        _meleeAbilities.AddRange(meleeAbilities.Select(x => new RarifiedAbility {Name = x, Rarity = AbilityRarity.Epic } ));
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

    private async Task MoveMeleeAbilityAsync(RarifiedAbility ability, MoveDirection direction)
    {
        MoveAbility(ability, _meleeAbilities, (int)direction);
        await LocalStorage.SetItemAsync(MeleeAbilitiesOrderStorageKey, _meleeAbilities.Select(x => x.Name));
    }

    private enum MoveDirection
    {
        Up = -1,
        Down = 1
    }
}
