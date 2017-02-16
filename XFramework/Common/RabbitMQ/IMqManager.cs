namespace XFramework.Common.RabbitMQ
{
    public interface IMqManager
    {
        IMqReceiver GetMqReceiver(MqConfig config);

        IMqSender GetMqSender(MqConfig config);
    }
}
