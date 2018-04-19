using Serilog.Core;
using Serilog.Events;

namespace Operations.Serilog
{
    public class OperationContextEnricher : ILogEventEnricher
    {
        public OperationContextEnricher(ISerilogOperationFormatter formatter, IOperation operation)
        {
            Formatter = formatter;
            Operation = operation;
        }

        protected ISerilogOperationFormatter Formatter { get; }
        protected IOperation Operation { get; }
        public virtual void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var value = Formatter.Format(Operation);
            var property = propertyFactory.CreateProperty("operationContext", value.Data, destructureObjects: value.Destructure);
            logEvent.AddPropertyIfAbsent(property);
        }
    }
}