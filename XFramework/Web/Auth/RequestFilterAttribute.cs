using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace XFramework.ServiceStack.Auth
{
    public class CustomAuthenticateAttribute : RequestFilterAttribute
    {
        public override void Execute(
            IHttpRequest httpRequest, IHttpResponse httpRespnse, object requestDto)
        {
            new CustomAuthenHandler().Execute(httpRequest, httpRespnse, requestDto);
        }
    }
}
