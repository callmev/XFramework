using ServiceStack.ServiceHost;

namespace XFramework.ServiceStack.Auth
{
    internal class SessionHandler
    {
        public static CustomUserSession GetRequestSession<T>(IHttpRequest serviceStackHttpRequest, T businessRequest)
        {
            return RequestSessionFactory.Create<T>(serviceStackHttpRequest, businessRequest).GetRequestSession();
        }

        public static void CheckUserSession<T>(IHttpRequest serviceStackHttpRequest, T businessRequest)
        {
            RequestSessionFactory.Create<T>(serviceStackHttpRequest, businessRequest).CheckUserSession();
        }
    }
}
