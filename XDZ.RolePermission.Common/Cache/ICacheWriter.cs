using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDZ.RolePermission.Common.Cache
{
    public interface ICacheWriter
    {
        void AddCache(string key, object value, DateTime expDate);
        void AddCache(string key, object value);
        object GetCache(string key);
        T GetCache<T>(string key);
        void SetCache(string key, object value, DateTime expDate);
        void SetCache(string key, object value);
        bool Delete(string key);
    }
}
