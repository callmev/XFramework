using System;
using XFramework.Base;

namespace XFramework.Common.Cache
{
    /// <summary>
    /// Redis缓存操作提供者
    /// </summary>
    public class RedisProvider : ICache
    {
        public void Add<T>(string key, T value)
        {
            RedisClientManager.Item_Set(key, value, TimeSpan.FromMinutes(GlobalConfig.CacheTime));
        }

        public void Add<T>(string key, T value, TimeSpan ts)
        {
            RedisClientManager.Item_Set(key, value, ts);
        }

        public void Update<T>(string key, T value)
        {
            RedisClientManager.Item_Update<T>(key, value, TimeSpan.FromMinutes(GlobalConfig.CacheTime));
        }

        public void Update<T>(string key, T value, TimeSpan ts)
        {
            RedisClientManager.Item_Update<T>(key, value, ts);
        }

        public void Remove(string key)
        {
            RedisClientManager.Item_Remove(key);
        }

        public T Get<T>(string key) where T : class 
        {
            return RedisClientManager.Item_Get<T>(key);
        }
    }
}
