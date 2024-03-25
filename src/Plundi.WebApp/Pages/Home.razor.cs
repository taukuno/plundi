using Microsoft.AspNetCore.Components;
using Plundi.Core.Models;
using Plundi.WebApp.States;

namespace Plundi.WebApp.Pages;

public sealed partial class Home : IDisposable
{
    private bool _damageAbilitiesInCompactView;
    private bool _utilityAbilitiesInCompactView;

    [Inject] private GlobalState GlobalState { get; set; } = default!;
    [Inject] private List<IAbility> Abilities { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    /// <inheritdoc />
    public void Dispose()
    {
        GlobalState.OnChange -= StateHasChanged;
    }

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        base.OnInitialized();
        GlobalState.OnChange += StateHasChanged;
    }

    /// <inheritdoc />
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await GlobalState.InitializeAbilitiesAsync(Abilities);
            GlobalState.OnChange += StateHasChanged;
            StateHasChanged();
        }
    }
}