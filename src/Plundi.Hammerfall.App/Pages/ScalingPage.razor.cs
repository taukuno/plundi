using Microsoft.AspNetCore.Components;
using Plundi.Hammerfall.App.Components;
using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.App.Pages;

public partial class ScalingPage : ComponentBase
{
    private readonly List<IAbility> _abilitiesToCompare = [];
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

    [Inject] private List<IAbility> Abilities { get; set; } = null!;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        base.OnInitialized();

        var searingAxe = Abilities.SingleOrDefault(x => x.Name == "Searing Axe");
        var slicingWinds = Abilities.SingleOrDefault(x => x.Name == "Slicing Winds");
        var toxicSmackerel = Abilities.SingleOrDefault(x => x.Name == "Toxic Smackerel");

        if (searingAxe is null || slicingWinds is null || toxicSmackerel is null)
        {
            return;
        }

        _abilitiesToCompare.AddRange([searingAxe, slicingWinds, toxicSmackerel]);
    }

    /// <inheritdoc />
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await DisplayChartsAsync();
        }
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

    private async Task AddAbilityForComparisonAsync(IAbility ability)
    {
        if (_abilitiesToCompare.Contains(ability))
        {
            return;
        }

        _abilitiesToCompare.Add(ability);
        await DisplayChartsAsync();
    }

    private async Task RemoveAbilityFromComparisonAsync(IAbility ability)
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