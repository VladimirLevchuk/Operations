using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Operations.Util
{
    public class StructuredDataHelper
    {
        public static Dictionary<string, object> ObjectToDictionary(object values)
        {
            var result = new Dictionary<string, object>();
            FillDictionaryWithValuesFromObject(values, (key, value) => result[key] = value);
            return result;
        }

        internal static void FillDictionaryWithValuesFromObject(object values,
            Action<string, object> addValueToDictionary)
        {
            if (values != null)
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(values))
                {
                    var propertyValue = property.GetValue(values);
                    addValueToDictionary(property.Name, propertyValue);
                }
            }
        }
    }
}
