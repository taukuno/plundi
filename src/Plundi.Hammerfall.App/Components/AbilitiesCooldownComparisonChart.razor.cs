using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Plundi.Hammerfall.App.Services;
using Plundi.Hammerfall.Core.Models;
using Plundi.Hammerfall.Core.Services;

namespace Plundi.Hammerfall.App.Components;

public partial class AbilitiesCooldownComparisonChart : IAsyncDisposable
{
    private readonly string _canvasId = $"canvas-{Guid.NewGuid()}";
    private bool _isDisposed;
    private bool _isDrawn;
    private bool _isJsModuleLoaded;

    private IJSObjectReference? _jsModule;

    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private IEnumerable<IAbilityDetailsProvider> AbilityDetailsProviders { get; set; } = null!;

    [Parameter] public List<string> Abilities { get; set; } = [];
    [Parameter] public int CharacterLevel { get; set; } = 1;


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
        _jsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", $"./Components/AbilitiesCooldownComparisonChart.razor.js?cacheBuster={DateTime.UtcNow.Ticks}");
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
            var detailsProvider = GetAbilityDetailsProvider(ability);
            var cooldownScalingData = GenerateCooldownScalingData(ability);
            abilitiesData.Add(new { Label = detailsProvider.GetDisplayName(ability), Data = cooldownScalingData });
        }

        await _jsModule.InvokeVoidAsync("updateChart", _canvasId, abilitiesData);
    }

    private IAbilityDetailsProvider GetAbilityDetailsProvider(string ability)
    {
        return AbilityDetailsProviders.FirstOrDefault(x => x.CanHandleAbility(ability)) ?? throw new InvalidOperationException($"No details provider registered for the ability '{ability}'.");
    }

    private List<object> GenerateCooldownScalingData(string ability)
    {
        var detailsProvider = GetAbilityDetailsProvider(ability);
        return Enum.GetValues<AbilityRarity>().Select(rarity => detailsProvider.GetCooldown(ability, CharacterLevel, rarity)).Cast<object>().ToList();
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
