using Operations.Configuration;

namespace Operations.Internal
{
    public class DefaultOperationsRunnerFactory
    {
        private readonly IOperationsConfigurator _configurator;

        public DefaultOperationsRunnerFactory(IOperationsConfigurator configurator)
        {
            _configurator = configurator;
        }

        public virtual IOperationsRunner CreateRunner()
        {
            var configuration = _configurator.Build();

            var runner = new DefaultOperationsRunner(configuration.OperationFactoryFactoryMethod(), configuration.OperationScopeFactoryFactoryMethod(),
                configuration.RootOperationTrackerFactoryFactoryMethod().Create(configuration.Trackers));
            return runner;
        }
    }
}