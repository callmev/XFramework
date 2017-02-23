using System;

namespace XFramework.Web.ApiLog
{
    public class ApiLogAttribute : Attribute
    {
        public ApiLogMode Mode { get; set; }

        public ApiLogAttribute()
        {
            Mode = ApiLogMode.Both;
        }

        public ApiLogAttribute(ApiLogMode mode)
        {
            Mode = mode;
        }
    }

    public enum ApiLogMode
    {
        JustRequest,
        JustResponse,
        Both,
        None
    }
}
