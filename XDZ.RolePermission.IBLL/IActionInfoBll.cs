using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDZ.RolePermission.Model;

namespace XDZ.RolePermission.IBLL
{
    public partial interface IActionInfoBll : IBaseBll<ActionInfo>
    {
        IQueryable<ActionInfo> LoadPageData(Model.Param.ActionQueryParam actionQueryParam);
        bool SetRole(int actionId, List<int> roleIds);
    }
}
