 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDZ.RolePermission.IBLL;
using XDZ.RolePermission.Model;
using XDZ.RolePermission.Model.Param;

namespace XDZ.RolePermission.BLL
{
		
       public partial class ActionInfoBll : BaseBll<ActionInfo>,IActionInfoBll
        {    
        public override void SetCurrentDal()
            {
                 CurrentDal=DbSession.ActionInfoDal;
            }
        }
		
       public partial class OrderInfoBll : BaseBll<OrderInfo>,IOrderInfoBll
        {
           public override void SetCurrentDal()
            {
                 CurrentDal=DbSession.OrderInfoDal;
            }
        }
		
       public partial class R_UserInfo_ActionInfoBll : BaseBll<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoBll
        {
           public override void SetCurrentDal()
            {
                 CurrentDal=DbSession.R_UserInfo_ActionInfoDal;
            }
        }
		
       public partial class RoleInfoBll : BaseBll<RoleInfo>,IRoleInfoBll
        {    
        public override void SetCurrentDal()
            {
                 CurrentDal=DbSession.RoleInfoDal;
            }
        }
		
       public partial class UserInfoBll : BaseBll<UserInfo>,IUserInfoBll
        {
           public override void SetCurrentDal()
            {
                 CurrentDal=DbSession.UserInfoDal;
            }
        }
		
       public partial class UserInfoExtBll : BaseBll<UserInfoExt>,IUserInfoExtBll
        {
           public override void SetCurrentDal()
            {
                 CurrentDal=DbSession.UserInfoExtDal;
            }
        }
	
}