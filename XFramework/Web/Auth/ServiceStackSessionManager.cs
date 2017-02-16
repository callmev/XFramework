using System;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace XFramework.Web.Auth
{
    public class ServiceStackSessionManager<T> : SessionManager<T>
    {
        public ServiceStackSessionManager(
            IHttpRequest httpRequest, T businessRequest, SessionDto dto)
            : base(httpRequest, businessRequest, dto)
        {
        }

        public override void SaveSession()
        {
            lock (syncObj)
            {
                if (SessionDto != null)
                {
                    if (SessionDto.State == UserState.Normal)
                    {
                        lock (syncObj)
                        {
                            this.HttpRequest.SaveSession(this.Build(), TimeSpan.FromSeconds(SessionDto.TimeOut));
                        }
                    }
                }
                else
                {
                    if (this.GetSession() != null && this.GetSession().IsAuthenticated)
                    {
                        lock (syncObj)
                        {
                            this.HttpRequest.SaveSession(this.Build(), TimeSpan.FromSeconds(SessionDto.TimeOut));
                        }
                    }
                }
            }
        }

        public override void RemoveSession()
        {
            this.HttpRequest.RemoveSession();
        }

        public override XUserSession GetSession()
        {
            return (XUserSession)HttpRequest.GetSession(false);
        }
    }
}