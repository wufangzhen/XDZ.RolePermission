using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDZ.RolePermission.DALFactory;
using XDZ.RolePermission.EFDAL;
using XDZ.RolePermission.IBLL;
using XDZ.RolePermission.IDAL;
using XDZ.RolePermission.Model;
using XDZ.RolePermission.Model.Param;
using XDZ.RolePermission.NHDAL;

namespace XDZ.RolePermission.BLL
{
    public partial class UserInfoBll : BaseBll<UserInfo>, IUserInfoBll
    {
        public bool ModifyPwd(int id, string newPwd)
        {
            return DbSession.UserInfoDal.ModifyPwd(id, newPwd) > 0;
        }
        public IQueryable<UserInfo> LoadPageData(UserQueryParam userQueryParam)
        {
            short normalFlag = (short)XDZ.RolePermission.Model.Enum.DelFlagEnum.Normal;
            //拿到未删除的数据
            var temp = DbSession.UserInfoDal.GetEntities(u => u.DelFlag == normalFlag);
            //过滤
            if (!string.IsNullOrEmpty(userQueryParam.SchName))
            {
                temp = temp.Where(u => u.UName.Contains(userQueryParam.SchName)).AsQueryable();
            }
            if (!string.IsNullOrEmpty(userQueryParam.SchRemark))
            {
                temp = temp.Where(u => u.Remark.Contains(userQueryParam.SchRemark)).AsQueryable();
            }
            userQueryParam.Total = temp.Count();
            //分页
            return temp.OrderBy(u => u.Id)
                .Skip(userQueryParam.PageSize * (userQueryParam.PageIndex - 1))
                .Take(userQueryParam.PageSize).AsQueryable();
        }

        //UserInfoDal userInfoDal = new UserInfoDal();//变为由接口类型来接收
        //IUserInfoDal userInfoDal = new UserInfoDal();
        //IUserInfoDal userInfoDal = new NHUserInfoDal();
        //设置关联用户角色
        public bool SetRole(int userId, List<int> roleIds)
        {
            //1.获取用户
            var user = DbSession.UserInfoDal.GetEntities(u => u.Id == userId).FirstOrDefault();
            //2.设置用户角色：用户的角色是通过导航属性（RoleInfo）来设置
            //2.1把当前用户的原先角色全部删除
            user.RoleInfo.Clear();
            //2.2拿到所有要设置的角色
            var allRoles = DbSession.RoleInfoDal.GetEntities(r => roleIds.Contains(r.Id));
            //2.3通过遍历把所有角色都加入到当前用户
            foreach (var roleInfo in allRoles)
            {
                user.RoleInfo.Add(roleInfo);
            }
            //3.保存到数据库
            DbSession.SaveChanges();
            return true;
        }

        #region old
        //IUserInfoDal userInfoDal = StaticDalFactory.GetUserInfoDal();
        //public UserInfo Add(UserInfo userInfo)
        //{
        //    return userInfoDal.Add(userInfo);
        //}
        //public bool Update(UserInfo userInfo)
        //{
        //    return userInfoDal.Update(userInfo);
        //} 
        #endregion

        #region 封装DbSession的好处
        ////IDbSession dbsession = new DbSession();
        //IDbSession dbsession = DbSessionFactory.GetCurrentDbSession();
        ////变成依赖接口编程，发生质的变化
        //public void Operate(UserInfo userInfo)
        //{
        //    dbsession.UserInfoDal.Add(userInfo);
        //    dbsession.SaveChanges();
        //    dbsession.UserInfoDal.Add(new UserInfo());
        //    if (dbsession.SaveChanges() > 0)
        //    {
        //    }
        //    dbsession.OrderInfoDal.Delete(new OrderInfo());
        //    dbsession.OrderInfoDal.Update(new OrderInfo());
        //    dbsession.SaveChanges();
        //    //数据提交的权利从数据库访问层提到了业务逻辑层。你看上面4个操作可以一次性提交到数据库（批量操作，大型网站一般都这么做），如果在数据库访问层每个方法都有SaveChanges()方法，那么每操作一次都会与数据库发生交互。而到了业务层来了就非常灵活，可以攒攒一起提交，当然需要的话也可以一个操作结束了就提交,只要写上dbsession.SaveChanges();即可。如果要判断该操作是否成功，可以根据dbsession.SaveChanges() > 0来判断

        //    //return userInfoDal.Add(userInfo);

        //}
        #endregion
        #region 由模板生成
        //public override void SetCurrentDal()
        //{
        //    CurrentDal=DbSession.UserInfoDal;
        //} 
        #endregion
        //public int DeleteList(List<int> ids)
        //{
        //    foreach (var id in ids)
        //    {
        //        DbSession.UserInfoDal.Delete(new UserInfo() { Id = id });//这个{}里非空属性都要赋值的
        //    }
        //    return DbSession.SaveChanges();//这里把SaveChanges方法提到了循环体外，自然就与数据库交互一次
        //}
    }
}
