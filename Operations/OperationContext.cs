using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Operations.Util;

namespace Operations
{
    /// <summary>
    /// The default thread-safe implementation of the operation context.
    /// </summary>
    public class OperationContext : IOperationContext
    {
        private readonly ConcurrentDictionary<string, object> _data;

        public OperationContext()
        {
            _data = new ConcurrentDictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        public OperationContext(IDictionary<string, object> initialValue)
        {
            _data = new ConcurrentDictionary<string, object>(initialValue, StringComparer.OrdinalIgnoreCase);
        }

        public virtual IReadOnlyDictionary<string, object> ToDictionary()
        {
            return new ReadOnlyDictionary<string, object>(_data);
        }

        public virtual IReadOnlyList<KeyValuePair<string, object>> ToList()
        {
            return _data.ToList();
        }

        public virtual void AddOrUpdate(string key, object value)
        {
            _data.AddOrUpdate(key, value, (k, old) => value);
        }

        public virtual void AddIfAbsent(string key, object value)
        {
            _data.TryAdd(key, value);
        }

        public virtual void RemoveIfPresent(string key)
        {
            _data.TryRemove(key, out object _);
        }

        public virtual bool TryGet(string key, out object value)
        {
            return _data.TryGetValue(key, out value);
        }

        public virtual object TryGet(string key, object fallback = null)
        {
            return _data.TryGetValue(key, out object value) ? value : fallback;
        }

        public virtual bool IsEmpty()
        {
            return _data.IsEmpty;
        }

        public virtual object GetOrAdd(string key, Func<string, object> valueFactory)
        {
            return _data.GetOrAdd(key, valueFactory);
        }

        public virtual bool Contains(string key)
        {
            return _data.ContainsKey(key);
        }

        public override string ToString()
        {
#pragma warning disable 618 // TODO: revise Formatter concept
            return Formatter.Current.Format(_data.ToDictionary(x => x.Key, x => x.Value));
#pragma warning restore 618
        }
    }

    // todo: create faster thread-unsafe context implementation
}