using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NrSearchApp;
using NrSearchApp.Services;
using NrSearchApp.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Explicit configuration setup
var apiSettings = new ApiSettings 
{ 
    NumverifyApiKey = "ee00a3752fdf985a24170072ad92afc0" 
};
builder.Services.AddSingleton(apiSettings);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<NumverifyService>();
builder.Services.AddSingleton<ISearchStorage, LocalSearchStorage>();

await builder.Build().RunAsync();

