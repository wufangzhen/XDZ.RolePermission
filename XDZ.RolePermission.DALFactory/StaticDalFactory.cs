using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XDZ.RolePermission.EFDAL;
using XDZ.RolePermission.IDAL;

namespace XDZ.RolePermission.DALFactory
{
    public  partial class StaticDalFactory
    {
       public static string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];
        #region 改由模板自动生成
        //public static IUserInfoDal GetUserInfoDal()
        //{
        //    //return new UserInfoDal();//简单工厂方法创建实例
        //    //下面为抽象工厂方法创建实例
        //    //return Assembly.Load("XDZ.RolePermission.EFDAL").CreateInstance("XDZ.RolePermission.EFDAL.UserInfoDal") as IUserInfoDal;          
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserInfoDal") as IUserInfoDal;
        //}
        //public static IOrderInfoDal GetOrderInfoDal()
        //{
        //    //return new UserInfoDal();//简单工厂方法创建实例
        //    //下面为抽象工厂方法创建实例
        //    //return Assembly.Load("XDZ.RolePermission.EFDAL").CreateInstance("XDZ.RolePermission.EFDAL.UserInfoDal") as IUserInfoDal;
        //    //string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".OrderInfoDal") as IOrderInfoDal;
        //} 
        #endregion
    }
}
