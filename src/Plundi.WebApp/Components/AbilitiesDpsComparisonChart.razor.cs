using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Plundi.Core.Models;
using Plundi.Core.Services;

namespace Plundi.WebApp.Components;

public sealed partial class AbilitiesDpsComparisonChart : IAsyncDisposable
{
    private readonly string _canvasId = $"canvas-{Guid.NewGuid()}";
    private bool _isDisposed;
    private bool _isDrawn;
    private bool _isJsModuleLoaded;

    private IJSObjectReference? _jsModule;

    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    [Parameter] public List<IAbility> Abilities { get; set; } = [];
    [Parameter] public int ForLevel { get; set; } = 1;
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
        _jsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/charts/abilitiesDpsComparisonChart.js");
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
            var dpsScalingData = GenerateDpsScalingData(ability, ForLevel);
            abilitiesData.Add(new { Label = ability.Name, Data = dpsScalingData });
        }

        await _jsModule.InvokeVoidAsync("updateChart", _canvasId, abilitiesData, SmoothLines);
    }

    private static List<object> GenerateDpsScalingData(IAbility ability, int characterLevel)
    {
        var dpsScaling = new List<object>();

        foreach (var rarity in Enum.GetValues<AbilityRarity>())
        {
            var report = DamageReportGenerator.GenerateDamageReport(ability, characterLevel, rarity);
            dpsScaling.Add(new { Min = Math.Round(report.DamageRange.MinDps, 1), Max = Math.Round(report.DamageRange.MaxDps, 1) });
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