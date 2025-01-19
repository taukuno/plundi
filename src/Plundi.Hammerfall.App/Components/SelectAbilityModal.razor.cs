using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.App.Components;

public partial class SelectAbilityModal : ComponentBase
{
    private readonly string _dialogId = $"dialog-{Guid.NewGuid()}";

    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private IEnumerable<IAbility> Abilities { get; set; } = null!;

    [Parameter] public EventCallback<IAbility> OnAbilitySelected { get; set; }

    public async void Open()
    {
        await JsRuntime.InvokeVoidAsync("window.dialogFunctions.openDialog", _dialogId);
        StateHasChanged();
    }

    public async void Close()
    {
        await JsRuntime.InvokeVoidAsync("window.dialogFunctions.closeDialog", _dialogId);
    }
}
