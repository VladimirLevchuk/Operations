using System;
using Operations.Internal;

namespace Operations.Configuration
{
    public static class DefaultFactories
    {
        private static readonly DefaultOperationContextFactory DefaultOperationContextFactory = new DefaultOperationContextFactory();
        public static Func<IOperationContextFactory> OperationContextFactoryFactoryMethod { get; set; } = () => DefaultOperationContextFactory;

        private static readonly DefaultOperationFactory DefaultOperationFactory = new DefaultOperationFactory();
        public static Func<IOperationFactory> OperationFactoryFactoryMethod { get; set; } =
            () => DefaultOperationFactory;

        private static readonly DefaultOperationScopeFactory DefaultOperationScopeFactory = new DefaultOperationScopeFactory();
        public static Func<IOperationScopeFactory> OperationScopeFactoryFactoryMethod { get; set; } = () => DefaultOperationScopeFactory;

        private static readonly DefaultRootOperationTrackerFactory DefaultRootOperationTrackerFactory = new DefaultRootOperationTrackerFactory();
        public static Func<IRootOperationTrackerFactory> RootOperationTrackerFactoryFactoryMethod { get; set; } = () => DefaultRootOperationTrackerFactory;
    }
}