namespace XFramework.Common.RabbitMQ
{
    public interface IMqSender
    {
        void SendMessage(string message);
    }
}
