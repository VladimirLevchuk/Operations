namespace Operations.Trackers.Profiler
{
    public class StopwatchProfilingTimerFactory : IProfilingTimerFactory
    {
        public virtual IProfilingTimer StartTimer()
        {
            return new StopwatchTimer();
        }
    }
}