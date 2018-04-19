Op - Operations Framework
=========================
Inspired by great Serilog and MiniProfiler Op provides a simple way to keep a diagnostics context. 

## Features
- Operation (IOperation) is a named (logical) scope of code holding an _operation context_ (dictionary-based storage)
- Operation Tracker (IOpeartionTracker) is simple handler of the predefined operation events: Start/Finish/Update/Error (Update is for tracking operation progress). 
Tracker stores its tracking information in the operation context. 
- The root operation tracker (IRootOperationTracker) is used by framework to configure list of trackers (to be sequentally run for each operation)
- So Op allows to _track_ logical operations by a configurable set of _trackers_ (like Enrichers in Serilog)
- Built-in trackers: 
    - StatusTracker - adds status ok flag and error (if any) to the operation context
    - ProfilingTracker - does the profiling job
    - SerilogOperationTracker - logs operation events to serilog

## Sample usage

```
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

            try
            {
                using (var op = Op.Start("root", Op.Context(new {Hello = "World"})))
                {
                    Thread.Sleep(1000);

                    for (int i = 1; i < 4; ++i)
                    {
                        var test = i;

						op.Update($"something new happended: starting #{test}");

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
```
## Release Notes
  - v0.9.0 - Initial release. todo: add tests & samples