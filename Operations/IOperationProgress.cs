using JetBrains.Annotations;

namespace Operations
{
    public interface IOperationProgress
    {
        [CanBeNull]
        string ToString();
    }
}