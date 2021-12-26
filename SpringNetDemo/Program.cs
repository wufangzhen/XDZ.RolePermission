using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 传统写法，控制没有反转
            //UserInfoDal userInfoDal = new UserInfoDal();
            //userInfoDal.Show();
            //Console.ReadKey();
            #endregion
            #region 下面走一个容器来创建一个userInfo实例
            //1.先在配置文件App.config文件中添加配置信息
            //2、初始化容器
            IApplicationContext ctx = ContextRegistry.GetContext();
            //2.1这个时候要先复制Spring.net相关程序集文件到当前项目中来。由于用到的是第三方程序集，第三方程序集一般都放在项目根目录下的packages文件夹下（当然再建一个Spring.Net文件夹比较好，然后复制到该文件夹下），Spring.net程序集文件比较多，实际这里只用到Common.Logging.dll和Spring.Core.dll。但是最好还是把所有的程序集文件都复制过来，因为后面可能用到。 
            //2.2添加引用
            //即是添加刚才复制到当项目下的程序集文件 Common.Logging.dll和Spring.Core.dll
            //2.3引入相应的命名空间 
            //3、把需要的实例对象的类(在此为UserInfoDal）配置到容器中去，然后通过容器才能拿到实例对象.即把下面代码
            //IUserInfoDal dal = ctx.GetObject("UserInfoDal") as IUserInfoDal;//方法GetObject中的参数为name属性值，也即是通过name属性值拿到实例对象，强转为IUserInfoDal。这样就拿到了实例对象，并强转为了IUserInfoDal 
            //dal.Show();
            UserInfoBll userInfoBll = ctx.GetObject("UserInfoBll") as UserInfoBll;
            userInfoBll.ShowBll();

           
            Console.ReadKey();

            #endregion

        }
    }
}
