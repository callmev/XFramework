using System;
using ServiceStack.ServiceHost;
using XFramework.Base;

namespace XFramework.Web.Auth
{
    public class CacheSessionManager<T> : SessionManager<T>
    {
        public CacheSessionManager(IHttpRequest httpRequest, T businessRequest, SessionDto sessionDto)
            : base(httpRequest, businessRequest, sessionDto) { }

        public override void SaveSession()
        {
            lock (syncObj)
            {
                if (SessionDto == null)
                    return;

                if (string.IsNullOrEmpty(SessionDto.AuthToken))
                    return;

                if (SessionDto.State != UserState.Normal)
                    return;

                lock (syncObj)
                {
                    XFrameworkHelper.GetCacheProvider().Update<XUserSession>(SessionDto.AuthToken, this.Build(), TimeSpan.FromSeconds(SessionDto.TimeOut));
                }
            }
        }

        public override void RemoveSession()
        {
            XFrameworkHelper.GetCacheProvider().Remove(this.GetAccessToken());
        }

        public override XUserSession GetSession()
        {
            var accessToken = this.GetAccessToken();
            if (string.IsNullOrEmpty(accessToken))
                return null;

            return XFrameworkHelper.GetCacheProvider().Get<XUserSession>(this.GetAccessToken());
        }
    }
}
