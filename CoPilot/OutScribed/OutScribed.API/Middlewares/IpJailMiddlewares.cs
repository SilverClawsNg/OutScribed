// In your API project, e.g., OutScribed.Api/Middleware/IpJailMiddleware.cs
using MassTransit; // If you need to publish events from middleware
using OutScribed.Modules.Jail.Application.Interfaces;
using OutScribed.SharedKernel.Enums;
using OutScribed.SharedKernel.Utilities;

namespace OutScribed.API.Middlewares
{
  
    public class IpJailMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<IpJailMiddleware> _logger;

        public IpJailMiddleware(RequestDelegate next, ILogger<IpJailMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IJailService jailService, IPublishEndpoint publishEndpoint)
        {
            string? ipAddress = IpAddressHelper.GetClientIpAddressRobust(context);

            // Handle cases where IP address might be null or empty (e.g., in development with no remote client)
            if (string.IsNullOrEmpty(ipAddress))
            {
                _logger.LogWarning("Could not determine IP address for request. Continuing without IP jail check.");
                await _next(context); // Allow request to proceed if IP is unknown
                return;
            }

            if (await jailService.IsCurrentlyJailedAsync(ipAddress))
            {
                _logger.LogWarning("Blocked request from jailed IP address: {IpAddress} to path: {Path}", ipAddress, context.Request.Path);

                // Publish an event indicating the violation for logging/monitoring/analytics
                //await publishEndpoint.Publish(new IpAddressViolationOccurredEvent(ipAddress, null, JailReason.IpLocked)); // Email is null here as we don't have request body access yet

                // Return an appropriate HTTP response to the client
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests; // Too Many Requests
                context.Response.ContentType = "application/json";
                // You can customize the error message to your API's standard format
                await context.Response.WriteAsJsonAsync(new
                {
                    status = StatusCodes.Status429TooManyRequests,
                    title = "Access Restricted",
                    detail = "Access from your IP address is temporarily restricted due to unusual activity. Please try again later."
                });

                return; // Short-circuit the pipeline; no further processing for this request
            }

            await _next(context); // If not jailed, proceed to the next middleware in the pipeline
        }
    }
}
