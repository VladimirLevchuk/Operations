using System;
using System.Threading.Tasks;

namespace Operations.Internal
{
    public class DefaultOperationsRunner : IOperationsRunner, IDisposable
    {
        public DefaultOperationsRunner(IOperationFactory operationFactory, IOperationScopeFactory operationScopeFactory, IRootOperationTracker tracker)
        {
            OperationFactory = operationFactory;
            OperationScopeFactory = operationScopeFactory;
            RootTracker = tracker;
        }

        public virtual IRootOperationTracker RootTracker { get; }
        public virtual IOperationFactory OperationFactory { get; }
        public virtual IOperationScopeFactory OperationScopeFactory { get; }

        public virtual IOperationScope Start(IOperation operation)
        {
            var scope = OperationScopeFactory.Create(this, operation);
            return scope;
        }

        public virtual IOperationScope Start(string operationName, IOperationContext context = null)
        {
            if (operationName == null) throw new ArgumentNullException(nameof(operationName));
            var operation = OperationFactory.Create(operationName, context);
            var result = Start(operation);
            return result;
        }

        public virtual async Task RunAsync(IOperation operation, Func<Task> operationAction)
        {
            if (operation == null) throw new ArgumentNullException(nameof(operation));
            if (operationAction == null) throw new ArgumentNullException(nameof(operationAction));

            using (var scope = Start(operation))
            {
                try
                {
                    await operationAction();
                }
                catch (Exception e)
                {
                    scope.Error(e);
                    throw;
                }
            }
        }

        public virtual Task RunAsync(string operationName, Func<Task> operationAction, IOperationContext context = null)
        {
            if (operationName == null) throw new ArgumentNullException(nameof(operationName));
            if (operationAction == null) throw new ArgumentNullException(nameof(operationAction));

            var operation = OperationFactory.Create(operationName, context);
            return RunAsync(operation, operationAction);
        }

        public virtual void Run(IOperation operation, Action operationAction)
        {
            if (operation == null) throw new ArgumentNullException(nameof(operation));
            if (operationAction == null) throw new ArgumentNullException(nameof(operationAction));

            using (var scope = Start(operation))
            {
                try
                {
                    operationAction();
                }
                catch (Exception e)
                {
                    scope.Error(e);
                    throw;
                }
            }
        }

        public virtual void Run(string operationName, Action operationAction, IOperationContext context)
        {
            if (operationName == null) throw new ArgumentNullException(nameof(operationName));
            if (operationAction == null) throw new ArgumentNullException(nameof(operationAction));

            var operation = OperationFactory.Create(operationName, context);
            Run(operation, operationAction);
        }

        public virtual void OperationScopeDisposing(IOperationScope scope)
        {
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                RootTracker?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}