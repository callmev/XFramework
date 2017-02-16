using XFramework.Common.RabbitMQ;
using XFramework.Utilities;

namespace XFramework.Base
{
    /// <summary>
    /// //获取配置时间webconfig中配置 单位：分钟 防止配置出错，如果出错则默认缓存120分钟
    /// </summary>
    public class GlobalConfig
    {
        /// <summary>
        /// 缓存类型
        /// </summary>
        public static string CacheType { get; private set; }

        /// <summary>
        /// 缓存时间（分钟）
        /// </summary>
        public static int CacheTime { get; private set; }

        /// <summary>
        /// Session存储类型
        /// </summary>
        public static string SessionType { get; private set; }
        
        /// <summary>
        /// Redis连接字符串
        /// 格式：password@host:port
        /// 例：123456@127.0.0.1:6397
        /// </summary>
        public static string RedisPath { get; private set; }

        static GlobalConfig()
        {
            CacheType = AppSettingHelper.Get<string>("CacheType", "cache").ToUpper();
            CacheTime = AppSettingHelper.Get<int>("CacheTime", 120);
            SessionType = AppSettingHelper.Get<string>("SessionType", "cache").ToUpper();
            RedisPath = AppSettingHelper.Get<string>("RedisPath");
        }
    }
}
