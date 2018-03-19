using System.Collections.Generic;
using System.ComponentModel;
using Operations.Util;

namespace Operations.Internal
{
    /// <summary>
    /// Creates default operation context implementation
    /// <see cref="OperationContext"/>
    /// </summary>
    public class DefaultOperationContextFactory : IOperationContextFactory
    {
        public virtual IOperationContext Create(IDictionary<string, object> initialValues = null)
        {
            return initialValues != null ? new OperationContext(initialValues) : new OperationContext();
        }

        public virtual IOperationContext Create(object initialValues)
        {
            var context = new OperationContext();
            StructuredDataHelper.FillDictionaryWithValuesFromObject(initialValues, context.AddOrUpdate);
            if (initialValues != null)
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(initialValues))
                {
                    var value = property.GetValue(initialValues);
                    context.AddOrUpdate(property.Name, value);
                }
            }

            return context;
        }
    }
}