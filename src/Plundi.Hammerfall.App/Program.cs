using System.Reflection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Plundi.Hammerfall.App;
using Plundi.Hammerfall.App.Services;
using Plundi.Hammerfall.Core.Services;
using Plundi.Hammerfall.Core.Services.AbilityDamageCalculators;
using Plundi.Hammerfall.Core.Services.AbilityDetailsProvider;

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
    "G.R.A.V. Glove",
    "Hunter's Chains",
    "Lightning Bulwark",
    "Quaking Leap",
    "Repel",
    "Snowdrift",
    "Steel Traps",
    "Windstorm"
});
var types = coreAssembly.GetTypes().Where(x => x is { IsInterface: false, IsAbstract: false }).ToList();

    /*builder.Services.AddSingleton<IAbilityDetailsProvider, AuraOfZealotryDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, CelestialBarrageDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, EarthbreakerDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, FireWhirlDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, HolyShieldDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, ManaSphereDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, RimeArrowDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, SearingAxeDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, SlicingWindsDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, StarBombDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, StormArchonDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, ToxicSmackerelDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, CallGalefeatherDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, ExplosiveCaltropsDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, FadeToShadowDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, FaeformDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, GravGloveDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, HuntersChainsDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, LightningBulwarkDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, QuakingLeapDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, RepelDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, SnowdriftDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, SteelTrapsDetailsProvider>();
    builder.Services.AddSingleton<IAbilityDetailsProvider, WindstormDetailsProvider>();

    builder.Services.AddSingleton<IAbilityDamageCalculator, GeneralAbilityDamageCalculator>();
    builder.Services.AddSingleton<IAbilityDamageCalculator, HolyShieldDamageCalculator>();
    builder.Services.AddSingleton<IAbilityDamageCalculator, ToxicSmackerelDamageCalculator>();*/


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
}

// Services
builder.Services.AddSingleton<AbilityReportGenerator>();
builder.Services.AddSingleton<LocalStorage>();

// App
await builder.Build().RunAsync();
