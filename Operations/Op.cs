using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Operations.Configuration;
using Operations.Configuration.Fluent;
using Operations.Internal;
using Operations.Util;

namespace Operations
{
    public static class Op
    {
        public static IOperationsRunner Runner { get; set; }

        public static IOperationScope Start([NotNull] string operationName, [CanBeNull] IOperationContext context = null)
        {
            if (operationName == null) throw new ArgumentNullException(nameof(operationName));
            return Runner.Start(operationName, context);
        }

        public static void Run([NotNull] string operationName, [NotNull] Action operation, 
            [CanBeNull] IOperationContext context = null)
        {
            if (operationName == null) throw new ArgumentNullException(nameof(operationName));
            if (operation == null) throw new ArgumentNullException(nameof(operation));
            Runner.Run(operationName, operation, context);
        }

        public static Task RunAsync([NotNull] string operationName, [NotNull] Func<Task> operation, 
            [CanBeNull] IOperationContext context = null)
        {
            if (operationName == null) throw new ArgumentNullException(nameof(operationName));
            if (operation == null) throw new ArgumentNullException(nameof(operation));
            return Runner.RunAsync(operationName, operation, context);
        }

        public static IRootOperationTracker Tracker => Runner.RootTracker;

        public static IOperationContext Context(object contextValues)
        {
            return OperationContextFactory.Create(contextValues);
        }

        public static IOperationContext Context(IDictionary<string, object> contextValues)
        {
            return OperationContextFactory.Create(contextValues);
        }

        public static IOperationContextFactory OperationContextFactory => OperationContextFactoryFactoryMethod();
        
        public static Func<IOperationContextFactory> OperationContextFactoryFactoryMethod =
            DefaultFactories.OperationContextFactoryFactoryMethod;

        public static DefaultOperationsRunnerFactory Configure(
            Action<FluentOperationsConfigurator> configurationAction)
        {
            var configurator = new FluentOperationsConfigurator();
            configurationAction(configurator);
            var result = new DefaultOperationsRunnerFactory(configurator);
            return result;
        }
    }
}