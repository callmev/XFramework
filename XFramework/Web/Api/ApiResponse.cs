using System;
using Newtonsoft.Json;
using ServiceStack.ServiceInterface.ServiceModel;

namespace XFramework.Web.Api
{
    public class ApiResponse : IHasResponseStatus
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
        
        public ApiResponse()
        {
            Success = false;
        }

        /// <summary>
        /// 执行正常（有业务数据返回）
        /// </summary>
        /// <param name="result"></param>
        public ApiResponse(object result)
        {
            Success = true;
            Result = result;
        }

        public ApiResponse(Exception ex)
        {
            Success = false;

            if (ex != null)
                Message = ex.Message;
        }

        public ApiResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public virtual string GetResponseData()
        {
            var resultString = JsonConvert.SerializeObject(this);

            return resultString;
        }
    }
}
