using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Plundi.Hammerfall.App.Services;
using Plundi.Hammerfall.Core.Models;
using Plundi.Hammerfall.Core.Services;

namespace Plundi.Hammerfall.App.Components;

public partial class AbilitiesDpsComparisonChart : IAsyncDisposable
{
    private readonly string _canvasId = $"canvas-{Guid.NewGuid()}";
    private bool _isDisposed;
    private bool _isDrawn;
    private bool _isJsModuleLoaded;

    private IJSObjectReference? _jsModule;

    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private AbilityReportGenerator AbilityReportGenerator { get; set; } = null!;
    [Inject] private AbilityServiceProvider AbilityServiceProvider { get; set; } = null!;

    [Parameter] public List<string> Abilities { get; set; } = [];
    [Parameter] public int PlayerLevel { get; set; } = 1;
    [Parameter] public bool SmoothLines { get; set; } = true;


    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if (_isDisposed)
        {
            return;
        }

        if (_isJsModuleLoaded && _jsModule is not null)
        {
            await _jsModule.InvokeVoidAsync("deleteChart", _canvasId);
            await _jsModule.DisposeAsync();
            _jsModule = null;
        }

        _isDisposed = true;
    }

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _jsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", $"./Components/AbilitiesDpsComparisonChart.razor.js?cacheBuster={DateTime.UtcNow.Ticks}");
        _isJsModuleLoaded = true;
    }

    public async Task DrawAsync()
    {
        if (_isDrawn)
        {
            return;
        }

        _isDrawn = true;
        await Task.Yield();

        while (!_isJsModuleLoaded)
        {
            await Task.Delay(10);
        }

        if (_jsModule is null)
        {
            return;
        }

        await _jsModule.InvokeVoidAsync("drawChart", _canvasId);
    }

    public async Task UpdateAsync()
    {
        if (!_isDrawn || _jsModule is null)
        {
            return;
        }


        var abilitiesData = new List<object>();

        foreach (var ability in Abilities)
        {
            var detailsProvider = AbilityServiceProvider.GetAbilityDetailsProvider(ability);
            var dpsScalingData = GenerateDpsScalingData(ability);
            abilitiesData.Add(new { Label = detailsProvider.GetDisplayName(ability), Data = dpsScalingData });
        }

        await _jsModule.InvokeVoidAsync("updateChart", _canvasId, abilitiesData, SmoothLines);
    }

    private List<object> GenerateDpsScalingData(string abilityName)
    {
        var dpsScaling = new List<object>();

        foreach (var rarity in Enum.GetValues<AbilityRarity>())
        {
            var report = AbilityReportGenerator.GenerateDamageReport(abilityName, rarity, PlayerLevel);
            dpsScaling.Add(new { Min = Math.Round(report.TotalDamageRange.MinDps, 1), Max = Math.Round(report.TotalDamageRange.MaxDps, 1) });
        }

        return dpsScaling;
    }

    public async Task ClearAsync()
    {
        if (!_isDrawn || _jsModule is null)
        {
            return;
        }

        await _jsModule.InvokeVoidAsync("clearChart", _canvasId);
    }
}
