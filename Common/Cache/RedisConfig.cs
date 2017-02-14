namespace XFramework.Common.Cache
{
    /// <summary>
    /// Redis 配置文件
    /// appSettings
    ///  add key="RedisConfig" value="{Host:localhost,Port:6379,Database:1,Password=null,Timeout:10000}"
    /// appSettings
    /// </summary>
    public class RedisConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public long Database { get; set; }
        public string Password { get; set; }
        public string Timeout { get; set; }
    }
}
