using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Plundi.Hammerfall.App.Layout;

public partial class MainLayout : LayoutComponentBase, IDisposable
{
    private bool _navigationMenuIsOpen;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    /// <inheritdoc />
    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        base.OnInitialized();
        NavigationManager.LocationChanged += OnLocationChanged;
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs args)
    {
        _navigationMenuIsOpen = false;
        StateHasChanged();
    }
}
