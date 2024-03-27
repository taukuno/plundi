using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Plundi.Core.Models;

namespace Plundi.WebApp.Components;

public sealed partial class SelectAbilityModal
{
    private readonly string _dialogId = $"dialog-{Guid.NewGuid()}";

    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;
    [Inject] private List<IAbility> Abilities { get; set; } = default!;

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