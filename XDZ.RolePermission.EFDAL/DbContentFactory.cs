using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using XDZ.RolePermission.Model;

namespace XDZ.RolePermission.EFDAL
{
    /// <summary>
    /// 职责用来保证线程内共享一个上下文实例
    /// </summary>
    public class DbContentFactory
    {
        public static DbContext GetCurrentDbContent()
        {
            //return new DataModelContainer();
            //一次请求共用一个上下文实例
            //每一次http请求都会开启一个新的线程，保证在一个线程中,DbContext是唯一的
            //CallContext：是线程内部唯一的数据槽（一块内存空间/容器）,相当于一个键值对数据容器，通过key获取value了,需要引入System.Runtime.Remoting.Messaging;命名空间
            DbContext db = CallContext.GetData("DbContext") as DbContext;
            //从 CallContext 中检索具有指定key“DbContext”的对象，并强转为DbContext
            if (db == null)//线程在数据槽里面没有此上下文
            {
                db = new DataModelContainer();
                CallContext.SetData("DbContext", db);//放到数据槽中去,DbContext是key，db是value
            }
            return db;
        }
    }
}
