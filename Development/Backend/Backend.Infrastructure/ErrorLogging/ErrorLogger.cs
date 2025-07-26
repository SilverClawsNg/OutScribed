using Backend.Application.Interfaces;
using Serilog;
using System;

namespace Backend.Infrastructure.ErrorLogging
{
    public class ErrorLogger : IErrorLogger
    {
        private static readonly DateTime date = DateTime.UtcNow;

        public void LogInfo(string message)
        {
            Log.Information("Information: {message} at {date}", message, date);
        }

        public void LogWarning(string message)
        {
            Log.Warning("Warning: {message} at {date}", message, date);
        }

        public void LogError(string message)
        {
            Log.Information("Error: {message} at {date}", message, date);
        }

        public void LogError(Exception exception)
        {
            Log.ForContext("LogType", "General")
                          .ForContext("Id", Guid.NewGuid())
                          .ForContext("Application", "OutScribed.API")
                          .Error(exception, "Exception occured: {Message}", exception.Message);
        }
    }
}
