using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XFramework.Common.Ioc;
using XFramework.Common.RabbitMQ;

namespace XFramework.Test.Cases
{
    [TestClass]
    public class RabbitMQTest 
    {
        //测试使用192.168.4.220服务器消息队列，应确保该服务正常，并且运行测试的机器与服务器通信正常
        private readonly MqConfig _integratedTestMqConfig = new MqConfig
        {
            Host = "127.0.0.1",
            Port = 5672,
            Exchange = "exchange_xframework_test",
            QueueName = "queuename_xframework_test",
            RoutingKey = "routingkey_xframework_test",
            HeartBeat = 30,
            UserName = "angel",
            Password = "qiang"
        };

        private readonly Collection<string> _receivedMessage = new Collection<string>();

        [TestMethod]
        public void IntegratedTest_MqTest_All()
        {
            
            var manager = XKernel.Get<IMqManager>();
            var sender = manager.GetMqSender(_integratedTestMqConfig);
            var receiver = manager.GetMqReceiver(_integratedTestMqConfig);
            receiver.Handler += TestMessageHandler;

            Task.Factory.StartNew(() =>
            {
                receiver.Start();
            });
            
            const string message1 = "ForTest1";
            sender.SendMessage(message1);

            //休眠一秒，等待MQ消息接收处理
            Thread.Sleep(1000);

            Assert.IsTrue(_receivedMessage.Count == 1);
            Assert.AreEqual(message1, _receivedMessage[0]);

            const string message2 = "ForTest2";
            const string message3 = "ForTest3";
            sender.SendMessage(message2);
            sender.SendMessage(message3);
            Thread.Sleep(1000);

            Assert.IsTrue(_receivedMessage.Count == 3);
            Assert.AreEqual(message2, _receivedMessage[1]);
            Assert.AreEqual(message3, _receivedMessage[2]);


            //receiver.Stop();

            //const string message4 = "ForTest4";
            //sender.SendMessage(message4);
            //Thread.Sleep(1000);

            //Assert.IsTrue(_receivedMessage.Count == 3);

            //receiver.Start();

            //Thread.Sleep(500);

            //Assert.IsTrue(_receivedMessage.Count == 4);
            //Assert.AreEqual(message4, _receivedMessage[3]);

            //receiver.Stop();
        }

        private bool TestMessageHandler(string message)
        {
            _receivedMessage.Add(message);

            return true;
        }
    }

    public class MqTestMessage
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
