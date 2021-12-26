using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XDZ.RolePermission.IBLL;
using XDZ.RolePermission.Model;

namespace XDZ.RolePermission.UI.Portal.Controllers
{
    public class OrderInfoController : Controller
    {
        public IOrderInfoBll OrderInfoBll { get; set; }
            // GET: OrderInfo
        public ActionResult Index()
        {
            //下面要通过OrderInfo去获取OrdeInfo表的所有记录
            ViewData.Model = OrderInfoBll.GetEntities(u => true).ToList();
            return View();
        }
    }
}