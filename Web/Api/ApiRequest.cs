using ServiceStack.ServiceHost;

namespace XFramework.Web.Api
{
    public abstract class ApiRequest : IReturn<ApiResponse>
    {
        public string AuthId { get; set; }

        public string AuthToken { get; set; }
    }
}
