using System;
using ServiceStack.ServiceHost;
using XFramework.Base;

namespace XFramework.Web.Auth
{
    public static class SessionFactory
    {
        /// <summary>
        /// 为保存session使用的工厂创建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpRequest"></param>
        /// <param name="businessRequest"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static SessionManager<T> Create<T>(
            IHttpRequest httpRequest, T businessRequest, SessionDto dto = null)
        {
            switch (GlobalConfig.SessionType)
            {
                case "CACHE":
                    {
                        return new CacheSessionManager<T>(httpRequest, businessRequest, dto);
                    }
                case "SERVICESTACK":
                    {
                        return new ServiceStackSessionManager<T>(httpRequest, businessRequest, dto);
                    }
                default:
                    {
                        throw new Exception("没有找到合适的session操作对象");
                    }
            }
        }
    }
}