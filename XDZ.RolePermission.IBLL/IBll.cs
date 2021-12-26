 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDZ.RolePermission.Model;

namespace XDZ.RolePermission.IBLL
{
		
    public partial interface IActionInfoBll:IBaseBll<ActionInfo>
    {
        //IQueryable<ActionInfo> LoadPageData(Model.Param.ActionQueryParam actionQueryParam);
        //bool SetRole(int actionId, List<int> roleIds);
    }
		
    public partial interface IOrderInfoBll:IBaseBll<OrderInfo>
    {
    }
		
    public partial interface IR_UserInfo_ActionInfoBll:IBaseBll<R_UserInfo_ActionInfo>
    {
    }
		
    public partial interface IRoleInfoBll:IBaseBll<RoleInfo>
    {
        IQueryable<RoleInfo> LoadPageData(Model.Param.RoleQueryParam actionQueryParam);
    }
		
    public partial interface IUserInfoBll:IBaseBll<UserInfo>
    {
    }
		
    public partial interface IUserInfoExtBll:IBaseBll<UserInfoExt>
    {
    }
	
}