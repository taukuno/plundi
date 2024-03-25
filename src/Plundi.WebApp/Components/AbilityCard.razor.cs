using System.Text;
using Microsoft.AspNetCore.Components;
using Plundi.Core.Models;
using Plundi.WebApp.Models;
using Plundi.WebApp.States;

namespace Plundi.WebApp.Components;

public sealed partial class AbilityCard : IDisposable
{
    [Inject] private GlobalState GlobalState { get; set; } = default!;

    [Parameter] public RarifiedAbility Ability { get; set; } = default!;
    [Parameter] public bool CompactView { get; set; }
    [Parameter] public int Position { get; set; }
    [Parameter] public EventCallback OnMoveUpClicked { get; set; }
    [Parameter] public EventCallback OnMoveDownClicked { get; set; }

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

    private static string DamageSequenceToReadableString(List<double> damageSequence)
    {
        var readableString = new StringBuilder();
        var distinctDamages = damageSequence.GroupBy(x => x).ToList();

        foreach (var group in distinctDamages)
        {
            if (group.Count() > 1)
            {
                readableString.Append($"{group.Count()} x {group.Key}, ");
            }
            else
            {
                readableString.Append($"{group.Key}, ");
            }
        }

        return readableString.ToString().TrimEnd(',', ' ');
    }
}