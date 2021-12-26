using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDZ.RolePermission.Common.Cache
{
    public class MemcacheWriter : ICacheWriter
    {
        private MemcachedClient memcachedClient;
        public MemcacheWriter()
        {
            //分布Memcachedf服务IP 端口
            //string[] servers = { "192.168.1.113:11211", "192.168.202.128:11211" };//可以多台电脑做服务，就是做成Memcached集群
            //string[] servers= { };
            string[] servers = { "127.0.0.1:11211" };//默认为本机作为服务器
            string strAppMemcachedServer = System.Configuration.ConfigurationManager.AppSettings["MemcacheServerList"];
            if (strAppMemcachedServer != null)//说明web.config进行了配置
            {
                servers = strAppMemcachedServer.Split(',');
            }

            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            //设置服务器列表
            pool.SetServers(servers);
            //初始化时创建连接数
            pool.InitConnections = 3;
            //最小连接数
            pool.MinConnections = 3;
            //最大连接数
            pool.MaxConnections = 5;
            //socket连接的超时时间，如果为0表示不超时（单位ms），即一直保持链接状态
            pool.SocketConnectTimeout = 1000;
            //通讯的超时时间，下面设置为3秒（单位ms），
            pool.SocketTimeout = 3000;
            //维护线程的间隔激活时间，下面设置为30秒（单位s），设置为0时表示不启用维护线程
            pool.MaintenanceSleep = 30;
            //设置SocktIO池的故障标志
            pool.Failover = true;
            //是否对TCP/IP通讯使用nalgle算法，
            pool.Nagle = false;
            pool.Initialize();
            //客户端实例
            MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            //是否启用压缩数据：如果启用了压缩，数据压缩长于门槛的数据将被储存在压缩的形式
            mc.EnableCompression = false;
            memcachedClient = mc;
        }

        public void AddCache(string key, object value, DateTime expDate)
        {
            memcachedClient.Add(key, value, expDate);
        }

        public void AddCache(string key, object value)
        {
            memcachedClient.Add(key, value);
        }

        public object GetCache(string key)
        {
            return memcachedClient.Get(key);
        }

        public T GetCache<T>(string key)
        {
            return (T)memcachedClient.Get(key);
        }
        public void SetCache(string key, object value, DateTime expDate)
        {
            memcachedClient.Set(key, value, expDate);
        }

        public void SetCache(string key, object value)
        {
            memcachedClient.Set(key, value);
        }
        /// <summary>
        /// 删除Memcache中的数据
        /// </summary>
        /// <param name="key"></param>
        public bool Delete(string key)
        {
            if (memcachedClient.KeyExists(key))
            {
                return memcachedClient.Delete(key);
            }
            return false;
        }
    }
}
