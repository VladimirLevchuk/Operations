using System;
using System.Diagnostics;
using System.Threading;
using Operations.Debugging;
using Operations.Serilog;
using Operations.Trackers;
using Operations.Trackers.Profiler;
using Operations.Util;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;

namespace Operations.ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole(LogEventLevel.Debug)
                // The following requires https://getseq.net/
                .WriteTo.Seq("http://localhost:5341/")
                .Enrich.FromLogContext()
                .CreateLogger();

            SelfLog.Enable(msg =>
            {
                Console.WriteLine(msg);
                Debug.WriteLine(msg);
            });

            OperationsLog.Enable(Console.WriteLine);
            OperationsSerilogDefaults.Apply();

            Op.Runner = Op.Configure(x => x
                .Track.With<StatusTracker>()
                .Track.With<ProfilingTracker>()
                .Track.With<SerilogOperationTracker>())
                .CreateRunner();

            using (var op = Op.Start("root", Op.Context(new {Hello = "World"})))
            {
                Thread.Sleep(1000);
                using (Op.Start("child", Op.Context(new { parent = "root" })))
                {
                    Thread.Sleep(1000);
                    Log.Logger.Information("hello from child");
                }
                Log.Logger.Information("root: child is finished");
                Thread.Sleep(1000);
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
