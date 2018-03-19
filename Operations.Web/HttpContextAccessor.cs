using System;
using System.Web;
using JetBrains.Annotations;

namespace Operations.Web
{
    public class HttpContextAccessor
    {
        public static Func<HttpContextBase> HttpContextGetter { get; set; } = 
            () => HttpContext.Current != null ? new HttpContextWrapper(HttpContext.Current) : null;

        [NotNull]
        public static HttpContextBase Current
        {
            get
            {
                var result = CurrentOrNull;
                if (result == null)
                {
                    throw new InvalidOperationException("No http context");
                }

                return result;
            }
        }

        [CanBeNull]
        public static HttpContextBase CurrentOrNull => HttpContextGetter();
    }
}