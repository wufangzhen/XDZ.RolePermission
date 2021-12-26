using MVCFilterDemoV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFilterDemoV2.Controllers
{
    [MyActionFilterAttribute(Name = "过滤器测试 特性打在类上")]
    public class FilterController : Controller
    {
        // GET: Filter
        [MyActionFilterAttribute(Name = "过滤器测试特性打在Action上")]
        public ActionResult Index()
        {
            Response.Write("<br />Action执行了 <br />");
            return View();
        }
        public ActionResult About()
        {
            Response.Write("<br />About 的 Action执行了 <br />");
            return View();
        }
    }
}