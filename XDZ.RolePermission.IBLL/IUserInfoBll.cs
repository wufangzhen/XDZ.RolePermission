using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDZ.RolePermission.Model;

namespace XDZ.RolePermission.IBLL
{
    public partial interface IUserInfoBll:IBaseBll<UserInfo>
    {
        IQueryable<UserInfo> LoadPageData(Model.Param.UserQueryParam userQueryParam);
        bool SetRole(int userId, List<int> roleIds);
        bool ModifyPwd(int id, string newPwd);
    }
}
