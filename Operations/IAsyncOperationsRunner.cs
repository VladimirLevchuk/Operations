using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Operations
{
    public interface IAsyncOperationsRunner
    {
        Task RunAsync([NotNull] IOperation operation, [NotNull] Func<Task> operationAction);

        Task RunAsync([NotNull] string operationName, [NotNull] Func<Task> operationAction,
            [CanBeNull] IOperationContext context = null);

        /// <summary>
        /// Gets a root operation tracker for the current runner
        /// </summary>
        IRootOperationTracker RootTracker { get; }
        IOperationFactory OperationFactory { get; }
        IOperationScopeFactory OperationScopeFactory { get; }
    }
}