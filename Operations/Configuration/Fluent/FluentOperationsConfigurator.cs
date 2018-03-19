using System;
using System.Collections.Generic;

namespace Operations.Configuration.Fluent
{
    public class FluentOperationsConfigurator : IOperationsConfigurator
    {
        private readonly List<IOperationTracker> _trackers = new List<IOperationTracker>();

        public virtual TrackingConfiguration Track => new TrackingConfiguration(this, _trackers.Add);

        public virtual Func<IOperationContextFactory> OperationContextFactoryFactoryMethod { get; set; } =
            DefaultFactories.OperationContextFactoryFactoryMethod;

        public virtual Func<IOperationFactory> OperationFactoryFactoryMethod { get; set; } =
            DefaultFactories.OperationFactoryFactoryMethod;

        public virtual Func<IOperationScopeFactory> OperationScopeFactoryFactoryMethod { get; set; } =
            DefaultFactories.OperationScopeFactoryFactoryMethod;

        public virtual Func<IRootOperationTrackerFactory> RootOperationTrackerFactoryFactoryMethod { get; set; } =
            DefaultFactories.RootOperationTrackerFactoryFactoryMethod;

        public virtual OperationsConfiguration Build()
        {
            var configuration = new OperationsConfiguration
            {
                OperationFactoryFactoryMethod = OperationFactoryFactoryMethod,
                OperationScopeFactoryFactoryMethod = OperationScopeFactoryFactoryMethod,
                OperationContextFactoryFactoryMethod = OperationContextFactoryFactoryMethod,
                RootOperationTrackerFactoryFactoryMethod = RootOperationTrackerFactoryFactoryMethod,
                Trackers = _trackers.AsReadOnly()
            };

            return configuration;
        }
    }
}