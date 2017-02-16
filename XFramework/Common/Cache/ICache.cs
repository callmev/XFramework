using System;

namespace XFramework.Common.Cache
{
    /// <summary>
    /// 统一缓存接口
    /// </summary>
    public interface ICache
    {
        void Add<T>(string key, T value);

        void Add<T>(string key, T value, TimeSpan ts);

        void Update<T>(string key, T value);

        void Update<T>(string key, T value, TimeSpan ts);

        void Remove(string key);

        T Get<T>(string key) where T : class;
    }
}
