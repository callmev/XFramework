using ServiceStack.ServiceHost;
using XFramework.Web.Api;

namespace XFramework.Web.Resource.GetToken
{
    [Route("/GetToken", "Post",
    Summary = "认证",
    Notes = @"<br>
                    <PRE>
                        <br><strong>Request</strong>        
                        <br>    Body                     需要的认证信息
                        <br>        GrantType                登录类型
                        <br>        AuthId                   登录名
                        <br>        Secret                   密码
                    </PRE>
                    <PRE>
                        <br><strong>Response</strong>
                        <br>    Success                  执行结果是否成功
                        <br>    Message                  执行结果描述
                        <br>    Result                   结果
                        <br>        AuthId                   登录名
                        <br>        AuthToken                授权码
                        <br>        TimeOut                  时效（秒）
                    </PRE>")]
    public class GetTokenRequest : ApiRequest
    {
        public GetTokenBody Body { get; set; }
    }
}
