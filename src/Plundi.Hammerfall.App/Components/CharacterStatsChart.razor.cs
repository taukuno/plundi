using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Plundi.Hammerfall.Core.Services;

namespace Plundi.Hammerfall.App.Components;

public partial class CharacterStatsChart : IAsyncDisposable
{
    private readonly string _canvasId = $"canvas-{Guid.NewGuid()}";
    private bool _isDisposed;
    private bool _isDrawn;
    private bool _isJsModuleLoaded;

    private IJSObjectReference? _jsModule;

    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
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
        _jsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", $"./Components/CharacterStatsChart.razor.js?cacheBuster={DateTime.UtcNow.Ticks}");
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

        var hitPointsData = new List<int>();
        var attackPowerData = new List<int>();

        for (var level = 1; level <= 10; level++)
        {
            hitPointsData.Add(CharacterStatsProvider.GetHitPoints(level));
            attackPowerData.Add(CharacterStatsProvider.GetAttackPower(level));
        }

        await _jsModule.InvokeVoidAsync("updateChart", _canvasId, hitPointsData, attackPowerData, SmoothLines);
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