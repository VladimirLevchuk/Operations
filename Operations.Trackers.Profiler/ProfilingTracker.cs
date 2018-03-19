using System;
using System.Collections.Concurrent;
using Operations.Debugging;

namespace Operations.Trackers.Profiler
{
    public class ProfilingTracker : IOperationTracker
    {
        public const string ContextProperty = "profiler";

        public ProfilingTracker() : this(new StopwatchProfilingTimerFactory())
        {}

        public ProfilingTracker(IProfilingTimerFactory timerFactory)
        {
            TimerFactory = timerFactory;
        }

        protected IProfilingTimerFactory TimerFactory { get; }

        private readonly ConcurrentDictionary<string, IProfilingTimer> _profilerData = new ConcurrentDictionary<string, IProfilingTimer>();

        public virtual void StartOperation(IOperation operation)
        {
            operation.Context.AddOrUpdate(ContextProperty, new ProfilerData { StartTime = DateTime.UtcNow });
            var ok = _profilerData.TryAdd(operation.Id, TimerFactory.StartTimer());
            if (!ok)
            {
                OperationsLog.WriteLine(() => $"Unable to start profiling timer for operation {operation}");
            }
        }

        public virtual void FinishOperation(IOperation operation)
        {
            var ok = _profilerData.TryRemove(operation.Id, out IProfilingTimer timer);

            if (ok)
            {
                var data = (ProfilerData)operation.Context.GetOrAdd(ContextProperty, x => new ProfilerData());
                // warning: the update is not thread-safe, but finish event should occur only once, so it's ok

                /* ReSharper disable once PossibleNullReferenceException - 
                 * NRE here is ok: null check is not here for performance reasons, 
                 * and we expect either data created in StartOperation or a newly created object */
                data.DurationMilliseconds = timer.Stop();
            }
            else
            {
                OperationsLog.WriteLine(() => $"unable to find profiling data for operation {operation}");
            }
        }

        public virtual void UpdateOperation(IOperation operation, IOperationProgress progress)
        {
        }

        public virtual void OperationError(IOperation operation, Exception error)
        {
        }
    }
}
