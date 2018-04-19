using System.Collections.Generic;
using System.Web;
using Operations.Util;

namespace Operations.Web
{
    public class RequestOperationContext : IStructuredData
    {
        public RequestOperationContext(HttpRequestBase request)
        {
            RawUrl = request.RawUrl;
            Method = request.HttpMethod;
            Url = request.Url?.ToString();
        }

        public string RawUrl { get; }
        public string Url { get; }
        public string Method { get; }

        public virtual string GetOperationName()
        {
            return $"{Method} {RawUrl}";
        }

        public virtual Dictionary<string, object> ToDictionary()
        {
            return StructuredDataHelper.ObjectToDictionary(this);
        }
    }
}