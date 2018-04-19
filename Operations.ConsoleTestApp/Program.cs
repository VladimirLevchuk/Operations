using System;
using System.Diagnostics;
using System.Threading;
using Operations.Debugging;
using Operations.Serilog;
using Operations.Trackers;
using Operations.Trackers.Profiler;
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
                .WriteTo.ColoredConsole(LogEventLevel.Debug, "{Level:u3} {Timestamp:yyyy-MM-dd HH:mm:ss} {Message}{NewLine}{Exception}{NewLine}{Properties}{NewLine}{NewLine}")
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

            Operations.Serilog.Factories.UseDestructuring();

            Op.Runner = Op.Configure(x => x
                .Track.With<StatusTracker>()
                .Track.With<ProfilingTracker>()
                .Track.With<SerilogOperationTracker>())
                .CreateRunner();

            try
            {
                using (var op = Op.Start("root", Op.Context(new {Hello = "World"})))
                {
                    Thread.Sleep(1000);

                    for (int i = 1; i < 4; ++i)
                    {
                        var test = i;

                        op.Update($"something new happended: starting #{test}");
                        // todo: support Op.Run("name", op => { ... op.Update() } })

                        Op.Run($"child #{test}", () =>
                        {
                            /* and Run/RunAsync automatically track operation status 
                             * and track operation errors (by calling Operation.Error) when exception occurs 
                             */
                            Thread.Sleep(1000);

                            if (test == 3)
                            {
                                throw new Exception("space oddity");
                            }
                            
                            Log.Logger.Information("hello from child");
                        }, Op.Context(new {parent = "root"}));
                    }

                    Log.Logger.Information("root: child is finished");
                    Thread.Sleep(1000);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }
        }
    }
}
