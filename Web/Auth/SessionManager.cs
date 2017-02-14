using System;
using ServiceStack.ServiceHost;
using XFramework.Web.Api;

namespace XFramework.Web.Auth
{
    public abstract class SessionManager<T>
    {
        protected static object syncObj = new object();

        public SessionDto SessionDto { get; private set; }

        public T BusinessRequest { get; private set; }

        public IHttpRequest HttpRequest { get; private set; }

        protected SessionManager(IHttpRequest httpRequest, T businessRequest, SessionDto sessionDto = null)
        {
            this.HttpRequest = httpRequest;
            this.BusinessRequest = businessRequest;
            this.SessionDto = sessionDto;
        }

        public abstract void SaveSession();
        public abstract void RemoveSession();
        public abstract XUserSession GetSession();

        public virtual string GetAccessToken()
        {
            var request = BusinessRequest as ApiRequest;
            return request != null ? request.AuthToken : string.Empty;
        }

        public virtual void CheckUserSession()
        {
            var session = this.GetSession();
            if (session == null)
                throw new Exception("没有登录或登录过期");

            if (!this.IsNotIllegal(session))
                throw new Exception("非法请求");

            if (this.IsNormal(session))
                return;

            if (this.IsNotAuthenticated(session))
                throw new Exception("没有登录或登录过期");

            if (this.IsNotAuthority(session))
                throw new Exception("没有授权");
        }

        private bool IsNormal(XUserSession session)
        {
            if (session == null || session.XUserInfo == null)
                return false;

            return session.XUserInfo.State == UserState.Normal;
        }

        private bool IsNotIllegal(XUserSession session)
        {
            if (session == null || session.XUserInfo == null)
                return false;

            var request = BusinessRequest as ApiRequest;

            return session.XUserInfo.AuthId.Equals(request.AuthId);
        }

        private bool IsNotAuthenticated(XUserSession session)
        {
            if (session == null || session.XUserInfo == null)
                return false;

            return session.XUserInfo.State == UserState.NotAuthenticated;
        }

        private bool IsNotAuthority(XUserSession session)
        {
            if (session == null || session.XUserInfo == null)
                return false;

            return session.XUserInfo.State == UserState.NotAuthority;
        }

        protected XUserSession Build()
        {
            var session = GetSession();
            if (session == null)
                session = new XUserSession();

            session.XUserInfo = SessionDto;
            session.UserAuthId = SessionDto.AuthId;
            session.IsAuthenticated = SessionDto.State == UserState.Normal;

            return session;
        }
    }
}
