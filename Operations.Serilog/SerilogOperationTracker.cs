using System;
using Operations.Trackers.Profiler;
using Serilog;

namespace Operations.Serilog
{
    public class SerilogOperationTracker : IOperationTracker
    {
        public virtual void StartOperation(IOperation operation)
        {
            Log.ForContext<SerilogOperationTracker>().Debug(() => $"{operation.Name} (#{operation.Id}) - started");
        }

        public virtual void FinishOperation(IOperation operation)
        {
            var duration = (operation.Context.TryGet(ProfilingTracker.ContextProperty) as ProfilerData)?.DurationMilliseconds?.ToString();
            Log.ForContext<SerilogOperationTracker>().Information(
                () => $"{operation.Name} (#{operation.Id}) - done {(duration != null ? $"in {duration}ms" : "")}");
        }
        
        public virtual void UpdateOperation(IOperation operation, IOperationProgress progress)
        {
            Log.ForContext<SerilogOperationTracker>().Debug(() => $"{operation.Name} (#{operation.Id}) updated: {progress}");
        }

        public virtual void OperationError(IOperation operation, Exception error)
        {
            Log.ForContext<SerilogOperationTracker>().Error(error, () => $"{operation.Name} (#{operation.Id}) error: {error.Message}");
        }
    }
}                                                                                                                                                              