using System.Web.Mvc;

namespace Operations.Web.Mvc
{
    public static class ControllerContextExtensions
    {
        public static string GetControllerName(this ControllerContext context)
        {
            return context.RouteData.Values["controller"]?.ToString();
        }

        public static string GetActionName(this ControllerContext context)
        {
            return context.RouteData.Values["action"]?.ToString();
        }
    }
}
