using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Operations.Util;

namespace Operations
{
    public class OperationScope : IOperationScope
    {
        [NotNull]
        public IOperationsRunner Runner { get; }
        [NotNull]
        public IOperation Operation { get; }

        protected IRootOperationTracker RootTracker { get; }

        private bool _disposed = false;

        public OperationScope([NotNull] IOperationsRunner runner, [NotNull] IOperation operation)
        {
            if (runner == null) throw new ArgumentNullException(nameof(runner));
            if (operation == null) throw new ArgumentNullException(nameof(operation));

            Runner = runner;
            Operation = operation;
            RootTracker = runner.RootTracker;
            RootTracker.StartOperation(operation);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    RootTracker.FinishOperation(Operation);
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public virtual IReadOnlyDictionary<string, object> ToDictionary()
        {
            var data = new Dictionary<string, object>
            {
                {"scope", Operation}
            };
            return data;
        }

#pragma warning disable 618 // TODO: revise Formatter concept
        public override string ToString() => Formatter.Current.Format(ToDictionary());
#pragma warning restore 618
    }
}