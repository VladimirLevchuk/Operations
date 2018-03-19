using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using Operations.Util;

namespace Operations
{
    /// <summary>
    /// The default implementation of the <see cref="IOperation"/>
    /// - immutable operation with mutable context
    /// </summary>
    [DebuggerDisplay("{ToString()}", Name = "{Name}")]
    public class Operation : IOperation
    {
        public Operation([NotNull] string id, [NotNull] string name, 
            [CanBeNull] IOperationContext context = null)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (name == null) throw new ArgumentNullException(nameof(name));
            Id = id;
            Name = name;
            Context = context ?? new OperationContext();
        }

        public string Id { get; }
        public virtual string Name { get;  }
        public IOperationContext Context { get; }

        public virtual IReadOnlyDictionary<string, object> ToDictionary()
        {
            var data = new Dictionary<string, object>
            {
                { nameof(Name), Name },
                { nameof(Id), Id },
                { nameof(Context), Context.ToDictionary() }
            };

            return data;
        }
#pragma warning disable 618 // TODO: revise Formatter concept
        public override string ToString() => Formatter.Current.Format(ToDictionary());
#pragma warning restore 618
    }
}
