using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using OutScribed.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExtensions(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRouting(); // Important: If you need access to endpoint info (like RouteData or Endpoint Name) in middleware, UseRouting() should come before it.

app.UseIpJail(); // <-- Add your custom IP Jail middleware here

app.UseAuthentication(); // Example: place after IP jail

app.UseAuthorization();   // Example: place after IP jail

app.UseFastEndpoints(c =>
{
    c.Errors.ResponseBuilder = (failures, ctx, statusCode) =>
    {
        return new ValidationProblemDetails(
            failures.GroupBy(f => f.PropertyName)
                    .ToDictionary(
                        keySelector: e => e.Key,
                        elementSelector: e => e.Select(m => m.ErrorMessage).ToArray()))
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "One or more validation errors occurred.",
            Status = statusCode,
            Instance = ctx.Request.Path,
            Detail = "Please refer to the 'errors' property for more details.",
            Extensions = { { "traceId", ctx.TraceIdentifier } }
        };
    };
});

app.Run();
