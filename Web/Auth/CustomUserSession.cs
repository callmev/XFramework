using System.Linq;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.Auth;

namespace XFramework.ServiceStack.Auth
{
    //A customizeable typed UserSession that can be extended with your own properties
    //To access ServiceStack's Session, Cache, etc from MVC Controllers inherit from ControllerBase<CustomUserSession>
    public class CustomUserSession : AuthUserSession
    {
        public CustomUserSession() { }

        public UserState CurrentUserState { set; get; }
        public string LoginId { get; set; }
        public string RoleId { get; set; }

        // 超时时间设置 单位：秒
        public int TimeOut { get; set; }

        public bool RoleIsRight()
        {
            return null != this.Roles && this.Roles.Any();
        }

        public bool RoleIsMatch(string functionId)
        {
            return this.Roles.Exists(match => functionId.Split('|').Contains(match));
        }

        public static CustomUserSession GetRequestSession<T>(T businessRequest, IHttpRequest httpRequest)
        {
            return RequestSessionFactory.Create<T>(httpRequest, businessRequest).GetRequestSession();
        }

        public void SaveRequestSession<T>(T businessRequest, IHttpRequest httpRequest)
        {
            CustomUserSession userSession =
                GetRequestSession<T>(businessRequest, httpRequest);

            if (userSession != null)
            {
                RequestSessionFactory.Create<T>(httpRequest, businessRequest).
                    SaveRequestSession(userSession.TimeOut);
            }
        }

        public void CheckUserSession<T>(T businessRequest, IHttpRequest httpRequest)
        {
            SessionHandler.CheckUserSession<T>(httpRequest, businessRequest);
        }

        public void ValidateSession(string functionId)
        {
            if (!this.IsAuthenticated)
            {
                this.CurrentUserState = UserState.NotAuthenticated;
                return;
            }

            if (string.IsNullOrEmpty(functionId))
                return;

            if (!this.RoleIsRight())
            {
                this.CurrentUserState = UserState.NotAuthority;
                return;
            }

            if (!this.RoleIsMatch(functionId))
            {
                this.CurrentUserState = UserState.NotAuthority;
                return;
            }

            this.CurrentUserState = UserState.Normal;
        }
    }
}