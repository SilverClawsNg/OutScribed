using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace OutScribed.Infrastructure.ExceptionHandling
{
    public class GlobalExceptionHandler : IExceptionHandler
    {

        //private readonly ILogger<GlobalExceptionHandler> _logger;

        //public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        //{
        //    _logger = logger;
        //}

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {

            Log.ForContext("LogType", "General")
                          .ForContext("Id", Guid.NewGuid())
                         .ForContext("Application", "OutScribed.API")
                         .Error(exception, "Exception occured: {Message}", exception.Message);



            var problemDetails = new ProblemDetails();

            switch (exception)
            {
                case BadHttpRequestException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = exception.GetType().Name;
                    problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6-6-1";
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case UnauthorizedAccessException:
                    problemDetails.Status = StatusCodes.Status401Unauthorized;
                    problemDetails.Title = "Unauthroized Access";
                    problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6-6-1";
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    break;
                default:
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Title = "Internal Server Error";
                    problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6-6-1";
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
