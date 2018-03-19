using System.Collections.Generic;
using Operations.Trackers.Internal;

namespace Operations.Internal
{
    // todo: add context enrichers in v2
    //public interface IOperationContextEnricher
    //{
    //    void Enrich([NotNull] IOperationScope scope);
    //}

    public class DefaultRootOperationTrackerFactory : IRootOperationTrackerFactory
    {
        public virtual IRootOperationTracker Create(IReadOnlyList<IOperationTracker> trackers)
        {
            return new AggregateSafeOperationTracker(trackers);
        }
    }
}
