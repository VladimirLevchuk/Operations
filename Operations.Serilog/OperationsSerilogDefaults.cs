using Operations.Configuration;

namespace Operations.Serilog
{
    public static class OperationsSerilogDefaults
    {
        private static readonly SerilogOperationScopeFactory DefaultSerilogOperationScopeFactory = new SerilogOperationScopeFactory();
        public static void Apply()
        {
            DefaultFactories.OperationScopeFactoryFactoryMethod = () => DefaultSerilogOperationScopeFactory;
        }
    }
}