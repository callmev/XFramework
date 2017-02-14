namespace XFramework.Common.RabbitMQ
{
    public interface IMqReceiver
    {
        event MessageHandler Handler;

        bool IsReceiving { get; set; }

        void Start();

        void Stop();
    }

    public delegate bool MessageHandler(string arg);
}

