using System.Collections.Generic;
using System.Web.Mvc;
using Operations.Util;

namespace Operations.Web.Mvc
{
    public class MvcOperationContext : IStructuredData
    {
        public MvcOperationContext(ControllerContext context, string subject)
        {
            ControllerType = context.Controller?.GetType().ToString();
            Controller = context.GetControllerName();
            Action = context.GetActionName();
            Subject = subject;
            var request = context.HttpContext.Request;
            IsChildAction = context.IsChildAction;
            Method = request.HttpMethod;
            RawUrl = request.RawUrl;
        }

        public string RawUrl { get; }
        public string Framework { get; } = "MVC";
        public string Subject { get; }
        public string ControllerType { get; }
        public string Controller { get; }
        public string Action { get; }
        public bool IsChildAction { get; }
        public string Method { get; }
        public virtual string GetOperationName()
        {
            return $"{Method} {RawUrl} - {Subject} {Controller}/{Action}";
        }

        public virtual string GetOperationKey()
        {
            return $"{Controller}/{Action}-{Subject}";
        }

        public virtual Dictionary<string, object> ToDictionary()
        {
            return StructuredDataHelper.ObjectToDictionary(this);
        }
    }
}