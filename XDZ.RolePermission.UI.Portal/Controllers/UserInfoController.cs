using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XDZ.RolePermission.BLL;
using XDZ.RolePermission.IBLL;
using XDZ.RolePermission.Model;
using XDZ.RolePermission.Model.Param;

namespace XDZ.RolePermission.UI.Portal.Controllers
{
    public class UserInfoController : BaseController
    {
        short delflagNormal = (short)XDZ.RolePermission.Model.Enum.DelFlagEnum.Normal;
        short delflagDeleted = (short)XDZ.RolePermission.Model.Enum.DelFlagEnum.Deleted;
        //IUserInfoBll userInfoBll = new UserInfoBll();
        public IUserInfoBll UserInfoBll { get; set; }
        public IRoleInfoBll RoleInfoBll { get; set; }//注入该属性目的是方便拿到所有角色
        public IActionInfoBll ActionInfoBll { get; set; }//注入该属性目的是方便拿到所有权限
        public IR_UserInfo_ActionInfoBll R_UserInfo_ActionInfoBll { get; set; }//注入该属性目的是方便拿到用户对应的特殊权限
        // GET: UserInfo
        public ActionResult Index()
        {
            //UserInfoBll userInfoBll = new UserInfoBll();
           
            //ViewData.Model = UserInfoBll.GetEntities(u => true);
            return View();
        }
       

        public ActionResult GetAllUserInfos()
        {
            //jquery easyUI:table:{total:32,rows:[{},{}]}//需要json数据格式
            //jquery easyUI:table:在初始化时自动发送以下两个参数值
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;
            //拿到过滤条件用户名和备注的值
            string schName = Request["schName"];
            string schRemark = Request["schRemark"];
            var queryParam = new UserQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Total = total,
                SchName = schName,
                SchRemark = schRemark
            };
            var pageData = UserInfoBll.LoadPageData(queryParam);
            //获取部分列
            var temp = pageData.Select(u => new { ID = u.Id, u.ModifyOn, u.Pwd, u.Remark, u.ShowName, u.SubTime, u.UName });
            var data = new { total = queryParam.Total, rows = temp.ToList() };
            
