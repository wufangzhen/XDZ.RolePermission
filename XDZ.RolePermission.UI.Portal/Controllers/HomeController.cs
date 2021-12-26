using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XDZ.RolePermission.IBLL;
using XDZ.RolePermission.Model;

namespace XDZ.RolePermission.UI.Portal.Controllers
{
    //public class HomeController : Controller
  
    public class HomeController : BaseController
    {
        short delflagNormal = (short)XDZ.RolePermission.Model.Enum.DelFlagEnum.Normal;
        public IUserInfoBll UserInfoBll { get; set; }//需要到controllers.xml里面为Home控制器注入UserInfoBll属性；检查Blls.xml文件是否有UserInfoBll的指向
        public IActionInfoBll ActionInfoBll { get; set; }//需要到controllers.xml里面为Home控制器注入ActionInfoBll属性；检查Blls.xml文件是否有ActionInfoBll的指向
        public ActionResult Index()
        {
            //检测用户是否登录,有了全局校验这个不需要，但是 LoadUserMenu()不是Action，所以还是需要
            if (Session["loginUser"] == null)
            {
                return RedirectToAction("Index", "UserLogin");
            }

            //调用获取前台需要的图标数据方法
            ViewBag.AllMenu = LoadUserMenu();
            ViewData["LoginName"] = LoginUser.UName;
            //return View("TreeView"); //树形框架
            return View();//windows桌面框架
        }
        //获取前端需要的图标数据方法
        public List<ActionInfo> LoadUserMenu()
        {
            //1.拿到当前用户ID。LoginUser在BaseController下已经为它赋值了(LoginUser已存在缓存中），知道当前登录用户是谁了
            int userId = this.LoginUser.Id;
            //2.根据用户id查询出当前用户来，就要用到UserInfoBll，添加该属性，然后注入进来
            //2.1问：为什么需要再查询一遍了，BaseController控制下不是已经查询出来了当前登录用户吗？
            //2.2答：因为基类控制下的LoginUser是存放在缓存中的，它不能被上下文跟踪，因此不能使用其导航属性，再次查询出来了就被上下文跟踪，这样就可以使用该用户的导航属性
            //var u = this.LoginUser;
            //u.R_UserInfo_ActionInfo;
            var user = UserInfoBll.GetEntities(u => u.Id == userId).FirstOrDefault();
            //3.拿到当前用户的所有权限【必须是菜单类型的权限】有两条线路权限
            //下面走2号线拿所有权限
            //3.1先通过2号线路拿到用户所有角色,再拿到所有的角色权限id，转为集合
            var allRoles = user.RoleInfo;//拿到用户具有的角色
            var allRoleActionIds = (from r in allRoles
                                    from a in r.ActionInfo
                                    select a.Id).ToList();
            //上面的Linq查询实际上关联了5张表的查询。UserInfo、UserInfoRoleInfo、RoleInfo、RoleInfoActionInfo、ActionInfo
            //3.2拿到该用户在特殊权限表里面已设置为拒绝的权限ID
            var allDenyActionIds = (from r in user.R_UserInfo_ActionInfo
                                    where r.HasPermission == false
                                    select r.ActionInfoId).ToList();
            //说明：用户拥有的特殊权限优先于用户的角色权限
            //4.所以就要把allRoleActionIds与allDenyActionIds做差集，即用户拥有的角色权限要减去被拒绝的权限。
            //var allActionIds = allRoleActionIds.Where(u => !allDenyActionIds.Contains(u));//这句话意思就是allRoleActionIds集合中的元素，但是要不在allDenyActionIds集合中（去掉被拒绝的）。看不懂，用下面的Linq查询也可以
            var allActionIds = (from n in allRoleActionIds
                                where !allDenyActionIds.Contains(n)
                                select n).ToList();
            //5.下面走1号线拿所有权限
            var allUserActionIds = (from t in user.R_UserInfo_ActionInfo
                                    where t.HasPermission == true
                                    select t.ActionInfoId).ToList();
            //6.把1、2两条线的权限进行合并.即把当前用户所有权限拿到
            allActionIds.AddRange(allUserActionIds.AsEnumerable());
            allActionIds = allActionIds.Distinct().ToList();//去掉重复的
                                                            //7.根据权限ID拿到且为菜单的权限   
            var actionList = ActionInfoBll.GetEntities(a => allActionIds.Contains(a.Id) && a.IsMenu == true && a.DelFlag == delflagNormal).ToList();
            return actionList;
        }
       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}