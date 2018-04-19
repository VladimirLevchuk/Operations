using System;
using System.Collections.Generic;
using Operations.Util;

namespace Operations.Trackers.Profiler
{
    public class ProfilerData : IStructuredData
    {
        public DateTime? StartTime { get; set; }
        public long? DurationMilliseconds { get; set; }

        public override string ToString()
        {
#pragma warning disable 618 // TODO: revise Formatter concept
            return Formatter.Current.Format(this.ToDictionary());
#pragma warning restore 618
        }

        public virtual Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                { nameof(StartTime), StartTime },
                { nameof(DurationMilliseconds), DurationMilliseconds }
            };
        }
    }
}