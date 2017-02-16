using XFramework.Base;
using XFramework.Web.Api;
using XFramework.Web.Auth;

namespace XFramework.Web.Resource.GetToken
{
    /// <summary>
    /// 获取AuthToken
    /// </summary>
    public class GetTokenEntrance : ApiEntrance<GetTokenRequest>
    {
        public override object OnPost(GetTokenRequest request)
        {
            var authResult = this.Authenticate(request);

            this.SaveSession(request, authResult);

            return authResult;
        }

        protected virtual GetTokenResult Authenticate(GetTokenRequest request)
        {
            return null;
        }

        private void SaveSession(GetTokenRequest request, GetTokenResult result)
        {
            result.AuthToken = XFrameworkHelper.GenerateAuthToken();

            //var dto = new SessionDto(result.AuthId, result.AuthToken, UserState.Normal, result.TimeOut);

            var dto = new SessionDto(result.AuthId, result.AuthToken, request.AuthId, UserState.Normal, result.TimeOut);

            Auth.SessionFactory.Create<GetTokenRequest>(this.Request, request, dto).SaveSession();
        }
    }
}
