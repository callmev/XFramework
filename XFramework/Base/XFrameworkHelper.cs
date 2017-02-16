using System;
using Ninject;

namespace XFramework.Base
{
    public class XFrameworkHelper
    {
        /// <summary>
        /// 获取缓存操作对象
        /// </summary>
        /// <returns></returns>
        public static Common.Cache.ICache GetCacheProvider()
        {
            return new StandardKernel(new XFrameworkInjectModule()).Get<Common.Cache.ICache>(GlobalConfig.CacheType);
        }

        /// <summary>
        /// 生成唯一的AuthToken
        /// </summary>
        /// <returns></returns>
        public static string GenerateAuthToken()
        {
            //return Guid.NewGuid().ToString().ToLower();
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).TrimEnd('=');
        }
    }
}
