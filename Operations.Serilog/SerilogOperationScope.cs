using System;
using Serilog.Context;

namespace Operations.Serilog
{
    public class SerilogOperationScope : OperationScope, IOperationScope
    {
        private readonly IDisposable _logContextScope;
        private bool _disposed = false;

        public SerilogOperationScope(IOperationsRunner runner, IOperation operation)
            : base(runner, operation)
        {
            this._logContextScope = LogContext.Push(Factories.OperationContextEnricherEnricherFactoryMethod(operation));
        }

        protected override void Dispose(bool disposing)
        {
            // finish operation
            base.Dispose(disposing);

            if (!_disposed)
            {
                if (disposing)
                {
                    _logContextScope.Dispose();
                }

                _disposed = true;
            }
        }
    }
}