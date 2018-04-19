using System.Collections.Generic;
using JetBrains.Annotations;

namespace Operations
{
    public interface IStructuredData
    {
        [NotNull]
        Dictionary<string, object> ToDictionary();
    }
}