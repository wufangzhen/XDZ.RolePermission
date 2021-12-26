using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDZ.RolePermission.EFDAL;
using XDZ.RolePermission.IDAL;

namespace XDZ.RolePermission.DALFactory
{
    public partial class DbSession:IDbSession
    {
        #region 改由模板来生成
        //public IUserInfoDal UserInfoDal
        //{
        //    get { return StaticDalFactory.GetUserInfoDal(); }
        //}
        //public IOrderInfoDal OrderInfoDal
        //{
        //    get { return StaticDalFactory.GetOrderInfoDal(); }
        //} 
        #endregion
        public int SaveChanges()
        {
            return DbContentFactory.GetCurrentDbContent().SaveChanges();
        }
    }
}
