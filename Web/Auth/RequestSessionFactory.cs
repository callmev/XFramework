using ServiceStack.ServiceHost;

namespace XFramework.ServiceStack.Auth
{
    public static class RequestSessionFactory
    {
        private static int defaultCode = 0;

        /// <summary>
        /// 为保存session使用的工厂创建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpRequest"></param>
        /// <param name="businessRequest"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static RequestSessionManager<T> Create<T>(
            IHttpRequest httpRequest, T businessRequest, SessionDto dto)
        {
            if (defaultCode == 0)
                return new ServiceStackRequestSessionManager<T>(httpRequest, businessRequest, dto);
            else
                return new CustomizeRequestSessionManager<T>(httpRequest, businessRequest, dto);
        }

        /// <summary>
        /// 为检查（CheckUserSession\GetRequestSession使用的工厂创建）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender"></param>
        /// <param name="businessRequest"></param>
        /// <returns></returns>
        public static RequestSessionManager<T> Create<T>(IHttpRequest httpRequest, T businessRequest)
        {
            if (defaultCode == 0)
                return new ServiceStackRequestSessionManager<T>(httpRequest, businessRequest);
            else
                return new CustomizeRequestSessionManager<T>(httpRequest, businessRequest);
        }
    }
}