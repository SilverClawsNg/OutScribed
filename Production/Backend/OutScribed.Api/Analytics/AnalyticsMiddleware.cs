using Backend.Api.GeoData;
using MaxMind.GeoIP2;
using Serilog;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;

namespace OutScribed.Api.Analytics
{
    public class AnalyticsMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly GeoLocationService _geo;

        public AnalyticsMiddleware(RequestDelegate next, GeoLocationService geo)
        {
            _next = next;
            _geo = geo;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();

            var ipAddress = context.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                                 ?? context.Connection.RemoteIpAddress?.ToString();

            var (continent, country, region, city) = _geo.GetLocation(ipAddress);

            Log.ForContext("LogType", "Analytics")
                .ForContext("Id", Guid.NewGuid())
                 .ForContext("UserId", context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Anonymous")
                   .ForContext("Username", context.User.FindFirstValue(ClaimTypes.Name) ?? "Anonymous")
                   .ForContext("Path", context.Request.Path)
                   .ForContext("Method", context.Request.Method)
                   .ForContext("StatusCode", context.Response.StatusCode)
                   .ForContext("ElapsedMs", stopwatch.ElapsedMilliseconds)
                    .ForContext("IpAddress", ipAddress)
                    .ForContext("Country", country)
                    .ForContext("Continent", continent)
                    .ForContext("City", city)
                    .ForContext("Region", region)
                   .ForContext("UserAgent", context.Request.Headers["User-Agent"].ToString())
                   .Information("Request completed");
        }
    }
}
