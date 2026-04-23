using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MailerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IConnection _connection;
        private RabbitMQ.Client.IModel _channel;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: "user_registered_queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var user = JsonConvert.DeserializeObject<UserRegisteredEvent>(message);

                    if (user != null)
                    {
                        _logger.LogInformation($"📩 Received email for: {user.Email}");
                        await SendEmail(user);
                    }
                    else
                    {
                        _logger.LogWarning("❗ Received null user event.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"❌ Error: {ex.Message}");
                }
            };

            
            _channel.BasicConsume(
                queue: "user_registered_queue",
                autoAck: true,
                consumer: consumer
            );

            return Task.CompletedTask;
        }

        private async Task SendEmail(UserRegisteredEvent user)
        {
            try
            {
                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("your-email@gmail.com", "your-app-password"),
                    EnableSsl = true,
                };

                var mail = new MailMessage(
                    "your-email@gmail.com",
                    user.Email,
                    "Welcome 🎉",
                    $"Hello {user.Name}, welcome to our platform!"
                );

                await smtp.SendMailAsync(mail);

                _logger.LogInformation($"✅ Email sent to {user.Email}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Email failed: {ex.Message}");
            }
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Dispose();
            base.Dispose();
        }
    }

    // Event Model
    public class UserRegisteredEvent
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}