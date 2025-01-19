using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Plundi.Hammerfall.Core.Models;
using Plundi.Hammerfall.Core.Services;

namespace Plundi.Hammerfall.App.Components;

public partial class SelectAbilityModal : ComponentBase
{
    private readonly string _dialogId = $"dialog-{Guid.NewGuid()}";

    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private List<string> Abilities { get; set; } = null!;
    [Inject] private IEnumerable<IAbilityDetailsProvider> AbilityDetailsProviders { get; set; } = null!;

    [Parameter] public EventCallback<string> OnAbilitySelected { get; set; }

    public async void Open()
    {
        await JsRuntime.InvokeVoidAsync("window.dialogFunctions.openDialog", _dialogId);
        StateHasChanged();
    }

    public async void Close()
    {
        await JsRuntime.InvokeVoidAsync("window.dialogFunctions.closeDialog", _dialogId);
    }

    private IAbilityDetailsProvider GetAbilityDetailsProvider(string ability)
    {
        return AbilityDetailsProviders.FirstOrDefault(x => x.CanHandleAbility(ability)) ?? throw new InvalidOperationException($"No details provider registered for the ability '{ability}'.");
    }
}
