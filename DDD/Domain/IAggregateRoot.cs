namespace XFramework.DDD.Domain
{
    public interface IAggregateRoot
    {
        string Id { get; set; }

        int Version { get; set; }
    }
}
