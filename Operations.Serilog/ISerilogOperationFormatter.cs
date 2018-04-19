using JetBrains.Annotations;

namespace Operations.Serilog
{
    public interface ISerilogOperationFormatter
    {
        [NotNull]
        SerilogContextValue Format([NotNull] IOperation operation);
    }
}