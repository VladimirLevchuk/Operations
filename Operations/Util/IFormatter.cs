using System.Collections.Generic;
using JetBrains.Annotations;

namespace Operations.Util
{
    public interface IFormatter
    {
        [NotNull]
        string Format([CanBeNull] object value);
        [NotNull]
        string Format([CanBeNull] IDictionary<string, object> values);
    }
}