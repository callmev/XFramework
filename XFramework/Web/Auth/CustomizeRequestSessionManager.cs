using ServiceStack.ServiceHost;
using XFramework.Common.Cache;

namespace XFramework.ServiceStack.Auth
{
    public class CustomizeRequestSessionManager<T> : RequestSessionManager<T>
    {
        public CustomizeRequestSessionManager(
            IHttpRequest httpRequest, T businessRequest, SessionDto sessionDto)
            : base(httpRequest, businessRequest, sessionDto)
        { }

        public CustomizeRequestSessionManager(IHttpRequest httpRequest, T businessRequest)
            : this(httpRequest, businessRequest, null) { }

        /// <summary>
        /// 该方法仅在AuthenticationService服务上调用
        /// </summary>
        public override void SaveRequestSession(int timeOut)
        {
            lock (syncObj)
            {
                if (!base.SessionDto.IsLoginSuccess)
                    return;

                lock (syncObj)
                {
                    CacheHelper.Get<CustomUserSession>(base.GetCookies(), 3 * 3600, () =>
                    {
                        lock (this)
                        {
                            if (CacheHelper.Get(base.GetCookies()) != null)
                                return CacheHelper.Get(base.GetCookies()) as CustomUserSession;

                            return this.Build();
                        }
                    });
                }

            }
        }

        public override void RemoveRequestSession()
        {
            CacheHelper.Remove(base.GetCookies());
        }

        public override CustomUserSession GetRequestSession()
        {
            return CacheHelper.Get(base.GetCookies()) as CustomUserSession;
        }
    }
}