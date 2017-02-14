namespace XFramework.Web.Auth
{
    public enum UserState
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 未认证
        /// </summary>
        NotAuthenticated = -1,

        /// <summary>
        /// 无权限
        /// </summary>
        NotAuthority = -2
    }
}