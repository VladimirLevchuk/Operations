using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Operations.Util
{
    public class ToStringFormatter : IFormatter
    {
        public static readonly string NullString = "null";

        public virtual string Format(object value)
        {
            var dictionary = value as IDictionary<string, object>;
            return dictionary != null 
                ? Format(dictionary) 
                :  value != null ? $"\"{value}\"" : NullString;
        }

        public virtual string Format(IDictionary<string, object> values)
        {
            if (values == null)
                return NullString;

            var buffer = new StringBuilder();
            bool first = true;
            buffer.AppendLine("{");
            foreach (var pair in values.ToList())
            {
                buffer.AppendLine($"{(first ? string.Empty : ", ")} \"{pair.Key}\": {Format(pair.Value)}");
                first = false;
            }
            buffer.AppendLine("}");
            return buffer.ToString();
        }
    }

    public class JsonFormatter : IFormatter
    {
        public static JsonSerializerSettings DefaultSettings { get; set; } = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public JsonFormatter(JsonSerializerSettings settings)
        {
            Settings = settings;
        }

        protected JsonSerializerSettings Settings { get; }

        public virtual string Format(object value)
        {
            return JsonConvert.SerializeObject(value, Settings);
        }

        public virtual string Format(IDictionary<string, object> values)
        {
            return JsonConvert.SerializeObject(values, Settings);
        }
    }
}