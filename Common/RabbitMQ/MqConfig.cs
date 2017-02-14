namespace XFramework.Common.RabbitMQ
{
    /// <summary>
    /// Mq����ʵ����
    /// </summary>
    public class MqConfig
    {
        /// <summary>
        /// ��Ϣ���з����ַ
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// ��Ϣ���з���˿�
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public string Exchange { get; set; }

        /// <summary>
        /// ·�ɼ�
        /// </summary>
        public string RoutingKey { get; set; }

        /// <summary>
        /// �û���
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// �������룩
        /// </summary>
        public ushort HeartBeat { get; set; }
    }
}

