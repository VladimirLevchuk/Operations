using System;
using System.Collections.Generic;
using System.Linq;
using Operations.Debugging;

namespace Operations.Trackers.Internal
{
    /// <summary>
    /// Immutable, thread-safe
    /// </summary>
    public class AggregateSafeOperationTracker : IRootOperationTracker
    {
        private readonly IReadOnlyList<IOperationTracker> _trackers;
        public IReadOnlyList<IOperationTracker> Trackers => _trackers;

        public AggregateSafeOperationTracker(IReadOnlyList<IOperationTracker> trackers)
        {
            _trackers = trackers;
        }

        public virtual void StartOperation(IOperation operation)
        {
            Track(operation, (op, tracker) => tracker.StartOperation(op));
        }

        public virtual void FinishOperation(IOperation operation)
        {
            Track(operation, (op, tracker) => tracker.FinishOperation(op));
        }

        public virtual void UpdateOperation(IOperation operation, IOperationProgress progress)
        {
            Track(operation, (op, tracker) => tracker.UpdateOperation(op, progress));
        }

        public virtual void OperationError(IOperation operation, Exception error)
        {
            Track(operation, (op, tracker) => tracker.OperationError(op, error));
        }

        protected virtual void Track(IOperation operation, Action<IOperation, IOperationTracker> trackAction)
        {
            foreach (var tracker in _trackers)
            {
                try
                {
                    trackAction(operation, tracker);
                }
                catch (Exception e)
                {
                    OperationsLog.WriteLine(() =>
                        $"{nameof(AggregateSafeOperationTracker)} tracker {tracker.GetType()} failed to track operation {operation} with error: {e} ");
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var disposable in _trackers.OfType<IDisposable>())
                {
                    disposable.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}