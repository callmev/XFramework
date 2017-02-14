using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace XFramework.Common.Cache
{
    /// <summary>
    /// 数据缓存
    /// </summary>
    public static class CacheManager
    {
        //>> Based on Factor = 5 default value
        /// <summary>
        /// DayFactor
        /// </summary>
        public static readonly int DayFactor = 86400;
        /// <summary>
        /// HourFactor
        /// </summary>
        public static readonly int HourFactor = 3600;
        /// <summary>
        /// MinuteFactor
        /// </summary>
        public static readonly int MinuteFactor = 60;
        /// <summary>
        /// SecondFactor
        /// </summary>
        public static readonly double SecondFactor = 1;
        /// <summary>
        /// _cache
        /// </summary>
        private static readonly System.Web.Caching.Cache Cache;
        /// <summary>
        /// Factor
        /// </summary>
        private static int _factor = 1;
        /// <summary>
        /// ReSetFactor
        /// </summary>
        /// <param name="cacheFactor"></param>
        public static void ResetFactor(int cacheFactor)
        {
            _factor = cacheFactor;
        }

        /// <summary>
        /// Static initializer should ensure we only have to look up the current cache
        /// instance once.
        /// </summary>
        static CacheManager()
        {
            var context = HttpContext.Current;
            Cache = context != null ? context.Cache : HttpRuntime.Cache;
        }

        /// <summary>
        /// Removes all items from the Cache
        /// </summary>
        public static void Clear()
        {
            var cacheEnum = Cache.GetEnumerator();
            var al = new List<object>();
            while (cacheEnum.MoveNext())
            {
                al.Add(cacheEnum.Key);
            }

            foreach (string key in al)
            {
                Cache.Remove(key);
            }

        }
        /// <summary>
        /// RemoveByPattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public static void RemoveByPattern(string pattern)
        {
            IDictionaryEnumerator cacheEnum = Cache.GetEnumerator();
            var regex = new Regex(
                pattern,
                RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

            while (cacheEnum.MoveNext())
            {
                if (regex.IsMatch(cacheEnum.Key.ToString()))
                    Cache.Remove(cacheEnum.Key.ToString());
            }
        }

        /// <summary>
        /// Removes the specified key from the cache
        /// </summary>
        /// <param name="key">key</param>
        public static void Remove(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// SetServiceObject the current "obj" into the cache. 
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="valueInserted">obj</param>
        public static void Insert(string key, object valueInserted)
        {
            Insert(key, valueInserted, null, 1000);
        }
        /// <summary>
        /// SetServiceObject
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="valueInserted">obj</param>
        /// <param name="dep">dep</param>
        public static void Insert(string key, object valueInserted, CacheDependency dep)
        {
            Insert(key, valueInserted, dep, HourFactor * 12);
        }

        /// <summary>
        /// 数据依赖缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="valueInserted">对象</param>
        /// <param name="connectionString"></param>
        /// <param name="dependencyTableName">与 SqlCacheDependency 关联的数据库表的名称</param>
        /// <param name="databaseEntryName">在应用程序的 Web.config 文件的 caching 的 sqlCacheDependency 的 databases 元素（ASP.NET 设置架构）元素中定义的数据库的名称。 </param>
        public static void Insert(
            string key, object valueInserted, string connectionString, string dependencyTableName, string databaseEntryName)
        {
            SqlCacheDependencyAdmin.EnableNotifications(connectionString);
            SqlCacheDependencyAdmin.EnableTableForNotifications(connectionString, dependencyTableName);
            Insert(key, valueInserted, new SqlCacheDependency(databaseEntryName, dependencyTableName));
        }
        /// <summary>
        /// SetServiceObject
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="valueInserted">obj</param>
        /// <param name="seconds">seconds</param>
        public static void Insert(string key, object valueInserted, int seconds)
        {
            Insert(key, valueInserted, null, seconds);
        }
        /// <summary>
        /// SetServiceObject
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="valueInserted">obj</param>
        /// <param name="seconds">seconds</param>
        /// <param name="priority">priority</param>
        public static void Insert(string key, object valueInserted, int seconds, CacheItemPriority priority)
        {
            Insert(key, valueInserted, null, seconds, priority);
        }
        /// <summary>
        /// SetServiceObject
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="valueInserted">obj</param>
        /// <param name="dep">dep</param>
        /// <param name="seconds">seconds</param>
        public static void Insert(string key, object valueInserted, CacheDependency dep, int seconds)
        {
            Insert(key, valueInserted, dep, seconds, CacheItemPriority.Normal);
        }
        /// <summary>
        /// SetServiceObject
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">obj</param>
        /// <param name="dep">dep</param>
        /// <param name="seconds">seconds</param>
        /// <param name="priority">priority</param>
        public static void Insert(
            string key, object value, CacheDependency dep, int seconds, CacheItemPriority priority)
        {
            if (value != null)
            {
                Cache.Insert(
                    key,
                    value,
                    dep,
                    DateTime.Now.AddSeconds(_factor * seconds), TimeSpan.Zero, priority, null);
            }

        }
        /// <summary>
        /// MicroInsert
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="valueInserted">obj</param>
        /// <param name="secondFactor">secondFactor</param>
        public static void MicroInsert(string key, object valueInserted, int secondFactor)
        {
            if (valueInserted != null)
            {
                Cache.Insert(
                    key,
                    valueInserted,
                    null,
                    DateTime.Now.AddSeconds(_factor * secondFactor), TimeSpan.Zero);
            }
        }
        /// <summary>
        /// 在指定时间间隔内如果没有访问将会自动从缓存中清除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="ts">指定时间间隔</param>
        public static void Insert(string key, object value, TimeSpan ts)
        {
            if (value != null)
                Cache.Insert(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, ts);
        }
        /// <summary>
        /// SetServiceObject an item into the cache for the Maximum allowed time
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">obj</param>
        public static void Max(string key, object value)
        {
            Max(key, value, null);
        }
        /// <summary>
        /// Max
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">obj</param>
        /// <param name="dep">dep</param>
        public static void Max(string key, object value, CacheDependency dep)
        {
            if (value != null)
            {
                Cache.Insert(
                    key,
                    value,
                    dep,
                    DateTime.MaxValue,
                    TimeSpan.Zero,
                    CacheItemPriority.AboveNormal, null);
            }
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return Cache[key];
        }

        public static void Update(string key, object value, TimeSpan ts)
        {
            Remove(key);
            Insert(key, value, ts);
        }

        public delegate T InsertCacheFun<T>();
        /// <summary>
        /// 获取缓存, 如果缓存不存在就执行代理方法添加缓存
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">Key</param>
        /// <param name="getDataFun">获取数据的代理</param>
        /// <param name="second">缓存秒数</param>
        /// <returns>缓存对象</returns>
        public static T Get<T>(string key, int second, InsertCacheFun<T> getDataFun)
        {
            object obj = Cache[key];
            if (obj != null) return (T) obj;

            obj = getDataFun();

            if (second > 0)
                Insert(key, obj, second);

            return (T)obj;
        }

        public static T Get<T>(string key)
        {
            var obj = Cache[key];
            if (obj != null)
            {
                return (T) obj;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// SecondFactorCalculate
        /// </summary>
        /// <param name="seconds">seconds</param>
        /// <returns></returns>
        public static int SecondFactorCalculate(int seconds)
        {
            // SetServiceObject method below takes integer seconds, so we have to round any fractional values
            return Convert.ToInt32(Math.Round((double)seconds * SecondFactor));
        }

    }
}
