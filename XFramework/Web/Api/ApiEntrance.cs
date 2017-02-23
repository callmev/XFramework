using System;
using System.Diagnostics;
using log4net;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using XFramework.Common.Ioc;
using XFramework.Utilities;
using XFramework.Web.ApiLog;
using XFramework.Web.Auth;

namespace XFramework.Web.Api
{
    public class ApiEntrance<T> : RestServiceBase<T>
    {
        private ApiLog.ApiLog logInfo = new ApiLog.ApiLog();
        private Stopwatch _stopWatch = new Stopwatch();
        private ApiLogMode _logMode = ApiLogMode.Both;
        private XUserSession _xUserSession;
        ILog _logger = LogManager.GetLogger(typeof(ApiLog.ApiLog));

        public ApiEntrance()
        { }

        #region 重载方法

        protected override void OnBeforeExecute(T businessRequest)
        {
            _stopWatch.Start();
            _xUserSession = Auth.SessionFactory.Create<T>(this.Request, businessRequest).GetSession();

            this.ReadApiLogMode();

            this.StartLog(businessRequest);
        }

        protected override object OnAfterExecute(Object response)
        {
            var result = new ApiResponse(response);

            EndLog(result);
            
            return result;
        }

        protected override object HandleException(IHttpRequest httpReq, T request, Exception ex)
        {
            var message = string.Format("Api处理失败 => url:{0} request:{1}", logInfo.ApiUrl, logInfo.RequestData);
            _logger.Error(message, ex);

            logInfo.Success = false;
            logInfo.Exception = ex.Message;

            var response = new ApiResponse(ex);

            EndLog(response);

            return response;
        }

        #endregion

        #region 私有方法

        private void StartLog(T businessRequest)
        {
            logInfo.ApiType = businessRequest.GetType().ToString();
            //logInfo.ApiKey = string.Empty;
            logInfo.ApiUrl = this.Request.RawUrl;
            logInfo.RequestData = JsonHelper.Serialize(businessRequest);
            logInfo.Success = true;
        }

        private void EndLog(ApiResponse response)
        {
            if (_logMode != ApiLogMode.Both)
                return;

            logInfo.ResponseData = JsonHelper.Serialize(response);

            SaveLog();
        }

        private void SaveLog()
        {
            _stopWatch.Stop();
            logInfo.RunTime = _stopWatch.ElapsedMilliseconds;

            if (_xUserSession != null)
            {
                logInfo.CreateUser = _xUserSession.XUserInfo.AuthId;
            }
            
            //日志记录
            var apiLogPersistence = XKernel.Get<IApiLogPersistence>();
            apiLogPersistence.Save(logInfo);
        }

        private void ReadApiLogMode()
        {
            var apiLogAttr = this.GetAttribute<ApiLogAttribute>();
            if (apiLogAttr != null)
                _logMode = apiLogAttr.Mode;
        }

        protected XUserSession GetXUserSession()
        {
            return _xUserSession;

            //return XFrameworkHelper.GetCacheProvider().Get<XUserSession>(accessToken);
        }

        #endregion
    }
}
