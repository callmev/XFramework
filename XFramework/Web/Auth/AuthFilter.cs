using System;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.WebHost.Endpoints.Extensions;
using XFramework.Web.Api;
using SessionFactory = XFramework.Web.Auth.SessionFactory;

namespace XFramework.Web.Auth
{
    public class Auth : RequestFilterAttribute
    {
        private readonly string _functionId = string.Empty;
        public string FunctionId
        {
            get { return this._functionId; }
        }

        public Auth()
        {
            this.Priority = 0;
        }

        public Auth(string functionId)
        {
            this._functionId = functionId;
        }

        public override void Execute(IHttpRequest httpRequest, IHttpResponse httpResponse, object requestDto)
        {
            try
            {
                Check(httpRequest, httpResponse, requestDto);
            }
            catch (Exception exception)
            {
                AppendResponse(httpRequest, httpResponse, exception);
            }
        }

        private void AppendResponse(IHttpRequest httpRequest, IHttpResponse httpResponse, Exception exception)
        {
            var response = new ApiResponse(exception);

            var contentType = httpRequest.ResponseContentType;
            httpResponse.ContentType = contentType;
            httpResponse.WriteToResponse(httpRequest, response);
            // this is the proper way to close the request; you can use Close() but it won't write out global headers
            httpResponse.EndServiceStackRequest();
        }

        private static void Check(IHttpRequest httpRequest, IHttpResponse httpResponse, object requestDto)
        {
            //获取请求信息
            var apiRequest = (ApiRequest)requestDto;
            if (apiRequest == null)
                throw new ArgumentNullException("请求对象为空");

            //获取头信息
            FillApiHeader(apiRequest, httpRequest);
            if (string.IsNullOrEmpty(apiRequest.AuthId) || string.IsNullOrEmpty(apiRequest.AuthToken)) 
                throw new ArgumentNullException("请求头为空");

            //鉴权
            if (IsEnableAuth(httpRequest))
            {
                SessionFactory.Create<object>(httpRequest, requestDto).CheckUserSession();
            }   
        }

        private static void FillApiHeader(ApiRequest apiRequest, IHttpRequest httpRequest)
        {
            var header = new ApiHeaderPicker().PickTokenByBusinessRequest(httpRequest, apiRequest);
            apiRequest.AuthId = header.AuthId;
            apiRequest.AuthToken = header.AuthToken;
        }

        private static bool IsEnableAuth(IHttpRequest httpRequest)
        {
            if (httpRequest.RawUrl.IndexOf("/api/auth", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                httpRequest.RawUrl.IndexOf("/api/assignrole", StringComparison.InvariantCultureIgnoreCase) >= 0)
                return false;

            return true;
        }
    }
}