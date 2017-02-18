using System.Text;
using log4net;
using RabbitMQ.Client;

namespace XFramework.Common.RabbitMQ.Implement
{
    public class MqReceiver : IMqReceiver
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(MqReceiver));

        private MqConfig _config;

        public event MessageHandler Handler;

        public bool IsReceiving { get; set; }

        public MqReceiver(MqConfig config)
        {
            _config = config;
            IsReceiving = false;
        }

        public void Start()
        {
            IsReceiving = true;
            var factory = new ConnectionFactory
            {
                HostName = _config.Host,
                UserName = _config.UserName,
                Password = _config.Password,
                Port = _config.Port,
                RequestedHeartbeat = _config.HeartBeat
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(_config.Exchange, ExchangeType.Direct, true);
                    channel.QueueDeclare(_config.QueueName, true, false, false, null);
                    channel.QueueBind(_config.QueueName, _config.Exchange, _config.RoutingKey);

                    var consumer = new QueueingBasicConsumer(channel);

                    channel.BasicConsume(_config.QueueName, false, consumer);

                    while (IsReceiving)
                    {
                        var ea = consumer.Queue.Dequeue();
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);

                        _logger.Info(string.Format("已接收到消息：{0}", message));

                        bool success = true;
                        if (Handler != null)
                        {
                            success = Handler(message);
                        }

                        if (success)
                        {
                            _logger.Info(string.Format("处理成功，已发送回执消息(Ack)，已完成消息：{0}", message));
                            channel.BasicAck(ea.DeliveryTag, false);
                        }
                        else
                        {
                            _logger.Info(string.Format("处理失败，已发送回执消息(Nack)，不再处理此消息：{0}", message));

                            //回执：不再处理此条消息
                            channel.BasicNack(ea.DeliveryTag, true, false);
                        }
                    }
                }
            }
        }

        public void Stop()
        {
            IsReceiving = false;
        }
    }
}

