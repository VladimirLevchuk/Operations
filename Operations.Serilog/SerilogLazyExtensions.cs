using System;
using Serilog;
using Serilog.Events;

namespace Operations.Serilog
{
    public static class SerilogLazyExtensions
    {
        public static void Verbose(this ILogger log, Func<string> lazyMessage)
        {
            if (log.IsEnabled(LogEventLevel.Verbose))
                log.Verbose(lazyMessage());
        }

        public static void Debug(this ILogger log, Func<string> lazyMessage)
        {
            if (log.IsEnabled(LogEventLevel.Debug))
                log.Debug(lazyMessage());
        }

        public static void Information(this ILogger log, Func<string> lazyMessage)
        {
            if (log.IsEnabled(LogEventLevel.Information))
                log.Information(lazyMessage());
        }

        public static void Error(this ILogger log, Exception error, Func<string> lazyMessage)
        {
            if (log.IsEnabled(LogEventLevel.Error))
                log.Error(error, lazyMessage());
        }

        public static void Fatal(this ILogger log, Exception error, Func<string> lazyMessage)
        {
            if (log.IsEnabled(LogEventLevel.Fatal))
                log.Fatal(error, lazyMessage());
        }
    }
}
