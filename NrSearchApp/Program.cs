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

// Läs konfiguration från appsettings.json och appsettings.Development.json
builder.Configuration.AddJsonFile("appsettings.json", optional: true);
if (builder.HostEnvironment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true);
}

// Registrera ApiSettings från konfiguration
var apiSettings = new ApiSettings 
{ 
    NumverifyApiKey = builder.Configuration["ApiSettings:NumverifyApiKey"] ?? string.Empty
};
builder.Services.AddSingleton(apiSettings);

builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddScoped<NumverifyService>();
builder.Services.AddSingleton<ISearchStorage, LocalSearchStorage>();

await builder.Build().RunAsync();

