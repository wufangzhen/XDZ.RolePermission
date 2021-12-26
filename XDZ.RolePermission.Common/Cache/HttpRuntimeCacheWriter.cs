using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace XDZ.RolePermission.Common.Cache
{
    public class HttpRuntimeCacheWriter : ICacheWriter
    {
        public void AddCache(string key, object value, DateTime expDate)
        {
            HttpRuntime.Cache.Insert(key, value, null, expDate,TimeSpan.Zero);
        }

        public void AddCache(string key, object value)
        {
            HttpRuntime.Cache.Insert(key,value);
        }

        public bool Delete(string key)
        {
            throw new NotImplementedException();
        }

        public object GetCache(string key)
        {
            return HttpRuntime.Cache[key];
        }

        public T GetCache<T>(string key)
        {
            return (T)HttpRuntime.Cache[key];
        }
        public void SetCache(string key, object value, DateTime expDate)
        {
            HttpRuntime.Cache.Remove(key);//先把缓存数据清掉
            AddCache(key, value, expDate);//然后重新加入
        }

        public void SetCache(string key, object value)
        {
            HttpRuntime.Cache.Remove(key);//先把缓存数据清掉
            AddCache(key, value);//然后重新加入
        }

    }
}
