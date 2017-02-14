using System;
using ServiceStack.ServiceHost;
using XFramework.Web;

namespace XFramework.ServiceStack.Auth
{
    public abstract class RequestSessionManager<T>
    {
        protected static object syncObj = new object();

        public abstract void SaveRequestSession(int timeOut);
        public abstract void RemoveRequestSession();
        public abstract CustomUserSession GetRequestSession();
        public virtual string GetCookies()
        {
            return CookieManager.GetCookies(this.httpRequest);
        }

        private readonly SessionDto sessionDto;
        public SessionDto SessionDto
        {
            get { return sessionDto; }
        }

        private readonly T businessRequest;
        public T BusinessRequest
        {
            get { return businessRequest; }
        }

        private readonly IHttpRequest httpRequest;
        public IHttpRequest HttpRequest
        {
            get { return httpRequest; }
        }

        public RequestSessionManager(IHttpRequest httpRequest, T businessRequest, SessionDto sessionDto)
        {
            this.httpRequest = httpRequest;
            this.sessionDto = sessionDto;
            this.businessRequest = businessRequest;
        }

        public virtual void CheckUserSession()
        {
            RequestSessionManager<T> rsMgr = RequestSessionFactory.Create<T>(httpRequest, this.businessRequest);
            if (rsMgr.GetCookies().Length == 0)
                //throw new ArgumentException("请求没有携带Cookie值!");

            if (this.IsNormal())
                return;

            CustomUserSession session = this.GetRequestSession();
            if (session == null)
                throw new Exception("CustomUserSession缓存实例NULL");

            if (this.IsNotAuthenticated())
            {
                string loginId = string.Empty;
                if (this.sessionDto != null)
                    loginId = this.sessionDto.LoginId;

                throw new Exception(string.Format("没有登录或登录过期"));
            }

            if (this.IsNotAuthority())
                throw new Exception("没有授权");
        }

        private bool IsNormal()
        {
            CustomUserSession session = this.GetRequestSession();
            if (session == null)
                return false;

            return session.CurrentUserState == UserState.Normal;
        }

        private bool IsNotAuthenticated()
        {
            CustomUserSession session = this.GetRequestSession();
            if (session == null)
                return false;

            return session.CurrentUserState == UserState.NotAuthenticated;
        }

        private bool IsNotAuthority()
        {
            CustomUserSession session = this.GetRequestSession();
            if (session == null)
                return false;

            return session.CurrentUserState == UserState.NotAuthority;
        }

        protected CustomUserSession Build()
        {
            CustomUserSession session = this.GetRequestSession();

            if (this.sessionDto != null)
            {
                session.UserAuthId = this.sessionDto.LoginId;
                session.LoginId = this.sessionDto.LoginId;
                session.RoleId = this.sessionDto.RoleId;
                session.Roles = this.sessionDto.Roles;
                session.TimeOut = this.sessionDto.TimeOut;
            }

            session.IsAuthenticated = true;
            session.CurrentUserState = UserState.Normal;

            return session;
        }
    }
}