//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using System;
//using System.Text;

//public class RabbitMQConsumer
//{
//    private readonly IModel _channel;

//    public RabbitMQConsumer(IModel channel)
//    {
//        _channel = channel;
//    }

//    public void StartListening()
//    {
//        var consumer = new EventingBasicConsumer(_channel);
//        consumer.Received += (model, ea) =>
//        {
//            var body = ea.Body.ToArray();
//            var message = Encoding.UTF8.GetString(body);

//            Console.WriteLine($" [x] Received: {message}");

//            // Handle the received message (e.g., process it in your microservice)
//            ProcessReceivedMessage(message);

//            // Acknowledge the message
//            _channel.BasicAck(ea.DeliveryTag, false);
//        };

//        _channel.BasicConsume(queue: "contents_queue",
//                              autoAck: false,
//                              consumer: consumer);

//        Console.WriteLine(" [*] Waiting for messages. To exit, press CTRL+C");
//    }

//    private void ProcessReceivedMessage(string message)
//    {
//        // Implement your logic to process the received message
//        // For example, you can update the database or perform other actions
//        Console.WriteLine($"Processing message: {message}");
//    }
//}
