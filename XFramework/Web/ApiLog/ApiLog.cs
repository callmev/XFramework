using System;

namespace XFramework.Web.ApiLog
{
    /// <summary>
    /// Api接口日志
    /// </summary>
    public class ApiLog
    {
        /// <summary>
        /// 请求Id
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Api类型
        /// </summary>
        public string ApiType { get; set; }

        /// <summary>
        /// 关键值（建议使用关键业务Id, 方便日志检索）
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Api地址
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// 请求数据
        /// </summary>
        public string RequestData { get; set; }

        /// <summary>
        /// 响应数据
        /// </summary>
        public string ResponseData { get; set; }

        /// <summary>
        /// 接口是否调用成功
        /// true：成功；false：失败
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// 接口处理时长
        /// </summary>
        public long? RunTime { get; set; }

        /// <summary>
        /// 调用人
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 日志创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        public ApiLog()
        {
            CreateTime = DateTime.Now;
        }

        public ApiLog(string requestId, string apiType, string apiKey, string apiUrl, string requestData, string responseData, bool success, string exception, long runTime, string createUser)
        {
            RequestId = requestId;
            ApiType = apiType;
            ApiKey = apiKey;
            ApiUrl = apiUrl;
            RequestData = requestData;
            ResponseData = responseData;
            Success = success;
            Exception = exception;
            RunTime = runTime;
            CreateUser = createUser;
            CreateTime = DateTime.Now;
        }
    }
}
