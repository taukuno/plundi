using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Plundi.Hammerfall.Core.Models;
using Plundi.Hammerfall.Core.Services;

namespace Plundi.Hammerfall.App.Components;

public partial class SelectAbilityModal : ComponentBase
{
    private readonly string _dialogId = $"dialog-{Guid.NewGuid()}";

    private List<string> _abilitiesToHide = [];
    private bool _showOffensiveAbilities = true;
    private bool _showUtilityAbilities = true;

    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private List<string> Abilities { get; set; } = null!;
    [Inject] private AbilityServiceProvider AbilityServiceProvider { get; set; } = null!;

    [Parameter] public EventCallback<string> OnAbilitySelected { get; set; }

    public async void Open(List<string>? abilitiesToHide = null, bool showOffensiveAbilities = true, bool showUtilityAbilities = true)
    {
        _abilitiesToHide = abilitiesToHide ?? [];
        _showOffensiveAbilities = showOffensiveAbilities;
        _showUtilityAbilities = showUtilityAbilities;

        await JsRuntime.InvokeVoidAsync("window.dialogFunctions.openDialog", _dialogId);
        StateHasChanged();
    }

    public async void Close()
    {
        await JsRuntime.InvokeVoidAsync("window.dialogFunctions.closeDialog", _dialogId);
    }
}
