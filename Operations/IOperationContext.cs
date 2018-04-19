using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Operations
{
    /// <summary>
    /// Thread-safe operation context
    /// </summary>
    public interface IOperationContext : IStructuredData
    {
        [NotNull]
        IReadOnlyList<KeyValuePair<string, object>> ToList();
        void AddOrUpdate([NotNull] string key, [CanBeNull] object value);
        void AddIfAbsent([NotNull] string key, [CanBeNull] object value);
        void RemoveIfPresent([NotNull] string key);
        bool TryGet([NotNull] string key, [CanBeNull] out object value);
        [CanBeNull]
        object TryGet([NotNull] string key, [CanBeNull] object fallback = null);
        bool IsEmpty();
        [CanBeNull]
        object GetOrAdd([NotNull] string key, Func<string, object> valueFactory);
        bool Contains([NotNull] string key);
    }
}