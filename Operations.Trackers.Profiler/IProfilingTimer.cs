namespace Operations.Trackers.Profiler
{
    public interface IProfilingTimer
    {
        /// <summary>
        /// Stops the timer 
        /// </summary>
        /// <returns>returns the elapsed time (in milliseconds)</returns>
        long Stop();
    }
}