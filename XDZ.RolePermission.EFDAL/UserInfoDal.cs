using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using XDZ.RolePermission.IDAL;
using XDZ.RolePermission.Model;

namespace XDZ.RolePermission.EFDAL
{
    public partial class UserInfoDal:BaseDal<UserInfo>,IUserInfoDal
    {
        //DataModelContainer db = new DataModelContainer();
        //在继承关系中当子类对象赋值给父类变量的情况下，父类变量也可以通过强制转换指向子类变量。DbContentFactory.GetCurrentDbContent()返回值为DbContext，即是DataModelContainer父类。而DataModelContainer继承于DbContext
        DataModelContainer db = DbContentFactory.GetCurrentDbContent() as DataModelContainer;
        public int ModifyPwd(int id, string newPwd)
        {
            #region 方法1            
            //UserInfo user = db.UserInfo.Where(u => u.Id == id).FirstOrDefault();
            //user.Pwd = newPwd;
            //return db.SaveChanges();
            #endregion

            #region 方法2            
            //UserInfo user = new UserInfo();
            //user.Id = id;
            //user.Pwd = newPwd;
            //db.UserInfo.Attach(user); //相当于下面一句
            //db.Entry<UserInfo>(user).Property<string>(u => u.Pwd).IsModified = true;
            //db.Configuration.ValidateOnSaveEnabled = false;//关闭自动验证实体
            //return db.SaveChanges();
            #endregion
            #region 方法3            
            UserInfo user = db.UserInfo.Find(id);
            //user.Id = id;
            user.Pwd = newPwd;
            db.UserInfo.Attach(user); //相当于下面一句
            db.Entry<UserInfo>(user).Property<string>(u => u.Pwd).IsModified = true;
            db.Configuration.ValidateOnSaveEnabled = false;//关闭自动验证实体
            return db.SaveChanges();
            #endregion
        }

        //DataModelContainer db = new DataModelContainer();
        ////先添加XDZ.RolePermission.Model引用，bin导入命名空间
        //public UserInfo GetUserInfoById(int Id)
        //{
        //    //此方法当然可以用ADO.NET来实现——主要就是通过sqlHelper工具类来实现
        //    //在此我们用EF来实现

        //    return db.UserInfo.Find(Id);
        //    //此时在XDZ.RolePermission.EFDAL中还要添加对EntityFramework.dll的引用，如果没有此文件可以复制该文件到该项目的bin\debug目录，然后再添加引用
        //}

        //public List<UserInfo> GetAllUserInfo()
        //{
        //    return db.UserInfo.ToList();//ToList转为泛型集合，会立刻执行查询，将数据存入内存
        //}

        //public IQueryable<UserInfo> GetUsers(Expression<Func<UserInfo,bool>> whereLambda)
        //{
        //    return db.UserInfo.Where(whereLambda).AsQueryable();
        //}
        ////public List<UserInfo> GetUsers(Expression<Func<UserInfo, bool>> whereLambda)
        ////{
        ////    return db.UserInfo.Where(whereLambda).ToList();
        ////}

        //public IQueryable<UserInfo> GetPageUsers<S>(int pageSize,int pageIndex, out int total,Expression<Func<UserInfo, bool>> whereLambda, Expression<Func<UserInfo, S>> orderByLambda,bool isAsc)
        //{
        //    total = db.UserInfo.Where(whereLambda).Count();
        //    if(isAsc)
        //    {
        //        var pageData = db.UserInfo.Where(whereLambda)
        //                      .OrderBy<UserInfo, S>(orderByLambda)
        //                      .Skip(pageSize * (pageIndex - 1))
        //                      .Take(pageSize).AsQueryable();
        //        return pageData;
        //    }
        //    else
        //    {
        //        var pageData = db.UserInfo.Where(whereLambda)
        //                         .OrderByDescending<UserInfo, S>(orderByLambda)
        //                         .Skip(pageSize * (pageIndex - 1))
        //                         .Take(pageSize).AsQueryable();
        //        return pageData;
        //    }
        //}

        //public UserInfo Add(UserInfo userInfo)
        //{
        //    db.UserInfo.Add(userInfo);
        //    db.SaveChanges();
        //    return userInfo;
        //}

        //public bool Update(UserInfo userInfo)
        //{
        //    //所有字段均修改
        //    db.Entry<UserInfo>(userInfo).State = System.Data.Entity.EntityState.Modified;
        //    return db.SaveChanges() > 0;
        //}

        //public bool Delete(UserInfo userInfo)
        //{
        //    db.Entry<UserInfo>(userInfo).State = System.Data.Entity.EntityState.Deleted;
        //    return db.SaveChanges() > 0;
        //}



    }
}
