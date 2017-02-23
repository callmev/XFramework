namespace XFramework.Web.ApiLog
{
    public interface IApiLogPersistence
    {
        void Save(ApiLog log);
    }
}
