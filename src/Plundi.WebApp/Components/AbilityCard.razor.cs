using System.Text;
using Microsoft.AspNetCore.Components;
using Plundi.WebApp.Models;

namespace Plundi.WebApp.Components;

public sealed partial class AbilityCard
{
    [Parameter] public RarifiedAbility Ability { get; set; } = default!;
    [Parameter] public int Position { get; set; }
    [Parameter] public EventCallback OnMoveUpClicked { get; set; }
    [Parameter] public EventCallback OnMoveDownClicked { get; set; }

    [CascadingParameter(Name = "CharacterLevel")]
    public int CharacterLevel { get; set; }

    [CascadingParameter(Name = "DisplayInCompactView")]
    public bool DisplayInCompactView { get; set; }

    private static string DamageSequenceToReadableString(List<double> damageSequence)
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