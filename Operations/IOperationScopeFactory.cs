using JetBrains.Annotations;

namespace Operations
{
    public interface IOperationScopeFactory
    {
        IOperationScope Create([NotNull] IOperationsRunner runner, [NotNull] IOperation operation);
    }
}