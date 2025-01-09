using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Plundi.Hammerfall.Core.Models;

namespace Plundi.Hammerfall.App.Components;

public sealed partial class SelectAbilityModal
{
    private readonly string _dialogId = $"dialog-{Guid.NewGuid()}";

    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private List<IAbility> Abilities { get; set; } = null!;

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