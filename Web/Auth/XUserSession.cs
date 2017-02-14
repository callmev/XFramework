using ServiceStack.ServiceInterface.Auth;

namespace XFramework.Web.Auth
{
    public class XUserSession : AuthUserSession
    {
        public SessionDto XUserInfo;

        //UserAuthId
        //RequestTokenSecret
        //NickName
        //Roles
        //Permissions

        ////用户状态    
        //public UserState State { get; set; }

        ////头像
        //public string Avatar { get; set; }

        ////超时时间（秒）
        //public int TimeOut { get; set; }

        //public void ValidateSession(string functionId)
        //{
        //    if (!this.IsAuthenticated)
        //    {
        //        this.XUserInfo.State = UserState.NotAuthenticated;
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(functionId))
        //        return;

        //    if (!this.RoleIsRight())
        //    {
        //        this.CurrentUserState = UserState.NotAuthority;
        //        return;
        //    }

        //    if (!this.RoleIsMatch(functionId))
        //    {
        //        this.CurrentUserState = UserState.NotAuthority;
        //        return;
        //    }

        //    this.CurrentUserState = UserState.Normal;
        //}
    }
}
