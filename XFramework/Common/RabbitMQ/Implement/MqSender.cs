using System.Text;
using log4net;
using RabbitMQ.Client;

namespace XFramework.Common.RabbitMQ.Implement
{
    public class MqSender : IMqSender
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(MqSender));

        private MqConfig _config;

        public MqSender(MqConfig config)
        {
            _config = config;
        }

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory
            {
                HostName = _config.Host,
                UserName = _config.UserName,
                Password = _config.Password,
                Port = _config.Port
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(_config.Exchange, ExchangeType.Direct, true);
                    channel.QueueDeclare(_config.QueueName, true, false, false, null);
                    channel.QueueBind(_config.QueueName, _config.Exchange, _config.RoutingKey);

                    var body = Encoding.UTF8.GetBytes(message);

                    var properties = channel.CreateBasicProperties();
                    properties.SetPersistent(true);

                    channel.BasicPublish(_config.Exchange, _config.RoutingKey, properties, body);
                    _logger.Info(string.Format("已发送消息：{0}", message));
                }
            }
        }
    }
}
