using System;
using System.Collections.Generic;

namespace Operations.Configuration
{
    public class OperationsConfiguration
    {
        public Func<IOperationContextFactory> OperationContextFactoryFactoryMethod { get; set; }
        public Func<IOperationFactory> OperationFactoryFactoryMethod { get; set; }
        public Func<IOperationScopeFactory> OperationScopeFactoryFactoryMethod { get; set; }
        public Func<IRootOperationTrackerFactory> RootOperationTrackerFactoryFactoryMethod { get; set; }
        public IReadOnlyList<IOperationTracker> Trackers { get; set; }
    }
}