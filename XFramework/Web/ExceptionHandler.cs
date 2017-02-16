using ServiceStack;
using ServiceStack.ServiceHost;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;

namespace XFramework.Web
{
    internal class ExceptionHandler
    {
        private readonly AppHostBase _appHost;

        public ExceptionHandler(AppHostBase appHost)
        {
            _appHost = appHost;
        }

        /// <summary>
        /// 处理在服务内部抛出的异常
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="request"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public object OnServiceExceptionHandler(
            IHttpRequest httpRequest, object request, System.Exception exception)
        {
            return DtoUtils.HandleException(_appHost, request, exception);
        }

        /// <summary>
        /// 处理在服务之外抛出的异常
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="httpResponse"></param>
        /// <param name="operationName"></param>
        /// <param name="exception"></param>
        public void OnHandleUncaughtException(
            IHttpRequest httpRequest, IHttpResponse httpResponse, string operationName, System.Exception exception)
        {
            httpResponse.Write("Error: {0}: {1}".Fmt(exception.GetType().Name, exception.Message));
            httpResponse.EndRequest(skipHeaders: true);
            DtoUtils.HandleException(_appHost, httpResponse, exception);
        }
    }
}
