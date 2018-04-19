using System;
using System.Collections;
using Serilog.Core;
using Serilog.Events;

namespace Operations.Serilog
{
    public class Factories
    {
        public static Func<IOperation, ILogEventEnricher> OperationContextEnricherEnricherFactoryMethod { get; set; } =
            (operation) => new OperationContextEnricher(SerilogOperationFormatterFactoryMethod(), operation);

        public static Func<ISerilogOperationFormatter> SerilogOperationFormatterFactoryMethod { get; set; } =
            () => DefaultSerilogOperationFormatter.Default;

        public static void UseDestructuring()
        {
            SerilogOperationFormatterFactoryMethod = () => DestructuringSerilogOperationFormatter.Default;
        }
    }
}