using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OutScribed.Client.Services.Implementations;
using OutScribed.Client.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7027/api/") });

builder.Services.AddScoped(typeof(IApiGetServices<>), typeof(ApiGetServices<>));

builder.Services.AddScoped(typeof(IApiPatchServices<,>), typeof(ApiPatchServices<,>));

builder.Services.AddScoped(typeof(IApiPostServices<,>), typeof(ApiPostServices<,>));

builder.Services.AddScoped<IAuthenticationServices, AuthenticationServices>();

builder.Services.AddScoped<ISelectServices, SelectServices>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
