using System;

namespace Operations.Trackers
{
    public class StatusTracker : IOperationTracker
    {
        private const string ContextProperty = "status";

        public virtual void StartOperation(IOperation operation)
        {
        }

        public virtual void FinishOperation(IOperation operation)
        {
            operation.Context.AddIfAbsent(ContextProperty, "ok");
        }

        public virtual void UpdateOperation(IOperation operation, IOperationProgress progress)
        {
        }

        private object syncRoot = new object();

        public virtual void OperationError(IOperation operation, Exception error)
        {
            var status = (Status) operation.Context.GetOrAdd(ContextProperty, x => new Status());

            lock (syncRoot)
            {
                status.Errors.Add(new StatusError(error));
            }
        }
    }
}
