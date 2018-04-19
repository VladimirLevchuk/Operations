using System.Diagnostics;
using System.Web;
using Operations.Debugging;
using Operations.MvcTestApp;
using Operations.Serilog;
using Operations.Trackers;
using Operations.Trackers.Profiler;
using Operations.Util;
using Serilog;
using Serilog.Debugging;
using SerilogWeb.Classic.Enrichers;

namespace Operations.MvcTestApp
{
    public class OperationsConfig
    {
        public static void Startup()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                // The following requires https://getseq.net/
                .WriteTo.Seq("http://localhost:5341/")
                .ConfigureDefaultOperationsDestructuring()
                .Enrich.FromLogContext()
                .Enrich.With<HttpRequestIdEnricher>()
                .Enrich.With<HttpRequestTraceIdEnricher>()
                .Enrich.With<UserNameEnricher>()
                .Enrich.FromLogContext()
                .CreateLogger();

            SelfLog.Enable(msg =>
            {
                Debug.WriteLine(msg);
            });

            OperationsLog.Enable(msg => Log.ForContext<OperationsConfig>().Warning(msg));
            OperationsSerilogDefaults.Apply();
            Operations.Serilog.Factories.UseDestructuring();

            Op.Runner = Op.Configure(x => x
                    .Track.With<StatusTracker>()
                    .Track.With<ProfilingTracker>()
                    .Track.With<SerilogOperationTracker>())
                .CreateRunner();
        }
    }
}