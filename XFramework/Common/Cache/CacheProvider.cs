using System;
using XFramework.Base;

namespace XFramework.Common.Cache
{
    /// <summary>
    /// .Net缓存操作提供者
    /// </summary>
    public class CacheProvider : ICache
    {
        public void Add<T>(string key, T value)
        {
            CacheManager.Insert(key, value);
        }

        public void Add<T>(string key, T value, TimeSpan ts)
        {
            CacheManager.Insert(key, value, ts);
        }

        public void Update<T>(string key, T value)
        {
            CacheManager.Update(key, value, TimeSpan.FromMinutes(GlobalConfig.CacheTime));
        }

        public void Update<T>(string key, T value, TimeSpan ts)
        {
            CacheManager.Update(key, value, ts);
        }

        public void Remove(string key)
        {
            CacheManager.Remove(key);
        }

        public T Get<T>(string key) where T : class
        {
             return CacheManager.Get<T>(key);
        }
    }
}
