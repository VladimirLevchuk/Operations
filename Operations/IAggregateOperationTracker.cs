using System;
using System.Collections.Generic;

namespace Operations
{
    /// <summary>
    /// Aggregates trackers, sequentally calls them for each operation event
    /// </summary>
    public interface IAggregateOperationTracker : IOperationTracker
    {
        IReadOnlyList<IOperationTracker> Trackers { get; }
    }
}