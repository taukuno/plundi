using Microsoft.AspNetCore.Components;

namespace Plundi.Hammerfall.App.Components;

public partial class PowerLevelDisplay : ComponentBase
{
    [Parameter] public int PlayerLevel { get; set; } = 1;
    [Parameter] public int TargetLevel { get; set; } = 1;
}
