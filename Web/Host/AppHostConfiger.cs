using Funq;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;

namespace XFramework.Web.Host
{
    internal class AppHostConfiger
    {
        private readonly AppHostBase _appHost;
        private readonly Container _container;

        public AppHostConfiger(AppHostBase appHost, Container container)
        {
            _appHost = appHost;
            _container = container;

            var exceptionHandler = new ExceptionHandler(_appHost);
            _appHost.ServiceExceptionHandler = exceptionHandler.OnServiceExceptionHandler;
            _appHost.ExceptionHandler = exceptionHandler.OnHandleUncaughtException;
        }

        public void ConfigureIoc()
        {
            //if (string.IsNullOrWhiteSpace(EnvironmentConfig.Instance.RedisPoolConfig))
            //    throw new Exception("RedisPoolConfig 地址节点配置不正确");

            //_container.Register<IRedisClientsManager>(c => new PooledRedisClientManager(
            //    10, 600, EnvironmentConfig.Instance.RedisPoolConfig)).ReusedWithin(ReuseScope.None);
        }

        public void ConfigPlugins()
        {
            PluginLoader.LoadAuthFeature(_appHost.Plugins);
            PluginLoader.LoadRequestLogsFeature(_appHost.Plugins);
            PluginLoader.LoadValidationFeature(_appHost.Plugins);
            PluginLoader.LoadPostmanFeature(_appHost.Plugins);
            PluginLoader.LoadCorsFeature(_appHost.Plugins, _appHost);
            PluginLoader.LoadSwaggerFeature(_appHost.Plugins);
        }

        public void ConfigGlobleProperty()
        {
            //Set JSON web services to return idiomatic JSON camelCase properties
            JsConfig.EmitCamelCaseNames = true;
            JsConfig.IncludeNullValues = false;
            //JsConfig.ThrowOnDeserializationError = true;

            JsConfig.ExcludeTypeInfo = true;
        }
    }
}
