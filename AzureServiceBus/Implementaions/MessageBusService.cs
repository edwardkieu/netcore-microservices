using Azure.Messaging.ServiceBus;
using AzureServiceBus.Interfaces;
using AzureServiceBus.Models;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AzureServiceBus.Implementaions
{
    public class MessageBusService : IMessageBusService
    {
        public async Task PublishMessage(BaseMessage message, string connectionString, string queueOrTopicName)
        {
            await using var client = new ServiceBusClient(connectionString);

            var sender = client.CreateSender(queueOrTopicName);
            
            var serviceBusMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

            await sender.SendMessageAsync(serviceBusMessage);

            await client.DisposeAsync();
        }
    }
}
