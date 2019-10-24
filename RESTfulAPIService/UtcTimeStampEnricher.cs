using Serilog.Core;
using Serilog.Events;

namespace RESTfulAPIService
{
    /// <summary>
    /// UTS time
    /// </summary>
    public class UtcTimeStampEnricher : ILogEventEnricher
    {
        /// <summary>
        /// </summary>
        /// <param name="logEvent"></param>
        /// <param name="propertyFactory"></param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(
              propertyFactory.CreateProperty(
                  "UtcTimestamp",
                  logEvent.Timestamp.UtcDateTime));
        }
    }
}