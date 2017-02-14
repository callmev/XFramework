namespace XFramework.Common.RabbitMQ.Implement
{
    public class MqManager : IMqManager
    {
        public IMqReceiver GetMqReceiver(MqConfig config)
        {
            return new MqReceiver(config);
        }

        public IMqSender GetMqSender(MqConfig config)
        {
            return new MqSender(config);
        }
    }
}

