using MVCFilterDemoV2.Models;
using System.Web;
using System.Web.Mvc;

namespace MVCFilterDemoV2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new MyActionFilterAttribute() { Name="全局的"});
        }
    }
}
