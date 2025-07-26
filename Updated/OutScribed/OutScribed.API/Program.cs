using FastEndpoints;
using OutScribed.API.Extensions;
using OutScribed.Onboarding.Application.Features.CreateTempUser;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

//DI(Dependency Injection) extensions
builder.Services
       .AddOutscribedPersistence(builder.Configuration)
       .AddOutscribedInfrastructure();

builder.Services.AddFastEndpoints();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateTempUserEndpoint).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseFastEndpoints();

app.UseHttpsRedirection();

app.Run();
