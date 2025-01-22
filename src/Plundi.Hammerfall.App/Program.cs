using System.Reflection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Plundi.Hammerfall.App;
using Plundi.Hammerfall.App.Services;
using Plundi.Hammerfall.Core.Services;

var coreAssembly = Assembly.GetAssembly(typeof(IAbilityDetailsProvider))!;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Abilities
builder.Services.AddSingleton(new List<string>()
{
    "Aura of Zealotry",
    "Celestial Barrage",
    "Earthbreaker",
    "Fire Whirl",
    "Holy Shield",
    "Mana Sphere",
    "Rime Arrow",
    "Searing Axe",
    "Slicing Winds",
    "Star Bomb",
    "Storm Archon",
    "Toxic Smackerel",
    "Call Galefeather",
    "Explosive Caltrops",
    "Fade to Shadow",
    "Faeform",
    "Hunter's Chains",
    "Lightning Bulwark",
    "Quaking Leap",
    "Repel",
    "Snowdrift",
    "Steel Traps",
    "Windstorm",
    "Void Tear",
    "Slash",
    "Zealot's Smite"
});

var types = coreAssembly.GetTypes().Where(x => x is { IsInterface: false, IsAbstract: false }).ToList();

foreach (var type in types)
foreach (var @interface in type.GetInterfaces())
{
    if (@interface == typeof(IAbilityDetailsProvider))
    {
        builder.Services.AddSingleton(typeof(IAbilityDetailsProvider), type);
    }

    if (@interface == typeof(IAbilityDamageCalculator))
    {
        builder.Services.AddSingleton(typeof(IAbilityDamageCalculator), type);
    }

    if (@interface == typeof(IAbilitySimulationHandler))
    {
        builder.Services.AddSingleton(typeof(IAbilitySimulationHandler), type);
    }
}

// Services
builder.Services.AddSingleton<AbilityServiceProvider>();
builder.Services.AddSingleton<AbilityReportGenerator>();
builder.Services.AddSingleton<LoadoutSimulationGenerator>();
builder.Services.AddSingleton<LocalStorage>();

// App
await builder.Build().RunAsync();
