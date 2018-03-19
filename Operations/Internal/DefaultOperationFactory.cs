using System;

namespace Operations.Internal
{
    /// <summary>
    /// Creates default operations (Operation)
    /// <see cref="Operation"/>
    /// </summary>
    public class DefaultOperationFactory : IOperationFactory
    {
        public virtual IOperation Create(string name, IOperationContext context = null)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            
            return new Operation(Guid.NewGuid().ToString("N"), name, context);
        }
    }
}