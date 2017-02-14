using System;
using log4net;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using XFramework.Base;
using XFramework.Utilities;
using XFramework.Web.Auth;

namespace XFramework.Web.Api
{
    public class ApiEntrance<T> : RestServiceBase<T>
    {
        //private ResponseResult<T> responseResult = null;
        private ApiLog logInfo = new ApiLog();
        ILog _logger = LogManager.GetLogger(typeof(ApiLog));

        public ApiEntrance()
        { }

        #region 重载方法

        protected override void OnBeforeExecute(T businessRequest)
        {
            if (businessRequest == null)
                throw new ArgumentNullException(
                    "OnBeforeExecute(T businessRequest) => businessRequest == null");

            logInfo.Request = businessRequest;

            //this.CollectLogInfo(businessRequest);

            //if (!this.ContainAuthFilterProperty())
            //    return;

            //Auth.Session.SessionFactory.Create<T>(this.Request, businessRequest).CheckUserSession();
        }

        protected override object OnAfterExecute(Object response)
        {
            logInfo.Response = response;

            SaveLogInfo(true);

            //this.responseResult.Result = response;

            //this.logCollector.SetResponseDataProperty(
            //    new SerializeManager().SerializeToJson(this.responseResult));
            //this.logCollector.SaveLog();

            //return this.AppendCookieToResponseResult();



            return new ApiResponse(response);
        }

        protected override object HandleException(IHttpRequest httpReq, T request, Exception exception)
        {
            //if (businessRequest == null)
            //    throw new ArgumentNullException("OnBeforeExecute(T businessRequest) => request == null");

            //this.logCollector.SetExceptionDataProperty(ex);
            //this.logCollector.SaveLog();

            //this.AppendCookieToResponseResult();
            //return this.AppendErrorMessageToResponseResult(exception);

            var response = new ApiResponse(exception);
            logInfo.Response = response;
            SaveLogInfo(false);

            return response;
        }

        #endregion

        #region 私有方法

        private void SaveLogInfo(bool isSuccess)
        {
            var jsonstr = JsonHelper.Serialize(logInfo);

            if (isSuccess)
            {
                _logger.Info("request = >" + jsonstr);
            }
            else
            {
                _logger.Error("request = >" + jsonstr);
            }
        }

        private void CollectLogInfo(T businessRequest)
        {
            logInfo.Request = businessRequest;
            
            //this.logCollector = new LogInfoCollector(
            //    this.Request, SessionFactory.Create<T>(this.Request, businessRequest).GetRequestSession());

            //// 需要优化 （有一个反序列化操作）
            //logCollector.Collect(
            //    businessRequest.GetType(),
            //    new SerializeManager().SerializeToJson(businessRequest));
        }

        private bool ContainAuthFilterProperty()
        {
            var authFilter = this.GetAttribute<Auth.Auth>();
            return authFilter != null;
        }

        //private void BuildResponseResultObject(T businessRequest)
        //{
        //    this.responseResult = new ApiResponse();
        //}

        //private object AppendErrorMessageToResponseResult(Exception ex)
        //{
        //    if (ex != null)
        //    {
        //        this.responseResult.Success = false;
        //        this.responseResult.Message = ex.Message;
        //    }

        //    return this.responseResult;
        //}

        //private object AppendCookieToResponseResult()
        //{
        //    //this.responseResult.Cookies = CookieManager.GetCookies(this.Response);

        //    return this.responseResult;
        //}


        protected XUserSession GetXUserSession(string accessToken)
        {
            return XFrameworkHelper.GetCacheProvider().Get<XUserSession>(accessToken);
        }

        #endregion
    }
}
