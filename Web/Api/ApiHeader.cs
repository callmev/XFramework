using System;
using ServiceStack.ServiceHost;

namespace XFramework.Web.Api
{
    internal class ApiHeader
    {
        public string AuthId { get; set; }
        public string AuthToken { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(this.AuthId) && !string.IsNullOrEmpty(AuthToken);
        }
    }

    /// <summary>
    /// 根据Web端请求的HttpRequest来提取UserId、TokenId、PublicKey的信息，
    /// 如果HttpRequest的Header中没有获取到，从可测试的角度，从BusinessRequest类中提取。
    /// 生产环境下，都是从HttpRequest中提取。
    /// </summary>
    internal class ApiHeaderPicker
    {
        public ApiHeader PickTokenByAuthenticateRequst(IHttpRequest request, ApiRequest apiRequst)
        {
            if (request.AbsoluteUri.IndexOf("/api/auth", StringComparison.CurrentCultureIgnoreCase) >= 0)
                return null;

            return this.PickTokenByBusinessRequest(request, apiRequst);
        }

        public ApiHeader PickTokenByBusinessRequest(IHttpRequest request, ApiRequest apiRequst)
        {
            var header = new ApiHeader
            {
                AuthId = request.Headers["AuthId"],
                AuthToken = request.Headers["AuthToken"],
            };

            if (header.IsValid())
                return header;

            header = new ApiHeader()
            {
                AuthId = apiRequst.AuthId,
                AuthToken = apiRequst.AuthToken,
            };
            
            return header;
        }
    }
}
