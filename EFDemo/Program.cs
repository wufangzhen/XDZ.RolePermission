using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDZ.RolePermission.Model;

namespace EFDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 添加操作1
            ////1.声明/创建一个上下文对象,DataModelContainer在Model层下，所以添加对其的引用
            //DataModelContainer dbContent = new DataModelContainer();
            ////2.创建一个实体user
            //UserInfo user = new UserInfo();
            //user.UName = "zs"+i;
            //user.Pwd = "123";
            //user.ShowName = "张三"+i;
            ////3.告诉EF上下文要对上面的实体做插入操作，要添加对EntityFramework.dll和EntityFramework.SqlServer.dll的引用
            //dbContent.UserInfo.Add(user);
            ////4.告诉EF上下文把实体的变化保存到数据库里去
            //dbContent.SaveChanges();
            ////注意：运行测试前还要把Model层下的App.Config复制替换EFDemo下的App.Config（因为要知道如何连接数据库）
            //Console.ReadKey();
            #endregion
            #region 添加操作2
            ////1.声明/创建一个上下文对象,DataModelContainer在Model层下，所以添加对其的引用
            //DataModelContainer dbContent = new DataModelContainer();
            ////2.创建一个实体user
            //UserInfo user = new UserInfo();
            //user.UName = "ls";
            //user.Pwd = "123456";
            //user.ShowName = "李四";
            ////3.告诉EF上下文要对上面的实体做插入操作，要添加对EntityFramework.dll和EntityFramework.SqlServer.dll的引用
            //dbContent.UserInfo.Attach(user);//将给定的实体附加到上下文中
            ////dbContent.Entry<UserInfo>(user).State = System.Data.EntityState.Added;//EF5.0
            ////还要添加对System.Data.Entity.dll的引用
            //dbContent.Entry<UserInfo>(user).State = System.Data.Entity.EntityState.Added;//EF6.0

            ////4.告诉EF上下文把实体的变化保存到数据库里去
            //dbContent.SaveChanges();
            ////注意：运行测试前还要把Model层下的App.Config复制替换EFDemo下的App.Config（因为要知道如何连接数据库）
            //Console.WriteLine("ok");
            //Console.ReadKey();
            #endregion
            #region 多个实体的变化一次性提交到数据库
            //DataModelContainer dbContent = new DataModelContainer();
            ////1.添加1个用户
            //UserInfo user = new UserInfo();
            //user.UName = "xdz";
            //user.Pwd = "123456";
            //user.ShowName = "小豆子";
            //dbContent.UserInfo.Add(user);
            ////2.添加2个订单
            //OrderInfo order1 = new OrderInfo();
            //order1.Content = "订单满100元";
            //dbContent.OrderInfo.Add(order1);

            //OrderInfo order2 = new OrderInfo();
            //order2.Content = "订单满500元";
            //dbContent.OrderInfo.Add(order2);

            ////3.关联3个实体（1个用户拥有2个订单），有2种方式
            ////3.1 通过用户添加订单实体到自己的导航属性
            //user.OrderInfo.Add(order1);
            ////3.2 通过订单指定用户实体,下面2条语句都可以
            //order2.UserInfo = user;
            ////order2.UserInfoId = user.Id;

            ////4.多个实体变化一次性提交到数据库，减少与数据库的交互，减轻数据库负荷
            //dbContent.SaveChanges();
            //Console.WriteLine("ok");
            //Console.ReadKey();
            #endregion
            #region 修改操作1——修改所有字段
            //DataModelContainer dbContent = new DataModelContainer();
            //UserInfo user = new UserInfo();
            ////没有给定的字段将赋值为null
            //user.Id = 3;
            //user.UName = "ww";
            //user.Pwd = "123";
            //user.ShowName = "王五";
            ////dbContent.UserInfo.Attach(user);
            ////泛型版本写法。这是修改所有字段
            //dbContent.Entry<UserInfo>(user).State = System.Data.Entity.EntityState.Modified;
            ////非泛型版本写法
            ////dbContent.Entry(user).State = System.Data.Entity.EntityState.Modified;
            //dbContent.SaveChanges();
            //Console.WriteLine("ok");
            //Console.ReadKey();
            #endregion
            #region 修改操作1——修改部分字段
            DataModelContainer dbContent = new DataModelContainer();
            UserInfo user = new UserInfo();
            //没有给定的字段将赋值为null
            user.Id = 3;//主键必须给出，否则报错
            user.UName = "wangwu";
            user.Pwd = "12345";
            //dbContent.UserInfo.Attach(user);相当于下面一句
            dbContent.Entry<UserInfo>(user).State = System.Data.Entity.EntityState.Unchanged;
            //泛型版本写法，即强类型
            //dbContent.Entry<UserInfo>(user).Property<string>(u => u.Pwd).IsModified = true;
            //dbContent.Entry<UserInfo>(user).Property<string>(u => u.UName).IsModified = true;
            //非泛型版本写法，即弱类型
            dbContent.Entry<UserInfo>(user).Property("Pwd").IsModified = true;
            dbContent.Entry<UserInfo>(user).Property("UName").IsModified = true;
            dbContent.SaveChanges();
            Console.WriteLine("ok");
            Console.ReadKey();
            #endregion
            #region 删除操作1,只要给出主键字段值即可
            //DataModelContainer dbContent = new DataModelContainer();
            //UserInfo user = new UserInfo();
            //user.Id = 2;
            //dbContent.Entry<UserInfo>(user).State = System.Data.Entity.EntityState.Deleted;
            //dbContent.SaveChanges();
            //Console.WriteLine("ok");
            //Console.ReadKey();
            #endregion
            #region 删除操作2,只要给出主键字段值即可
            //DataModelContainer dbContent = new DataModelContainer();
            //UserInfo user = new UserInfo();
            //user.Id = 1;
            //dbContent.UserInfo.Attach(user);
            //dbContent.UserInfo.Remove(user);
            //dbContent.SaveChanges();
            //Console.WriteLine("ok");
            //Console.ReadKey();
            #endregion
            #region 遍历输出表中所有记录
            //DataModelContainer dbCotent = new DataModelContainer();
            //foreach (var user in dbCotent.UserInfo)
            //{
            //    Console.WriteLine(user.Id+"  "+user.UName);
            //}
            //Console.ReadKey();
            #endregion
            #region Linq to EF查询
            //DataModelContainer dbCotent = new DataModelContainer();
            ////linq查询是一种高效查询，执行时会先执行where条件过滤出符合条件的数据，而不是先查询出所有数据再过滤，在数据库端就过滤了
            ////linq查询返回值是IQuerable<UserInfo>类型，一种接口集合
            ////IQueryable < UserInfo >= from u in dbCotent.UserInfo 与下面一句等效
            ////单条件
            ////var temp = from u in dbCotent.UserInfo
            ////           where u.Id > 5
            ////           select u;
            ////多条件
            //var temp = from u in dbCotent.UserInfo
            //           where u.Id > 5 && u.UName.Contains("zs") || u.UName.StartsWith("x")
            //           select u;
            ////因此查询出来的temp就是一个IQueryable集合，里面装的UserInfo类型数据
            //foreach (var user in temp)
            //{
            //    Console.WriteLine(user.Id + "  " + user.UName);
            //}
            //Console.ReadKey();
            #endregion
            #region 延迟加载，用到的时候才去查询数据库
            //DataModelContainer dbCotent = new DataModelContainer();

            //var temp1 = from u in dbCotent.UserInfo
            //           where u.Id > 5
            //           select u;

            //var temp2 = from u in temp1
            //           where u.Id <10 && u.UName.Contains("zs")
            //           select u;
            ////因此查询出来的temp就是一个IQueryable集合，里面装的UserInfo类型数据
            ////注意：上面有2个Linq to EF语句，但不会执行2次查询，因为都会延迟到用的时候才查询，所以上面2条查询会合并后才执行查询
            //foreach (var user in temp2)
            //{
            //    Console.WriteLine(user.Id + "  " + user.UName);
            //}
            //Console.ReadKey();
            #endregion
            #region 多表查询,通过导航属性多表查询,没有进行表连接查询，数据量大的时候采用
            //DataModelContainer dbCotent = new DataModelContainer();
            //延迟加载
            //var temp = from u in dbCotent.UserInfo
            //            where u.Id > 5
            //            select u;
            ////通过导航属性多表查询,没有进行表连接查询
            //foreach (var user in temp)
            //{
            //    foreach (var orderInfo in user.OrderInfo)
            //    {
            //        Console.WriteLine(user.UName +"  "+orderInfo.Id+"  "+orderInfo.Content);
            //    }
            //}
            //Console.ReadKey();
            #endregion
            #region 多表查询,通过Include方法取消延迟加载，进行表连接查询，，数据量小的时候采用
            //DataModelContainer dbCotent = new DataModelContainer();
            ////用Include即进行了表连接查询，取消了延迟加载
            //var temp = from u in dbCotent.UserInfo.Include("OrderInfo")
            //           where u.Id > 5
            //           select u;
            ////通过导航属性多表查询,没有进行表连接查询
            //foreach (var user in temp)
            //{
            //    foreach (var orderInfo in user.OrderInfo)
            //    {
            //        Console.WriteLine(user.UName + "  " + orderInfo.Id + "  " + orderInfo.Content);
            //    }
            //}
            //Console.ReadKey();
            #endregion
            #region Linq to Object查询
            //DataModelContainer dbCotent = new DataModelContainer();
            ////IQuerable<UserInfo >= from u in dbCotent.UserInfo 与下面一句等效
            //var temp = from u in dbCotent.UserInfo.ToList()
            //           where u.Id > 5
            //           select u;
            ////这里加了ToList，就会先转换为List集合，这条语句就不是Linq to EF，而是Linq to Object。ToList()就会把所有数据加载到内存，然后再根据下面的where条件过滤，即为内存里过滤：就是把数据库中的所有数据查询到程序里面来之后，再进行过滤，如果数据库中数据庞大，内存就会爆掉
            //foreach (var user in temp)
            //{
            //    Console.WriteLine(user.Id + "  " + user.UName);
            //}
            //Console.ReadKey();
            #endregion
            #region List集合（包括Array、Dictionary）和IQueryable集合区别
            //DataModelContainer dbCotent = new DataModelContainer();

            ////linq to ef查询返回值是IQuerable<UserInfo>类型，一种接口集合
            //IQueryable < UserInfo > temp= from u in dbCotent.UserInfo
            //                         where u.Id > 5
            //                         select u;
            ////含义：1.把Linq语句转换成表达式树 2.给元素类型赋值 3.告知由EF解析表达式树
            ////当用到IQueryable集合时（下面语句）由EF解析表达式树，生成SQL语句，然后才执行。属于延迟加载
            //foreach (var user in temp)
            //{
            //    Console.WriteLine(user.Id + "  " + user.UName);
            //}
            //Console.ReadKey();
            #endregion
            #region Lambda查询
            //DataModelContainer dbContent = new DataModelContainer();
            ////var data = dbContent.UserInfo.Where(u => u.Id > 5);
            ////返回值类型也为IQueryable
            //IQueryable<UserInfo> data = dbContent.UserInfo.Where(u => u.Id > 5);
            //foreach (var user in data)
            //{
            //    Console.WriteLine(user.Id+"  "+user.UName);
            //}
            //Console.ReadKey();
            #endregion
            #region Lambda分页查询
            //DataModelContainer dbContent = new DataModelContainer();
            ////1.按Id升序，下面写的是泛型，第1个参数为实体类型，第2个参数为字段类型，也可以不写泛型，直接OrderBy(u => u.Id)。比如OrderBy<UserInfo, string>(u => u.NUame)表示按UName升序
            ////2.降序的话,关键字为OrderByDescending
            ////3.假设一页5条，要查询第3页数据
            //var pageData = dbContent.UserInfo.Where(u => u.Id > 5)
            //              .OrderBy<UserInfo, int>(u => u.Id)//
            //              .Skip(5 * (3 - 1))//4.要越过的记录数
            //              .Take(5);//取多少条记录

            //foreach (var user in pageData)
            //{
            //    Console.WriteLine(user.Id + "  " + user.UName);
            //}
            //Console.ReadKey();
            #endregion
            #region Linq分页查询
            //DataModelContainer dbContent = new DataModelContainer();
            //var pageData = (from u in dbContent.UserInfo
            //                where u.Id > 5
            //                orderby u.Id descending //不写descending默认为升序
            //                select u)
            //                .Skip(10)
            //                .Take(5);

            //foreach (var user in pageData)
            //{
            //    Console.WriteLine(user.Id + "  " + user.UName);
            //}
            //Console.ReadKey();
            #endregion
            #region Linq查询部分列数据
            //DataModelContainer dbContent = new DataModelContainer();
            //var data = from u in dbContent.UserInfo
            //           where u.Id>5
            //           //select u.UName//这种写法只能查询一列
            //           select new { u.Id, u.UName, OrderCount = u.OrderInfo.Count };
            ////new { u.Id, u.UName, OrderCount = u.OrderInfo.Count }为匿名类， OrderCount为自己取得属性名，如u.UName想更改属性名为MyName，只要MyName=u.UName
            //foreach (var user in data)
            //{
            //    Console.WriteLine(user.Id + "  " + user.UName+"  "+user.OrderCount);
            //}
            //Console.ReadKey();
            #endregion
            #region Lambda查询部分列数据
            //DataModelContainer dbContent = new DataModelContainer();
            //var data = dbContent.UserInfo.Where(u => u.Id > 10)
            //           .Select(u => new { u.Id, u.UName, OrderCount = u.OrderInfo.Count });                    
            //foreach (var user in data)
            //{
            //    Console.WriteLine(user.Id + "  " + user.UName + "  " + user.OrderCount);
            //}
            //Console.ReadKey();
            #endregion
        }
    }
}
