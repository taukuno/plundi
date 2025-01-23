using Microsoft.AspNetCore.Components;

namespace Plundi.Hammerfall.App.Components;

public partial class SiteInformationDisplay : ComponentBase
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

}
