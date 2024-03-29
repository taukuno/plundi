using System.Reflection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Plundi.Core.Models;
using Plundi.Core.Services;
using Plundi.WebApp;
using Plundi.WebApp.Common.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<LocalStorage>();

var types = Assembly.GetAssembly(typeof(IAbility))!.GetTypes().Where(x => x is { IsInterface: false, IsAbstract: false }).ToList();
builder.Services.AddSingleton(types.Where(x => x.GetInterfaces().Contains(typeof(IAbility))).Select(x => (IAbility)Activator.CreateInstance(x)!)
    .ToList());

foreach (var type in types)
foreach (var @interface in type.GetInterfaces())
{
    if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IAbilityDamageCalculator<>))
    {
        builder.Services.AddSingleton(@interface, type);
    }
}

builder.Services.AddSingleton<AbilityReportGenerator>();

await builder.Build().RunAsync();