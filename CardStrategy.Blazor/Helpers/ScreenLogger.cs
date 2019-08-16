using Microsoft.Extensions.Logging;
using MVVMShirt.Messages;
using System;
using System.Text.Json.Serialization;

namespace CardStrategy.Blazor.Helpers
{
    public class ScreenLogger<T> : ILogger<T>
    {
        private readonly IMessageBus _messageBus;
        private readonly ILogger<T> _logger;
        private string _context;        


        public ScreenLogger(IMessageBus messageBus, ILoggerFactory loggerFactory)
        {
            _messageBus = messageBus;
            _logger = new Logger<T>(loggerFactory);
        }

        // https://stackoverflow.com/questions/46110585/how-do-i-get-my-custom-ilogger-implementation-from-ilogger-in-for-example-a-con
        public IDisposable BeginScope<TState>(TState state)
        {
            var s = state as IDisposable;
            _context = JsonSerializer.ToString(s);
            return s;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }        

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            _logger.Log(logLevel, eventId, state, exception, formatter);
            _messageBus.Publish("LOG_MESSAGE");
        }
    }
}
