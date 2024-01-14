//using RabbitMQ.Client;

//public class ConsumerMicroservice
//{
//    private readonly IConnection _connection;
//    private readonly IModel _channel;
//    private readonly IConfiguration _configuration;


//    public ConsumerMicroservice()
//    {
//        var factory = new ConnectionFactory
//        {
//            Uri = new Uri(_configuration["RabbitMQ:Url"])
//        };

//        _connection = factory.CreateConnection();
//        _channel = _connection.CreateModel();
//    }

//    public void Start()
//    {
//        var consumer = new RabbitMQConsumer(_channel);
//        consumer.StartListening();
//    }

//    public void CloseConnection()
//    {
//        _channel.Close();
//        _connection.Close();
//    }
//}
