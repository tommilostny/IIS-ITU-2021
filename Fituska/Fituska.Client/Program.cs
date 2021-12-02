using Fituska.Client;
using Fituska.Shared.Models.Search;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<LastPageStorageService>();
builder.Services.AddSingleton<SearchRequestModel>();
builder.Services.AddSingleton<Base64ImageService>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<FituskaAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<FituskaAuthenticationStateProvider>());

await builder.Build().RunAsync();
