namespace Operations.Trackers.Profiler
{
    public interface IProfilingTimerFactory
    {
        IProfilingTimer StartTimer();
    }
}