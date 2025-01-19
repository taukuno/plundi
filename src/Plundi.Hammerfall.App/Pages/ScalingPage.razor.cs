using Microsoft.AspNetCore.Components;
using Plundi.Hammerfall.App.Components;
using Plundi.Hammerfall.Core.Models;
using Plundi.Hammerfall.Core.Services;

namespace Plundi.Hammerfall.App.Pages;

public partial class ScalingPage : ComponentBase
{
    private readonly List<string> _abilitiesToCompare = ["Searing Axe", "Slicing Winds", "Toxic Smackerel"];
    private AbilitiesDamageComparisonChart? _abilitiesDamageComparisonChart;
    private AbilitiesDpsComparisonChart? _abilitiesDpsComparisonChart;
    private AbilitiesCooldownComparisonChart? _abilitiesCooldownComparisonChart;
    private AbilitiesTimeToKillComparisonChart? _abilitiesTimeToKillComparisonChart;

    private int _characterLevel = 1;
    private int _enemyLevel = 1;
    private CharacterStatsChart? _characterStatsChart;
    private SelectAbilityModal? _selectAbilityModal;
    private bool _smoothLines;
    private bool _baseTimeToKillOnDps = true;

    [Inject] private List<string> Abilities { get; set; } = null!;
    [Inject] private IEnumerable<IAbilityDetailsProvider> AbilityDetailsProviders { get; set; } = null!;

    /// <inheritdoc />
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await DisplayChartsAsync();
        }
    }

    private IAbilityDetailsProvider GetAbilityDetailsProvider(string ability)
    {
        return AbilityDetailsProviders.FirstOrDefault(x => x.CanHandleAbility(ability)) ?? throw new InvalidOperationException($"No details provider registered for the ability '{ability}'.");
    }

    private async Task DisplayChartsAsync()
    {
        await Task.Yield();
        while (_characterStatsChart is null ||
               _abilitiesDamageComparisonChart is null ||
               _abilitiesDpsComparisonChart is null ||
               _abilitiesCooldownComparisonChart is null ||
               _abilitiesTimeToKillComparisonChart is null)
        {
            await Task.Delay(10);
        }

        await _characterStatsChart.DrawAsync();
        await _characterStatsChart.UpdateAsync();

        await _abilitiesDamageComparisonChart.DrawAsync();
        await _abilitiesDamageComparisonChart.UpdateAsync();

        await _abilitiesDpsComparisonChart.DrawAsync();
        await _abilitiesDpsComparisonChart.UpdateAsync();

        await _abilitiesCooldownComparisonChart.DrawAsync();
        await _abilitiesCooldownComparisonChart.UpdateAsync();

        await _abilitiesTimeToKillComparisonChart.DrawAsync();
        await _abilitiesTimeToKillComparisonChart.UpdateAsync();
    }

    private async Task AddAbilityForComparisonAsync(string ability)
    {
        if (_abilitiesToCompare.Contains(ability))
        {
            return;
        }

        _abilitiesToCompare.Add(ability);
        await DisplayChartsAsync();
    }

    private async Task RemoveAbilityFromComparisonAsync(string ability)
    {
        _abilitiesToCompare.Remove(ability);
        await DisplayChartsAsync();
    }

    private async Task SetCharacterLevelAsync(int characterLevel)
    {
        _characterLevel = characterLevel;
        await DisplayChartsAsync();
    }

    private async Task SetEnemyLevelAsync(int enemyLevel)
    {
        _enemyLevel = enemyLevel;
        await DisplayChartsAsync();
    }

    private async Task SetSmoothLinesAsync(bool smoothLines)
    {
        _smoothLines = smoothLines;
        await DisplayChartsAsync();
    }

    private async Task SetBaseTimeToKillOnDpsAsync(bool baseTimeToKillOnDps)
    {
        _baseTimeToKillOnDps = baseTimeToKillOnDps;
        await DisplayChartsAsync();
    }
}
