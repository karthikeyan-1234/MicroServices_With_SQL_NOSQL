using MassTransit.RabbitMqTransport.Topology;

using MediatR;
using PurchaseAPI.CQRS.Notifications;
using RabbitMQ.Client;
using System.Text;

namespace PurchaseAPI.CQRS.Handlers.RabbitMQ
{
    public class PurchaseAddedNotificationRMQHandler : INotificationHandler<PurchaseAddedNotification>
    {
        private string? exchange;
        private string? queue;
        private string? routingKey;

        public PurchaseAddedNotificationRMQHandler()
        {
            exchange = "ERP";
            queue = "Purchase";
            routingKey = "NewPurchase";
        }

        public Task Handle(PurchaseAddedNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Purchase Added Notification for RMQ with msg : " + notification.Message);

            var msg = notification.Message == null ? "" : notification.Message;

            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using (var connection = factory.CreateConnection())

            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Topic);
                var result = channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false);
                var queue_name = result.QueueName;

                channel.QueueBind(queue: queue_name,
                  exchange: exchange,
                  routingKey: routingKey);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;

                var body1 = Encoding.UTF8.GetBytes(msg);

                channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: properties, body: body1);
            }

            return Task.CompletedTask;
        }
    }
}
