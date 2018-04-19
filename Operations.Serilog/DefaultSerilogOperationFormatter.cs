namespace Operations.Serilog
{
    public class DefaultSerilogOperationFormatter : ISerilogOperationFormatter
    {
        public virtual SerilogContextValue Format(IOperation operation)
        {
            return SerilogContextValue.Create(operation.ToDictionary());
        }

        public static readonly ISerilogOperationFormatter Default = new DefaultSerilogOperationFormatter();
    }
}