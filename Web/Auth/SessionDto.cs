using System.Collections.Generic;

namespace XFramework.Web.Auth
{
    public class SessionDto
    {
        /// <summary>
        /// 授权Id
        /// </summary>
        public string AuthId { get; private set; }

        /// <summary>
        /// 授权Token
        /// </summary>
        public string AuthToken { get; private set; }

        /// <summary>
        /// 角色集
        /// </summary>
        public List<string> Roles { get; private set; }

        /// <summary>
        /// 权限集
        /// </summary>
        public List<string> Permissions { get; private set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public UserState State { get; private set; }

        /// <summary>
        /// 微信OpenId
        /// </summary>
        public string OpenId { get; private set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; private set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; private set; }

        /// <summary>
        /// 超时时间设置 单位：秒
        /// </summary>
        public int TimeOut { get; private set; }

        public SessionDto(string authId, string authToken, UserState state, int timeOut)
        {
            this.AuthId = authId;
            this.AuthToken = authToken;
            this.State = state;
            this.TimeOut = timeOut;
        }

        public SessionDto(string authId, string authToken, string openId, UserState state, int timeOut)
        {
            this.AuthId = authId;
            this.AuthToken = authToken;
            this.State = state;
            this.TimeOut = timeOut;
            this.OpenId = openId;
        }

        //public SessionDto(string authId, List<string> roles, UserState state, List<string> permissions, int timeOut)
        //{
        //    this.AuthId = authId;
        //    this.Roles = roles;
        //    this.State = state;
        //    this.Permissions = permissions;
        //    this.TimeOut = timeOut;
        //}
    }
}