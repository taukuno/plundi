using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Components;
using Plundi.Hammerfall.App.Components;
using Plundi.Hammerfall.Core.Models;
using Plundi.Hammerfall.Core.Services;

namespace Plundi.Hammerfall.App.Pages;

public partial class SimulationsPage : ComponentBase
{
    private readonly List<AbilitySetting> _abilitySettings = [];

    private SelectAbilityModal? _selectAbilityModal;
    private SimulatedDamageDoneChart? _simulatedDamageDoneChart;

    private string _selectAbilityModalOpenedFor = nameof(_primaryOffensiveAbilityName);

    private int _playerLevel = 10;
    private bool _useMeleeDuringDowntime = true;

    private string? _primaryOffensiveAbilityName;
    private AbilityRarity _primaryOffensiveAbilityRarity = AbilityRarity.Epic;
    private string? _secondaryOffensiveAbilityName;
    private AbilityRarity _secondaryOffensiveAbilityRarity = AbilityRarity.Epic;
    private string? _primaryUtilityAbilityName;
    private AbilityRarity _primaryUtilityAbilityRarity = AbilityRarity.Epic;
    private string? _secondaryUtilityAbilityName;
    private AbilityRarity _secondaryUtilityAbilityRarity = AbilityRarity.Epic;

    private bool _isSimulationRunning;
    private bool _isSimulationOutdated;
    private LoadoutSimulationReport? _simulationReport;

    private bool _areChartsDrawn;

    [Inject] private List<string> Abilities { get; set; } = null!;
    [Inject] private AbilityServiceProvider AbilityServiceProvider { get; set; } = null!;
    [Inject] private LoadoutSimulationGenerator LoadoutSimulationGenerator { get; set; } = null!;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        foreach (var ability in Abilities)
        {
            var detailsProvider = AbilityServiceProvider.GetAbilityDetailsProvider(ability);
            var possibleSettings = detailsProvider.GetPossibleSimulationSettings(ability);

            foreach (var possibleSetting in possibleSettings)
            {
                _abilitySettings.Add(
                    new()
                    {
                        AbilityName = ability,
                        SettingKey = possibleSetting.Key,
                        Description = possibleSetting.Value.Description,
                        PossibleValues = possibleSetting.Value.PossibleValues,
                        Value = possibleSetting.Value.DefaultValue
                    }
                );
            }
        }

        base.OnInitialized();
    }

    private void OnAbilitySelected(string abilityName)
    {
        _isSimulationOutdated = true;

        switch (_selectAbilityModalOpenedFor)
        {
            case nameof(_primaryOffensiveAbilityName):
                _primaryOffensiveAbilityName = abilityName;
                break;
            case nameof(_secondaryOffensiveAbilityName):
                _secondaryOffensiveAbilityName = abilityName;
                break;
            case nameof(_primaryUtilityAbilityName):
                _primaryUtilityAbilityName = abilityName;
                break;
            case nameof(_secondaryUtilityAbilityName):
                _secondaryUtilityAbilityName = abilityName;
                break;
        }
    }

    private async Task SimulateAsync()
    {
        _isSimulationRunning = true;
        StateHasChanged();

        var abilitiesBeforeMelee = new List<RarifiedAbility>();
        var abilitiesAfterMelee = new List<RarifiedAbility>();

        ProcessAbility(_primaryOffensiveAbilityName, _primaryOffensiveAbilityRarity);
        ProcessAbility(_secondaryOffensiveAbilityName, _secondaryOffensiveAbilityRarity);
        ProcessAbility(_primaryUtilityAbilityName, _primaryUtilityAbilityRarity);
        ProcessAbility(_secondaryUtilityAbilityName, _secondaryUtilityAbilityRarity);

        var abilities = abilitiesBeforeMelee.ToList();

        if (_useMeleeDuringDowntime)
        {
            abilities.Add(new() { Name = "Slash", Rarity = AbilityRarity.Common });
        }

        abilities.AddRange(abilitiesAfterMelee);

        var loadout = new Loadout
        {
            Abilities = abilities
        };

        var abilitySettings = _abilitySettings
            .GroupBy(setting => setting.AbilityName)
            .ToDictionary(
                group => group.Key,
                group => group.ToDictionary(
                    setting => setting.SettingKey,
                    setting => setting.Value)
            );

        _simulationReport = LoadoutSimulationGenerator.GenerateSimulationReport(loadout, 60, _playerLevel, abilitySettings);

        _isSimulationOutdated = false;
        _isSimulationRunning = false;
        StateHasChanged();

        await DrawChartsAsync();
        await UpdateChartsAsync();

        return;

        void ProcessAbility(string? abilityName, AbilityRarity abilityRarity)
        {
            if (abilityName is null)
            {
                return;
            }

            var detailsProvider = AbilityServiceProvider.GetAbilityDetailsProvider(abilityName);
            var newAbility = new RarifiedAbility { Name = abilityName, Rarity = abilityRarity };

            if (detailsProvider.CanBeCastedDuringGlobalCooldown(abilityName))
            {
                abilitiesAfterMelee.Add(newAbility);
            }
            else
            {
                abilitiesBeforeMelee.Add(newAbility);
            }
        }
    }

    private async Task DrawChartsAsync()
    {
        if (_areChartsDrawn)
        {
            return;
        }

        await Task.Yield();
        while (_simulatedDamageDoneChart is null)
        {
            await Task.Delay(10);
        }

        await _simulatedDamageDoneChart.DrawAsync();
        _areChartsDrawn = true;
    }

    private async Task UpdateChartsAsync()
    {
        if (!_areChartsDrawn || _simulationReport is null)
        {
            return;
        }

        await _simulatedDamageDoneChart!.UpdateAsync(_simulationReport.Events.Select(x => (x.Timestamp, x.Damage)).ToList());
    }

    private class AbilitySetting
    {
        public string AbilityName { get; set; } = "";
        public string SettingKey { get; set; } = "";
        public string Description { get; set; } = "";
        public List<string> PossibleValues { get; set; } = [];
        public string Value { get; set; } = "";
    }
}
