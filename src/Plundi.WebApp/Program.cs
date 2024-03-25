using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Plundi.Core.Models;
using Plundi.Core.Models.Abilities;
using Plundi.WebApp;
using Plundi.WebApp.Common.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<LocalStorage>();
builder.Services.AddSingleton(new List<IAbility>
{
    new Earthbreaker(),
    new FireWhirl(),
    new HolyShield(),
    new ManaSphere(),
    new RimeArrow(),
    new SearingAxe(),
    new SlicingWinds(),
    new StarBomb(),
    new StormArchon(),
    new ToxicSmackerel(),
    new ExplosiveCaltrops(),
    new FadeToShadow(),
    new Faeform(),
    new HuntersChains(),
    new LightningBulwark(),
    new QuakingLeap(),
    new Repel(),
    new Snowdrift(),
    new SteelTraps(),
    new Windstorm()
});

await builder.Build().RunAsync();