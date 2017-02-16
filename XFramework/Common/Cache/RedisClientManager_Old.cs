using System;
using ServiceStack.CacheAccess;
using ServiceStack.Redis;
using XFramework.Base;
using XFramework.Utilities;

namespace XFramework.Common.Redis
{
    public class RedisClientManager_Old
    {
        private static readonly object Lockobj = new object();

        public ICacheClient CacheClient { get; private set; }

        public RedisConfig RedisConfig { get; private set; }

        private static RedisConfig _config;

        private static RedisClientManager_Old _instance;

        public static RedisClientManager_Old Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                lock (Lockobj)
                {
                    if (_instance != null)
                        return _instance;

                    _instance = new RedisClientManager_Old();
                }

                return _instance;
            }
        }

        private RedisClientManager_Old()
        {
            try
            {
                _config = new RedisConfig()
                {
                    Host = AppSettingHelper.Get<string>("RedisHost"),
                    Port = AppSettingHelper.Get<int>("RedisPort"),
                    Password = AppSettingHelper.Get<string>("RedisPassword"),
                };

                RedisConfig = _config;

                //CacheClient = AppHostBase.Resolve<IRedisClientsManager>().GetClient();
            }
            catch (Exception)
            {
                this.TryGetClientFromConfig();
            }
        }

        private void TryGetClientFromConfig()
        {
            if (string.IsNullOrEmpty(_config.Password))
            {
                CacheClient = new RedisClient(
                    _config.Host,
                    _config.Port);
            }
            else
            {
                CacheClient = new RedisClient(
                   _config.Host,
                   _config.Port,
                   _config.Password);
            }
        }

        private string GenerateRedisConfigString()
        {
            if (_config == null)
                return string.Empty;

            return string.IsNullOrEmpty(_config.Password) ? string.Format("{0}:{1}", _config.Host, _config.Port) : string.Format("{0}@{1}:{2}", _config.Password, _config.Host, _config.Port);
        }

        public void Add<T>(string key, T value)
        {
            Add<T>(key, value, TimeSpan.FromMinutes(GlobalConfig.CacheDataTime));
        }
        
        public void Add<T>(string key, T value, TimeSpan ts)
        {
            using (var redisManager = new PooledRedisClientManager(GenerateRedisConfigString()))
            using (var redis = redisManager.GetClient())
            {
                var authRedis = redis.As<T>();
                //清除
                authRedis.RemoveEntry(key);
                //保存
                authRedis.SetEntry(key, value, ts);
            }
        }

        public void Update<T>(string key, T value)
        {
            Update<T>(key, value, TimeSpan.FromMinutes(GlobalConfig.CacheDataTime));
        }

        public void Update<T>(string key, T value, TimeSpan ts)
        {

        }

        public void Remove(string key)
        {
            using (var redisManager = new PooledRedisClientManager(GenerateRedisConfigString()))
            using (var redis = redisManager.GetClient())
            {
                

                var authRedis = redis.As<T>();
                //清除
                authRedis.RemoveEntry(key);
                //保存
                authRedis.SetEntry(key, value, ts);
            }
        }

        public void Clear()
        {
        }
    }
}