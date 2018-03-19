using System;
using JetBrains.Annotations;

namespace Operations.Configuration.Fluent
{
    public class TrackingConfiguration
    {
        [NotNull] private readonly FluentOperationsConfigurator _configurator;
        [NotNull] private readonly Action<IOperationTracker> _addTracker;

        public TrackingConfiguration([NotNull] FluentOperationsConfigurator configurator, [NotNull] Action<IOperationTracker> addTracker)
        {
            if (configurator == null) throw new ArgumentNullException(nameof(configurator));
            if (addTracker == null) throw new ArgumentNullException(nameof(addTracker));

            _configurator = configurator;
            _addTracker = addTracker;
        }

        public virtual FluentOperationsConfigurator With([NotNull] IOperationTracker tracker)
        {
            if (tracker == null) throw new ArgumentNullException(nameof(tracker));

            _addTracker(tracker);
            return _configurator;
        }

        public virtual FluentOperationsConfigurator With(params IOperationTracker[] trackers)
        {
            foreach (var tracker in trackers)
            {
                With(tracker);
            }

            return _configurator;
        }

        public virtual FluentOperationsConfigurator With<TTracker>()
            where TTracker : IOperationTracker, new()
        {
            return With(new TTracker());
        }
    }
}