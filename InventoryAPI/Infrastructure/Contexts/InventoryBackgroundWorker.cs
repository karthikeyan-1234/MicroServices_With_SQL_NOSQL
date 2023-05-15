using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using InventoryAPI.Services;
using System.Text.Json;
using ERPModels;

namespace InventoryAPI.Infrastructure.Contexts
{
    public class InventoryBackgroundWorker : BackgroundService
    {
        private string[] keys;
        private string keyList;
        private readonly IServiceProvider _serviceProvider;

        private string? exchange;
        private string? queue;
        private string? routingKey;


        public InventoryBackgroundWorker(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            keyList = configuration.GetSection("RabbitMQ").GetSection("routingKeys").Value;
            keys = keyList.Split(",");
            _serviceProvider = serviceProvider;

            exchange = "ERP";
            queue = "Purchase";
            routingKey = "NewPurchase";
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Topic);


                foreach (var key in keys)
                {
                    var result = channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false);
                    var queue_name = result.QueueName;

                    channel.QueueBind(queue: queue_name, exchange: exchange, routingKey: key);

                    var consumer = new EventingBasicConsumer(channel);

                    string methodName = "Handle_" + key;
                    var type = GetType();
                    MethodInfo? methodInfo = type.GetMethod(methodName);
                    if (methodInfo != null)
                    {
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);
                            object[] parameters = new object[] { message };
                            methodInfo.Invoke(this, parameters);

                        };
                    }

                    channel.BasicConsume(queue: queue_name, autoAck: true, consumer: consumer);

                }
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
        }

        public Task<bool> Handle_NewPurchase(string message)
        {
            Console.WriteLine("Received new purchase : " + message);
            return Task.FromResult(true);
        }

        public async Task<bool> Handle_NewPurchaseDetail(string message)
        {
            Console.WriteLine("Received new purchase detail : " + message);
            var purchDet = JsonSerializer.Deserialize<PurchaseDetailDTO>(message);

            using (var scope = _serviceProvider.CreateScope())
            {
                var inventoryService = scope.ServiceProvider.GetRequiredService<IInventoryService>();
                if (purchDet is not null)
                {
                    await inventoryService.AddNewInventory(new InventoryDTO { item_id = purchDet.item_id, qty = purchDet.qty, last_edit_at = DateTime.Now});
                    return true;
                }

                return false;
            }
        }


    }
}
