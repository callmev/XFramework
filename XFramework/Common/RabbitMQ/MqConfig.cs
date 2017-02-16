namespace XFramework.Common.RabbitMQ
{
    /// <summary>
    /// Mq配置实体类
    /// </summary>
    public class MqConfig
    {
        /// <summary>
        /// 消息队列服务地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 消息队列服务端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 交换器
        /// </summary>
        public string Exchange { get; set; }

        /// <summary>
        /// 路由键
        /// </summary>
        public string RoutingKey { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 心跳（秒）
        /// </summary>
        public ushort HeartBeat { get; set; }
    }
}

