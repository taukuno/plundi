using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Plundi.Hammerfall.App.Services;
using Plundi.Hammerfall.Core.Models;
using Plundi.Hammerfall.Core.Services;

namespace Plundi.Hammerfall.App.Components;

public partial class SimulatedDamageDoneChart : IAsyncDisposable
{
    private readonly string _canvasId = $"canvas-{Guid.NewGuid()}";
    private bool _isDisposed;
    private bool _isDrawn;
    private bool _isJsModuleLoaded;

    private IJSObjectReference? _jsModule;

    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

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
        _jsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", $"./Components/SimulatedDamageDoneChart.razor.js?cacheBuster={DateTime.UtcNow.Ticks}");
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

    public async Task UpdateAsync(List<(decimal Timestamp, decimal Damage)> damageDoneData)
    {
        if (!_isDrawn || _jsModule is null)
        {
            return;
        }

        var stepSize = 0.1m;
        var maxTimestamp = damageDoneData.Count > 0 ? damageDoneData.Max(x => x.Timestamp) : 1;
        maxTimestamp += maxTimestamp % stepSize;

        var timeLabels = new List<string>();
        var stackingDamageData = new List<decimal>();
        for (var timestamp = stepSize; timestamp <= maxTimestamp; timestamp += stepSize)
        {
            timeLabels.Add(timestamp.ToString(CultureInfo.InvariantCulture) + "s");
            stackingDamageData.Add(damageDoneData.Where(x => x.Timestamp <= timestamp).Sum(x => Math.Round(x.Damage, 1)));
        }

        await _jsModule.InvokeVoidAsync("updateChart", _canvasId, timeLabels, stackingDamageData, true);
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
