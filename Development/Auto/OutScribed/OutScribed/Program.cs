using Blazored.LocalStorage;
using OutScribed;
using OutScribed.Client.Services.Implementations;
using OutScribed.Client.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7027/api/") });

builder.Services.AddScoped(typeof(IApiGetServices<>), typeof(ApiGetServices<>));

builder.Services.AddScoped(typeof(IApiPatchServices<,>), typeof(ApiPatchServices<,>));

builder.Services.AddScoped(typeof(IApiPostServices<,>), typeof(ApiPostServices<,>));

builder.Services.AddScoped<IAuthenticationServices, AuthenticationServices>();

builder.Services.AddScoped<ISelectServices, SelectServices>();

builder.Services.AddBlazoredLocalStorage();

//builder.Services.AddAuthorizationCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(OutScribed.Client._Imports).Assembly);

app.Run();
