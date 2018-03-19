using System.Collections.Generic;
using JetBrains.Annotations;

namespace Operations
{
    public interface IOperationContextFactory
    {
        [NotNull]
        IOperationContext Create([CanBeNull] IDictionary<string, object> initialValues = null);
        [NotNull]
        IOperationContext Create([CanBeNull] object initialValues);
    }
}