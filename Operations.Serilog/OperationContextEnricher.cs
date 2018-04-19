using System.Linq;
using Serilog.Core;
using Serilog.Events;

namespace Operations.Serilog
{
    public class OperationContextEnricher : ILogEventEnricher
    {
        public OperationContextEnricher(IOperation operation)
        {
            Operation = operation;
        }

        protected IOperation Operation { get; }
        public virtual void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            // todo: use destrcuturing
            var property = propertyFactory.CreateProperty("operationContext", Operation.ToDictionary(), destructureObjects: false);
            logEvent.AddPropertyIfAbsent(property);
        }
    }
}