using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDZ.RolePermission.IBLL;
using XDZ.RolePermission.Model;

namespace XDZ.RolePermission.BLL
{
    public partial class OrderInfoBll : BaseBll<OrderInfo>,IOrderInfoBll
    {
        #region 由模板生成
        //public override void SetCurrentDal()
        //{
        //    CurrentDal=DbSession.OrderInfoDal;
        //} 
        #endregion
    }
}
