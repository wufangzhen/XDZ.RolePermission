using System.Web;
using System.Web.Mvc;
using XDZ.RolePermission.UI.Portal.Models;

namespace XDZ.RolePermission.UI.Portal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new MyExceptionFilterAttribute());
            //由基类控制器实现用户是否登录校验，下面就注释掉
            //filters.Add(new LoginCheckFilterAttribute() { IsCheck = true });
        }
    }
}
