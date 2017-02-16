using System.Web;
using ServiceStack.ServiceHost;

namespace XFramework.Web
{
    public static class CookieManager
    {
        public static string GetCookies(IHttpRequest httpRequest)
        {
            if (httpRequest == null || httpRequest.Cookies == null)
                return string.Empty;

            var a = string.Empty;
            var b = string.Empty;
            if (httpRequest.Cookies.Count > 0)
            {
                if (httpRequest.Cookies.ContainsKey("ss-id"))
                {
                    if (httpRequest.Cookies["ss-id"] != null)
                        a = httpRequest.Cookies["ss-id"].Value;
                }

                if (httpRequest.Cookies.ContainsKey("ss-pid"))
                {
                    if (httpRequest.Cookies["ss-pid"] != null)
                        b = httpRequest.Cookies["ss-pid"].Value;
                }
            }

            if (a.Length == 0 && b.Length == 0)
                return string.Empty;

            return string.Format("ss-id={0};ss-pid={1}", a, b);
        }

        public static string GetCookies(IHttpResponse httpResponse)
        {
            if (httpResponse == null || httpResponse.OriginalResponse == null)
                return string.Empty;

            var originalResponse = httpResponse.OriginalResponse as HttpResponse;
            if (originalResponse == null)
                return string.Empty;

            var a = string.Empty;
            var b = string.Empty;
            if (originalResponse.Cookies.Count > 0)
            {
                if (originalResponse.Cookies["ss-id"] != null)
                    a = originalResponse.Cookies["ss-id"].Value;

                if (originalResponse.Cookies["ss-pid"] != null)
                    b = originalResponse.Cookies["ss-pid"].Value;
            }

            if (a == null || b == null)
                return string.Empty;

            if (a.Length == 0 && b.Length == 0)
                return string.Empty;

            return string.Format("ss-id={0};ss-pid={1}", a, b);
        }
    }
}