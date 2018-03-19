using System;
using System.Collections.Generic;
using Operations.Util;

namespace Operations.Trackers.Profiler
{
    public class ProfilerData
    {
        public DateTime? StartTime { get; set; }
        public long? DurationMilliseconds { get; set; }

        public override string ToString()
        {
#pragma warning disable 618 // TODO: revise Formatter concept
            return Formatter.Current.Format(new Dictionary<string, object>
#pragma warning restore 618
            {
                { nameof(StartTime), StartTime },
                { nameof(DurationMilliseconds), DurationMilliseconds }
            });
        }
    }
}