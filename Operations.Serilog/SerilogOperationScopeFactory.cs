namespace Operations.Serilog
{
    public class SerilogOperationScopeFactory : IOperationScopeFactory
    {
        public virtual IOperationScope Create(IOperationsRunner runner, IOperation operation)
        {
            return new SerilogOperationScope(runner, operation);
        }
    }
}