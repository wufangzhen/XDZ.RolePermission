using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XDZ.RolePermission.Common;
using XDZ.RolePermission.Common.Cache;
using XDZ.RolePermission.IBLL;
using XDZ.RolePermission.Model;
using XDZ.RolePermission.UI.Portal.Models;

namespace XDZ.RolePermission.UI.Portal.Controllers
{
    //[LoginCheckFilter(IsCheck = false)]//打上次特性，表示不需要校验
    //public class UserLoginController : Controller
    public class UserLoginController : BaseController
    {
        //通过构造方法指明不去校验用户登录
        public UserLoginController()
        {
            IsCheck = false;
        }

        public IUserInfoBll UserInfoBll { get; set; }//导入命名空间，并且通过spring容器注入UserInfoBll属性
        // GET: UserLogin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowVCode()
        {
            Common.ValidateCode validateCode = new ValidateCode();
            string strCode = validateCode.CreateValidateCode(4);
            //把验证码存放到Session，方便后期/（下面）比较
            Session["Vcode"] = strCode;

            byte[] imgBytes = validateCode.CreateValidateGraphic(strCode);
            return File(imgBytes, @"image/jpeg");
        }
        public ActionResult ProcessLogin()
        {
            string strCode = Request["vCode"];
            string sessionCode = Session["Vcode"] as string;
            Session["Vcode"] = null;
            if(string.IsNullOrEmpty(sessionCode))
            {
                return Content("验证码错误");
            }
            if(strCode!= sessionCode)
            {
                return Content("验证码错误");
            }
            string name = Request["LoginCode"];
            string pwd = Request["LoginPwd"];
            short delNormal = (short)XDZ.RolePermission.Model.Enum.DelFlagEnum.Normal;
            var userInfo = UserInfoBll.GetEntities(u => u.UName == name && u.Pwd == pwd && u.DelFlag == delNormal).FirstOrDefault();
            if(userInfo==null)
            {
                return Content("用户名或密码错误！");
            }
            Session["loginUser"] = userInfo;//此处是写操作，往session里面写数据。此句话保留不注释为了Home控制器还需要通过session来判断用户是否登录。因为虽然过滤器写了验证用户是否登录，但验证是发生在Action执行之前，然而Home控制器下LoadUserMenu()不是Action
            //上面用Session记住当前用户改为下面用Memcache+Cookie来代替
            //立即分配一个标志Guid，把标志作为mm存储数据的key，把用户对象放到mm，把guid写到客户端的cookie
            string userLoginId = Guid.NewGuid().ToString();
            //把用户数据写到mm/HttpRuntimeCache 缓存里面去。如何解决变化点：1。可能写到不同地方（机器）去 2.可能同时写到不同地方去
            Common.Cache.CacheHelper.AddCache(userLoginId, userInfo, DateTime.Now.AddMinutes(20));
            //到Common层封装一个mm，现在Common层新建文件夹Cache,下面先新建一个CacheHelper.cs，封装AddCache、GetCache方法，再新建一个ICacheWriter.cs接口，接口里面也就是写AddCache、GetCache方法结构
            //然后分别添加实现ICacheWriter接口的方法。MemcacheWriter.cs和HttpRuntimeCacheWriter.cs（单机版）

            //往客户端写入cookie,没有写过期时间，默认就为会话级别的cookie，也就是浏览器关闭，cookie消失
            Response.Cookies["userLoginId"].Value = userLoginId;

            //记住用户名、密码。下面7句后加的
            HttpCookie ckUid = new HttpCookie("ckUid", userInfo.UName);
            HttpCookie ckPwd = new HttpCookie("ckPwd", userInfo.Pwd);
            ckUid.Expires = DateTime.Now.AddDays(3);
            ckPwd.Expires = DateTime.Now.AddDays(3);
            Response.Cookies["userLoginId"].Expires = DateTime.Now.AddDays(3);
            Response.Cookies.Add(ckUid);
            Response.Cookies.Add(ckPwd);
            //第三步：如果正确跳转到首页
            return Content("ok");

        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            if (Request.Cookies["userLoginId"] != null)
            {
                string key = Request.Cookies["userLoginId"].Value;
                MemcacheWriter memcacheWriter = new MemcacheWriter();
                memcacheWriter.Delete(key);
                Response.Cookies["ckUid"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["ckPwd"].Expires = DateTime.Now.AddDays(-1);
            }
            Session["loginUser"] = null;
            return RedirectToAction("Index", "UserLogin");
        }
        
    }
}