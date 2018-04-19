using System;

namespace Operations.Trackers
{
    public class StatusTracker : IOperationTracker
    {
        private const string ContextProperty = "status";

        public virtual void StartOperation(IOperation operation)
        {
            operation.Context.AddIfAbsent(ContextProperty, Status.Started());
        }

        public virtual void FinishOperation(IOperation operation)
        {
            var status = operation.Context.TryGet(ContextProperty) as Status;

            if (status != null)
            {
                status.Finish();
            }
            else
            {
                operation.Context.AddOrUpdate(ContextProperty, Status.Unknown());
            }
        }

        public virtual void UpdateOperation(IOperation operation, IOperationProgress progress)
        {
            var status = (Status) operation.Context.GetOrAdd(ContextProperty, key => Status.Started()) ?? Status.Started();

            status.Update(progress.ToString());
        }

        private object syncRoot = new object();

        public virtual void OperationError(IOperation operation, Exception error)
        {
            var status = (Status)operation.Context.GetOrAdd(ContextProperty, key => Status.Started()) ?? Status.Started();

            lock (syncRoot)
            {
                status.Error(error);
            }
        }
    }
}
