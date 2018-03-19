using System.Collections.Generic;

namespace Operations
{
    public interface IRootOperationTrackerFactory
    {
        IRootOperationTracker Create(IReadOnlyList<IOperationTracker> trackers);
    }
}