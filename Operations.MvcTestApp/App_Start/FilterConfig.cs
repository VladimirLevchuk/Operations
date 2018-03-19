using System.Web;
using System.Web.Mvc;
using Operations.Web.Mvc;

namespace Operations.MvcTestApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new OperationsActionFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