            //下面转为Json字符串格式，并输出至前台。允许是Get请求
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        //处理添加用户
        public ActionResult Add(UserInfo userInfo)
        {
            userInfo.UName = Request["UName"];//由于前台的name属性值与后台的属性值相同，具有自动装备功能，不需要赋值即可，当然你赋值也可一定，剩下的Pwd等属性值就不赋值了。
            userInfo.ModifyOn = DateTime.Now;
            userInfo.SubTime = DateTime.Now;
            userInfo.DelFlag = (short)XDZ.RolePermission.Model.Enum.DelFlagEnum.Normal;
            UserInfoBll.Add(userInfo);
            return Content("ok");
        }
        //删除
        public ActionResult Delete(string ids)
        {

            if (string.IsNullOrEmpty(ids))
            {
                return Content("请选中要删除的数据");
            }
            //正常处理
            //1逐个删除，在每个循环里面调用Delete方法都要执行DbSession.SaveChanges() > 0，即与数据库发生交互,而且这是真删除，而且以后每个实体都有删除，那么每个控制器都要写这些方法代码。
            //string[] strIds = ids.Split(',');
            //foreach (var strId in strIds)
            //{
            //    UserInfoBll.Delete(new UserInfo() { Id = int.Parse(strId) });
            //}
            //2批量删除，只需与数据库发生一次交互
            string[] strIds = ids.Split(',');
            List<int> idList = new List<int>();
            foreach (var strId in strIds)
            {
                idList.Add(int.Parse(strId));
            }
            //UserInfoBll.DeleteList(idList);//真正删除
            UserInfoBll.DeleteListByLogical(idList);//逻辑删除
            return Content("ok");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserInfo userInfo)
        {
            if(ModelState.IsValid==true)
            {
                UserInfoBll.Add(userInfo);
            }
            return RedirectToAction("Index");
        }
        #region 原先的删除方法
        //public ActionResult Delete(int id)
        //{
        //    ViewData.Model = UserInfoBll.GetEntities(u =>u.Id==id).FirstOrDefault();
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Delete(int id,string s)
        //{
        //    UserInfo userInfo = UserInfoBll.GetEntities(u => u.Id == id).FirstOrDefault();
        //    UserInfoBll.Delete(userInfo);
        //    return RedirectToAction("Index");
        //} 
        #endregion
        #region old Edit
        //public ActionResult Edit(int id)
        //{
        //    ViewData.Model = UserInfoBll.GetEntities(u => u.Id == id).FirstOrDefault();
        //    return View("Update");//（）里没有参数，默认就找与Action文件名相同的视图文件，在此找名为Update的视图。
        //}
        //[HttpPost]
        //public ActionResult Edit(UserInfo userInfo)
        //{
        //    UserInfoBll.Update(userInfo);
        //    return RedirectToAction("Index");
        //} 
        #endregion
        //public ActionResult Details(int id)
        //{
        //    ViewData.Model = UserInfoBll.GetEntities(u => u.Id == id).FirstOrDefault();
        //    return View();
        //}
        #region 修改
        public ActionResult Edit(int id)
        {
            ViewData.Model = UserInfoBll.GetEntities(u => u.Id == id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            UserInfoBll.Update(userInfo);
            return Content("ok");
        }

        #endregion
        public ActionResult SetRole(int id)
        {
            //获取当前要设置的用户
            int userId = id;
            UserInfo user = UserInfoBll.GetEntities(u => u.Id == id).FirstOrDefault();

            //ViewBag与ViewData一样都是往前台传数据的一个属性，但是ViewBag更好，是一个动态类型，以后尽量用它
            //要拿到所有的角色，这就必然要通过RoelInfoBll对象才能拿到，所以还需要把RoelInfoBll属性注入进来本控制器类

            ViewBag.AllRoles = RoleInfoBll.GetEntities(u => u.DelFlag == delflagNormal).ToList();
            //拿到了所有角色之后，前台就好展示，接下来就设置前台

            //用户已关联的角色Id发送到前台。Linq查询
            ViewBag.ExitRoles = (from r in user.RoleInfo select r.Id).ToList();
            return View(user);
        }
        public ActionResult SetAction(int id)
        {
            //获得当前用户
            ViewBag.user = UserInfoBll.GetEntities(u => u.Id == id).FirstOrDefault();
            UserInfo userinfo = ViewBag.user;
            // ViewData.Model表示强类型传入视图，在前台视图需要指明model的类型
            ViewData.Model = ActionInfoBll.GetEntities(u => u.DelFlag == delflagNormal).ToList();

            //用户已关联的角色Id发送到前台。Linq查询
            ViewBag.ExitAction = (from r in userinfo.R_UserInfo_ActionInfo
                                  where r.HasPermission == true && r.UserInfoId == userinfo.Id && r.DelFlag == delflagNormal
                                  select r.ActionInfoId).ToList();
            ViewBag.DelAction = (from r in userinfo.R_UserInfo_ActionInfo
                                 where r.UserInfoId == userinfo.Id && r.DelFlag == delflagDeleted
                                 select r.ActionInfoId).ToList();
            return View();
        }

        #region 处理设置用户角色/给用户设置角色
        public ActionResult ProcessSetRole(int UId)
        {
            //1.拿到当前用户ID——在前台把用户ID放在一个隐藏域里面,赋给name属性，然后通过方法参数UId与前台name属性值相同，就可以自动获取,
            //2.拿到前台所有打上勾的用户角色ID
            List<int> setRoleIdList = new List<int>();
            foreach (var key in Request.Form.AllKeys)
            //获取所有post提交过来的参数名称 例如 某个表单提交了 ID NAME SEX 那Allkeys就是 ID NAME SEX 只是键而已 并不是值
            {
                if (key.StartsWith("ckb_"))//说明该控件为checkbox控件
                {
                    int roleId = int.Parse(key.Replace("ckb_", ""));
                    setRoleIdList.Add(roleId);
                }
            }
            UserInfoBll.SetRole(UId, setRoleIdList);

            return Content("ok");
        }
        #endregion
        //删除特殊权限
        public ActionResult DeleteUserAction(int UId, int ActionId)
        {
            var rUserAction = R_UserInfo_ActionInfoBll.GetEntities(r => r.ActionInfoId == ActionId && r.UserInfoId == UId).FirstOrDefault();
            if (rUserAction != null)
            {
                //rUserAction.DelFlag = (short)XDZ.RolePermission.Model.Enum.DelFlagEnum.Deleted;//此句不行
                //也可以采用调用方法来做,都是逻辑删除
                R_UserInfo_ActionInfoBll.DeleteListByLogical(new List<int>() { rUserAction.Id });
            }
            return Content("ok");
        }
        //设置当前用户的特殊权限UId: uId, ActionId: actionId, Value: value
        //允许、拒绝共用 发送一个异步请求的
        public ActionResult SetUserAction(int UId, int ActionId, int Value)
        {
            var rUserAction = R_UserInfo_ActionInfoBll.GetEntities(r => r.ActionInfoId == ActionId && r.UserInfoId == UId && r.DelFlag == delflagNormal).FirstOrDefault();
            if (rUserAction != null)
            {
                //根据单击的允许还是拒绝进行修改，HasPermission为boolBean
                rUserAction.HasPermission = ((Value == 1) ? true : false);
                R_UserInfo_ActionInfoBll.Update(rUserAction);
            }
            else//不存在就加一条记录
            {
                R_UserInfo_ActionInfo r_UserInfo_ActionInfo = new R_UserInfo_ActionInfo();
                r_UserInfo_ActionInfo.ActionInfoId = ActionId;
                r_UserInfo_ActionInfo.UserInfoId = UId;
                r_UserInfo_ActionInfo.DelFlag = delflagNormal;
                r_UserInfo_ActionInfo.HasPermission = ((Value == 1) ? true : false);
                R_UserInfo_ActionInfoBll.Add(r_UserInfo_ActionInfo);
            }
            return Content("ok");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model">修改密码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangePassword(string Pwd)
        {
            string oldPwd = this.LoginUser.Pwd;
            string newPwd = Request.Form["NewPwd"].ToString();

            string chkNewPwd = Request.Form["ChkNewPwd"].ToString();
            if (oldPwd != Pwd)
            {
                return Content("输入的旧密码不正确！");
            }
            else if (chkNewPwd != newPwd)
            {
                return Content("两次输入的密码不一致！");
            }
            //UserInfo userInfo = UserInfoBll.GetEntities(u => u.Id == this.LoginUser.Id) as UserInfo;
            //修改密码
            UserInfoBll.ModifyPwd(this.LoginUser.Id, newPwd);
            return Content("ok");
        }
    }
}