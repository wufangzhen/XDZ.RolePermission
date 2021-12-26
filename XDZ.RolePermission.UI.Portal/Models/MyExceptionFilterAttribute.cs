using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XDZ.RolePermission.UI.Portal.Models
{
    public class MyExceptionFilterAttribute: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);//表示基类的异常处理方法,保留着
            //下面写自己的异常处理方法
            Common.LogHelper.WriteLog(filterContext.Exception.ToString());

        }
    }
}