using System;
using JetBrains.Annotations;

namespace Operations
{
    public interface IOperationsRunner : IAsyncOperationsRunner
    {
        IOperationScope Start(IOperation operation);
        IOperationScope Start([NotNull] string operationName, [CanBeNull] IOperationContext context = null);
        void Run([NotNull] IOperation operation, [NotNull] Action operationAction);
        void Run([NotNull] string operationName, [NotNull] Action operationAction, [CanBeNull] IOperationContext context);
        
        // TODO: maintain scope in v2.0
        ///// <summary>
        ///// We'll do our best tracking active scope within the runner, 
        ///// but please kep in mind that in async envronments it's not possible
        ///// </summary>
        //IOperationScope ActiveScope { get; }
    }
}