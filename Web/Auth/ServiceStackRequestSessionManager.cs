using System;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace XFramework.ServiceStack.Auth
{
    public class ServiceStackRequestSessionManager<T> : RequestSessionManager<T>
    {
        private readonly IHttpRequest httpRequest;

        public ServiceStackRequestSessionManager(
            IHttpRequest httpRequest, T businessRequest, SessionDto dto)
            : base(httpRequest, businessRequest, dto)
        {
            this.httpRequest = httpRequest;
        }

        public ServiceStackRequestSessionManager(IHttpRequest httpRequest, T businessRequest)
            : this(httpRequest, businessRequest, null) { }

        public override void SaveRequestSession(int timeOut)
        {
            lock (syncObj)
            {
                if (base.SessionDto != null)
                {
                    if (base.SessionDto.IsLoginSuccess)
                    {
                        lock (syncObj)
                        {
                            this.httpRequest.SaveSession(this.Build(), TimeSpan.FromSeconds(timeOut));
                        }
                    }
                }
                else
                {
                    if (this.GetRequestSession() != null && this.GetRequestSession().IsAuthenticated)
                    {
                        lock (syncObj)
                        {
                            this.httpRequest.SaveSession(this.Build(), TimeSpan.FromSeconds(timeOut));
                        }
                    }
                }
            }
        }

        public override void RemoveRequestSession()
        {
            this.httpRequest.RemoveSession();
        }

        public override CustomUserSession GetRequestSession()
        {
            return (CustomUserSession)httpRequest.GetSession(false);
        }
    }
}