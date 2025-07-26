using Serilog.Core;
using Serilog.Events;

namespace OutScribed.Api.Analytics
{
    public class AnalyticsLogEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("LogType", "Analytics"));
        }
    }
}
