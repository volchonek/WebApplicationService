using System;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace RESTfullAPIService
{
    public class XunitLogger<T> : ILogger<T>, IDisposable
    {
        private readonly ITestOutputHelper _output;

        public XunitLogger(ITestOutputHelper output)
        {
            _output = output;
        }

        public void Dispose()
        {
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            _output.WriteLine(state.ToString());
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }
    }

    public class XUnitLoggerProvider : ILoggerProvider
    {
        private readonly ILogger _logger;

        public XUnitLoggerProvider(ILogger logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _logger;
        }
    }
}