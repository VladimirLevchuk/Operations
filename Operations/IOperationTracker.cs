using System;
using JetBrains.Annotations;

namespace Operations
{
    /// <summary>
    /// The second core concept of the library.
    /// Represents trackable operation events. 
    /// </summary>
    public interface IOperationTracker
    {
        void StartOperation([NotNull] IOperation operation);
        void FinishOperation([NotNull] IOperation operation);
        void UpdateOperation([NotNull] IOperation operation, [NotNull] IOperationProgress progress);
        void OperationError([NotNull] IOperation operation, [NotNull] Exception error);
    }
}