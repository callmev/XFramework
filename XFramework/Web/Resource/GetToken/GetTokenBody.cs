namespace XFramework.Web.Resource.GetToken
{
    public class GetTokenBody
    {
        public GrantType GrantType { get; set; }

        public string AuthId { get; set; }

        public string Secret { get; set; }
    }

    public enum GrantType
    {
        /// <summary>
        /// 账户
        /// </summary>
        Account,

        /// <summary>
        /// 微信公众平台
        /// </summary>
        WeiXinPublicPlatform,
    }
}
