using Serilog.Core;
using Serilog.Events;

namespace RESTfullAPIService
{
    public class UtcTimeStampEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(
              propertyFactory.CreateProperty(
                  "UtcTimestamp",
                  logEvent.Timestamp.UtcDateTime));
        }
    }
}