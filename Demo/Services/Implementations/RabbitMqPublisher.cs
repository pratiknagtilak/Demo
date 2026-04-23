using Demo.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Demo.Services.Implementations
{
    public class RabbitMqPublisher: IMessagePublisher
    {
        public void PublishUserRegistered(object userEvent)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "user_registered_queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var message = JsonConvert.SerializeObject(userEvent);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: "user_registered_queue",
                basicProperties: null,
                body: body
            );
        }
    }
}
