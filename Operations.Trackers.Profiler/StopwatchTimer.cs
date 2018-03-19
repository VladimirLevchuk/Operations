using System.Diagnostics;

namespace Operations.Trackers.Profiler
{
    public class StopwatchTimer : IProfilingTimer
    {
        private readonly Stopwatch _stopwatch;

        public StopwatchTimer()
        {
            _stopwatch = Stopwatch.StartNew();
        }
        public long Stop()
        {
            _stopwatch.Stop();
            return _stopwatch.ElapsedMilliseconds;
        }
    }
}