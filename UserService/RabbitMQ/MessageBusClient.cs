using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using UserService.DTO;

namespace UserService.RabbitMQ
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_configuration["RabbitMQ:Url"])
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutDown;

                Console.WriteLine("--> Connected to MessageBus");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not connect to the Message Bus: {ex.Message}");
            }
        }
        public void PublishUserDeletion(DeleteUserDTO deleteUserDto)
        {
            var message = JsonSerializer.Serialize(deleteUserDto);
            if (_connection.IsOpen)
            {
                Console.WriteLine("--> RabbitMQ Connection Open, sending user deletion message...");
                SendMessage(message, "user_deletion_queue"); // Specify the appropriate queue name
            }
            else
            {
                Console.WriteLine("--> RabbitMQ connection is closed, not sending user deletion message");
            }
        }
        private void SendMessage(string message, string routingKey)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger",
                routingKey: routingKey,
                basicProperties: null,
                body: body);
            Console.WriteLine($"--> We have sent {message}");
        }

        private void Dispose()
        {
            Console.WriteLine("MessageBus Disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void RabbitMQ_ConnectionShutDown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> RabbitMQ Connection Shutdown");
        }
    }
}
