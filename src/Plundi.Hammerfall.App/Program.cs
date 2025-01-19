using System.Reflection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Plundi.Hammerfall.App;
using Plundi.Hammerfall.App.Services;
using Plundi.Hammerfall.Core.Models;
using Plundi.Hammerfall.Core.Services;

var coreAssembly = Assembly.GetAssembly(typeof(IAbility))!;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


// Abilities
var types = coreAssembly.GetTypes().Where(x => x is { IsInterface: false, IsAbstract: false }).ToList();
var abilities = types.Where(x => x.GetInterfaces().Contains(typeof(IAbility))).Select(x => (IAbility)Activator.CreateInstance(x)!);
foreach (var ability in abilities)
{
    builder.Services.AddSingleton(ability);
}

foreach (var type in types)
foreach (var @interface in type.GetInterfaces())
{
    if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IAbilityDamageCalculator<>))
    {
        builder.Services.AddSingleton(@interface, type);
    }
}

// Services
builder.Services.AddSingleton<AbilityReportGenerator>();
builder.Services.AddSingleton<LocalStorage>();

// App
await builder.Build().RunAsync();
