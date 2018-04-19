namespace Operations.Serilog
{
    public class DestructuringSerilogOperationFormatter : ISerilogOperationFormatter
    {
        public SerilogContextValue Format(IOperation operation)
        {
            return SerilogContextValue.CreateDestructured(operation.ToDictionary());
        }

        public static readonly ISerilogOperationFormatter Default = new DestructuringSerilogOperationFormatter();
    }
}