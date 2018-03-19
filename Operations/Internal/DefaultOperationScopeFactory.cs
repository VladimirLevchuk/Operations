namespace Operations.Internal
{
    public class DefaultOperationScopeFactory : IOperationScopeFactory
    {
        public virtual IOperationScope Create(IOperationsRunner runner, IOperation operation)
        {
            return new OperationScope(runner, operation);
        }
    }
}