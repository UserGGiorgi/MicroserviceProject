using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata;
using platformService.Dtos;
using RabbitMQ.Client;

namespace platformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly RabbitMQ.Client.IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory(){HostName = _configuration["RabbitMQHost"],
            Port = int.Parse(_configuration["RabbitMQPort"])};
            try
            {
               _connection = factory.CreateConnection();
               _channel = _connection.CreateModel();

               _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

               _connection.ConnectionShutdown += RabbitMQ_ConnectionShutDown;

               System.Console.WriteLine("--> connected to messageBus");
            }
            catch(Exception ex)
            {
                System.Console.WriteLine($"--> Could not connect to the message bnus {ex.Message}");
            }

        }
        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            var message = JsonSerializer.Serialize(platformPublishedDto);
            if(_connection.IsOpen)
            {
                System.Console.WriteLine("--> RabbtiMQ Connection Open, Sending message...");
                SendMessage(message);
            }
            else
            {
                System.Console.WriteLine("--> RabbitMQ Conenction Shutdown");
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger",
                            routingKey: "",
                            basicProperties: null,
                            body: body);
            System.Console.WriteLine($"--> We have sent {message}");
        }
        public void Dispose()
        {
            System.Console.WriteLine("MessageBus Disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
        private void RabbitMQ_ConnectionShutDown(object sender, ShutdownEventArgs e)
        {
            System.Console.WriteLine("--> RabbitMQ Connection Shutdown");
        }
    }
}