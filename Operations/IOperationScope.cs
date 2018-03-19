using System;
using JetBrains.Annotations;

namespace Operations
{
    public interface IOperationScope : IDisposable
    {
        [NotNull]
        IOperation Operation { get; }
        [NotNull]
        IOperationsRunner Runner { get; }
    }
}