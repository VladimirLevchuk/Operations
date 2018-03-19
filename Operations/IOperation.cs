using System.Collections.Generic;
using JetBrains.Annotations;

namespace Operations
{
    /// <summary>
    /// The operation
    /// </summary>
    public interface IOperation
    {
        /// <summary>
        /// Unique operation id
        /// </summary>
        [NotNull]
        string Id { get; }
        /// <summary>
        /// Readable name
        /// </summary>
        [NotNull]
        string Name { get; }
        /// <summary>
        /// Operation context. The mutable part of any operation. 
        /// </summary>
        [NotNull]
        IOperationContext Context { get; }

        IReadOnlyDictionary<string, object> ToDictionary();
    }
}