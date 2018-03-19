using JetBrains.Annotations;

namespace Operations
{
    public interface IOperationFactory
    {
        [NotNull]
        IOperation Create([NotNull] string name, [CanBeNull] IOperationContext context = null);
    }
}