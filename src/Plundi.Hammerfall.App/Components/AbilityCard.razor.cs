using System.Text;
using Microsoft.AspNetCore.Components;
using Plundi.Hammerfall.App.Models;
using Plundi.Hammerfall.App.Services;

namespace Plundi.Hammerfall.App.Components;

public partial class AbilityCard
{
    [Inject] private AbilityReportGenerator AbilityReportGenerator { get; set; } = null!;

    [Parameter] public RarifiedAbility Ability { get; set; } = null!;
    [Parameter] public int Position { get; set; }
    [Parameter] public EventCallback OnMoveUpClicked { get; set; }
    [Parameter] public EventCallback OnMoveDownClicked { get; set; }

    [CascadingParameter(Name = "CharacterLevel")]
    public int CharacterLevel { get; set; }

    [CascadingParameter(Name = "DisplayInCompactView")]
    public bool DisplayInCompactView { get; set; }

    private static string DamageSequenceToReadableString(List<decimal> damageSequence)
    {
        var readableString = new StringBuilder();
        var distinctDamages = damageSequence.GroupBy(x => x).ToList();

        foreach (var group in distinctDamages)
        {
            if (group.Count() > 1)
            {
                readableString.Append($"{group.Count()} x {Math.Round(group.Key, 1)}, ");
            }
            else
            {
                readableString.Append($"{Math.Round(group.Key, 1)}, ");
            }
        }

        return readableString.ToString().TrimEnd(',', ' ');
    }
}
