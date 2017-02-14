using System.Collections.Generic;
using ServiceStack.Api.Postman;
using ServiceStack.Api.Swagger;
using ServiceStack.ServiceInterface.Admin;
using ServiceStack.ServiceInterface.Cors;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.WebHost.Endpoints;

namespace XFramework.Web.Host
{
    public static class PluginLoader
    {
        public static void LoadAuthFeature(IList<IPlugin> plugins)
        {
            //plugins.Add(new AuthFeature(() =>
            //        new CustomUserSession(),
            //        new IAuthProvider[] { new CustomCredentialsAuthProvider() }));
        }

        public static void LoadRequestLogsFeature(IList<IPlugin> plugins)
        {
            plugins.Add(new RequestLogsFeature
            {
                RequiredRoles = new string[] { },
                EnableErrorTracking = true,
                EnableResponseTracking = true,
                EnableSessionTracking = true,
                Capacity = int.MaxValue
            });
        }

        public static void LoadValidationFeature(IList<IPlugin> plugins)
        {
            //Enable the validation feature
            plugins.Add(new ValidationFeature());
        }

        public static void LoadPostmanFeature(IList<IPlugin> plugins)
        {
            plugins.Add(new PostmanFeature
            {
                LocalOnly = false,
                DefaultLabel = "{route},{type}",
                DefaultHeaders = new[] { "Accept:application/json" }
            });
        }

        public static void LoadCorsFeature(IList<IPlugin> plugins, AppHostBase appHost)
        {
            plugins.Add(new CorsFeature()); //Registers global CORS Headers
        }

        public static void LoadSwaggerFeature(IList<IPlugin> plugins)
        {
            plugins.Add(new SwaggerFeature());
        }
    }
}
